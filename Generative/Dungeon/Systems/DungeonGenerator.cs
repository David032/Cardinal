using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.AI.Navigation;
using Runic.Characteristics;
using Runic.SceneManagement;
using System;
using Random = UnityEngine.Random;

namespace Cardinal.Generative.Dungeon
{
    public class DungeonGenerator : Generator
    {
        public bool FORCEBUILD = false;
        [Header("Variables")]
        //Size controls the max number of rooms generated
        public SizeOfDungeon DungeonSize = SizeOfDungeon.Small;
        //Type determines how it'll generate
        public TypeOfDungeon DungeonType = TypeOfDungeon.Branching;
        //Do we want a boss room?
        public bool RequiresBoss = true;
        //Puzzle Rooms?
        [Range(0, 4)]
        public int NumberOfPuzzleRooms = 0;
        //Special Rooms?
        [Range(0, 4)]
        public int NumberOfSpecialRooms = 0;
        //Harvestable resources
        public ResourceAvailability ResourceNodeSpread = 
            ResourceAvailability.Regular;
        //Lootable Objects
        public ResourceAvailability LootNodeSpread = 
            ResourceAvailability.Regular;
        //How many enemies to spawn?
        public ResourceAvailability EnemyAmount = 
            ResourceAvailability.Regular;
        [Header("Data Lists")]
        public CardinalGameobjectList RoomList;
        public CardinalGameobjectList StarterRooms;
        public CardinalGameobjectList BossRooms;
        public CardinalGameobjectList PuzzleRooms;
        public CardinalGameobjectList SpecialRooms;
        public CardinalGameobjectList ResourceNodes;
        public CardinalGameobjectList LootNodes;
        public CardinalGameobjectList EnemyList;
        public CardinalGameobjectList BossList;
        [Header("Generated Data")]
        public List<GameObject> GeneratedRooms;
        public List<GameObject> spawnedLoot;
        public List<GameObject> spawnedNodes;
        public List<GameObject> spawnedEnemies;
        public GameObject spawnedBoss;
        [Header("Built Status")]
        public BuildState State = BuildState.Empty;
        [Header("Internals")]
        GameObject SpawnRoom;
        GameObject BossRoom;
        GameObject priorRoom;



        // Start is called before the first frame update
        void Start()
        {
            if (FORCEBUILD)
            {
                StartCoroutine(LoadDungeon());
            }
        }
        public IEnumerator LoadDungeon() 
        {
            State = BuildState.Building;
            GenerateDungeon();
            GetComponent<NavMeshSurface>().BuildNavMesh();
            yield return new WaitForSeconds(2.5f);
            SpreadObjects(LootNodes, LootNodeSpread, MarkerType.Loot);
            yield return new WaitForSeconds(2.5f);
            SpreadObjects(ResourceNodes, ResourceNodeSpread, 
                MarkerType.Resource);
            yield return new WaitForSeconds(2.5f);
            SpawnEnemies(EnemyList, EnemyAmount, MarkerType.Enemy);
            State = BuildState.Built;
        }
        public void GenerateDungeon() 
        {
            switch (DungeonType)
            {
                case TypeOfDungeon.Linear:
                    break;
                case TypeOfDungeon.Branching:
                    GenerateSpawnRoom();
                    GenerateMainPath();
                    if (NumberOfSpecialRooms != 0)
                    {
                        GenerateSpecialRooms();
                    }
                    if (NumberOfPuzzleRooms != 0)
                    {
                        GeneratePuzzleRooms();
                    }
                    GenerateSecondaryRooms();
                    DoorCheck();
                    CheckForGenerationErrors();
                    break;
                case TypeOfDungeon.Special:
                    break;
                default:
                    break;
            }
        }

        #region PhysicalGeneration
        public void GenerateSpawnRoom() 
        {
            GameObject roomToSpawn = Instantiate
                (StarterRooms.GetRandomObject());
            roomToSpawn.transform.position = Vector3.zero;
            roomToSpawn.GetComponent<Room>().isMainRoute = true;
            SpawnRoom = roomToSpawn;
            GeneratedRooms.Add(roomToSpawn);
            priorRoom = SpawnRoom;
        }
        public void GenerateMainPath()
        {
            Doorway currentDoor = GetRandomDoor
                (SpawnRoom.GetComponent<Room>());
            currentDoor.ActivateDoorway();
            currentDoor.IsUsed = true;
            for (int i = 0; i < ((int)DungeonSize / 2) - 1; i++)
            {
                GameObject SpawnedRoom =
                    GenerateAndReturnSuitableRoom(currentDoor, 2);
                SpawnedRoom.GetComponent<Room>().isMainRoute = true;
                priorRoom = SpawnedRoom;
                currentDoor = GetRandomDoor
                    (SpawnedRoom.GetComponent<Room>());
            }

            if (RequiresBoss)
            {
                BossRoom = GenerateNonGenericRoom(currentDoor, BossRooms);
                BossRoom.GetComponent<Room>().isMainRoute = true;
                SpawnBoss();
            }

            //CheckForGenerationErrors();
        }
        private void CheckForGenerationErrors()
        {
            GeneratedRooms.Reverse();
            List<GameObject> DeactivatedRooms = new List<GameObject>();
            List<Tuple<GameObject, GameObject>> 
               DeactivatedRoomPairs = new();

            //Identify Overlapping Rooms
            foreach (GameObject item in GeneratedRooms)
            {
                List<GameObject> roomsTocheck = GeneratedRooms;
                foreach (GameObject room in roomsTocheck)
                {
                    Room thisRoom = item.GetComponent<Room>();
                    bool isStartOrEnd = thisRoom.RoomFlags.Contains
                        (RoomFlags.StartingRoom) ||
                        thisRoom.RoomFlags.Contains(RoomFlags.BossRoom);

                    if (Vector3.Distance(item.transform.position,
                        room.transform.position) < 5
                        && !(item == room) && !(isStartOrEnd))
                    {
                        print(item + " and " + room + 
                            " appear to be overlapping at "
                            + room.transform.position);
                        print("With a spacing of " +
                            Vector3.Distance(item.transform.position,
                            room.transform.position));
                        Tuple<GameObject, GameObject> roomPair = 
                            new(item, room); 
                        DeactivatedRoomPairs.Add(roomPair);
                        item.SetActive(false);
                        DeactivatedRooms.Add(item);
                    }
                }
            }

            //for (int i = 1; i < DeactivatedRooms.Count; i += 2)
            //{
            //    DeactivatedRooms[i].SetActive(true);
            //}

            #region Room Pairing Calculations
            //Dictionary<GameObject, GameObject> PairedRooms = 
            //    new Dictionary<GameObject, GameObject>();
            //foreach (GameObject room in DeactivatedRooms)
            //{
            //    GameObject nearestRoom = 
            //        GetClosestRoom(DeactivatedRooms, room);
            //    PairedRooms.Add(nearestRoom, room);                
            //}

            foreach (var item in DeactivatedRoomPairs)
            {
                print("Determining: " + item.Item1 + " & "
                    + item.Item2);
                //If A room has less doors than B room
                if (item.Item1.GetComponent<Room>().doorways.Count
                    < item.Item2.GetComponent<Room>().doorways.Count)
                {
                    item.Item1.SetActive(true);
                    print("Selected " + item.Item1);
                }
                //if B room has less doors than A room
                else if (item.Item1.GetComponent<Room>().doorways.Count
                    > item.Item2.GetComponent<Room>().doorways.Count)
                {
                    item.Item2.SetActive(true);
                    print("Selected " + item.Item2);
                }
                else
                {
                    item.Item1.SetActive(true);
                    print("Selected " + item.Item1);
                }
            }
            #endregion
        }

        GameObject GetClosestRoom(List<GameObject> rooms, 
            GameObject SeekingRoom)
        {
            GameObject bestTarget = null;

            Dictionary<GameObject, float> DistanceToRooms = new();
            foreach (GameObject item in rooms)
            {
                float distance = Vector3.Distance
                    (SeekingRoom.transform.position, 
                    item.transform.position);
                DistanceToRooms.Add(item, distance);
            }
            DistanceToRooms.OrderBy(x => x.Value);
            bestTarget = DistanceToRooms.First().Key;
            return bestTarget;
        }

        public void GenerateSecondaryRooms() 
        {
            List<GameObject> SecondaryRooms = new List<GameObject>();
            priorRoom = null;
            int roomBudget = (int)DungeonSize - GeneratedRooms.Count;
            print("Generating " + roomBudget + " rooms");
            for (int i = 0; i < roomBudget; i++)
            {
                //Find potential doors
                List<Doorway> doorways = new List<Doorway>();
                foreach (GameObject room in GeneratedRooms)
                {
                    Room roomData = room.GetComponent<Room>();
                    foreach (Doorway item in roomData.doorways)
                    {
                        if (!item.IsUsed)
                        {
                            doorways.Add(item);
                        }
                    }
                }
                //check if there's any left
                if (doorways.Count == 0)
                {
                    break;
                }

                //Select a doorway
                Doorway doorTobuildOffOf = GetRandomDoorFromList(doorways);
                //Build a new room
                GameObject secondaryRoom = 
                    GenerateAndReturnRoom(doorTobuildOffOf);
                if (Vector3.Distance(secondaryRoom.transform.position,
                    SpawnRoom.transform.position) < 5)
                {
                    GeneratedRooms.Remove(secondaryRoom);
                    secondaryRoom.SetActive(false);
                    print("Overlapped with spawn, removing!");
                }
                priorRoom = secondaryRoom;
                secondaryRoom.GetComponent<Room>().Type = 
                    RoomType.SecondaryRoom;
                SecondaryRooms.Add(secondaryRoom);
            }

            foreach (GameObject roomToTest in SecondaryRooms)
            {
                List<GameObject> RoomsTocheckAgainst = 
                    new List<GameObject>(GeneratedRooms);
                RoomsTocheckAgainst.Remove(roomToTest);
                foreach (GameObject Room in RoomsTocheckAgainst)
                {
                    float DistanceBetweenRooms = Vector3.Distance
                        (roomToTest.transform.position, 
                        Room.transform.position);
                    if (DistanceBetweenRooms < 5)
                    {
                        roomToTest.SetActive(false);
                        print("Set " + roomToTest 
                            + " to false as it overlapped with " + Room);
                    }
                }
            }
            //CheckForGenerationErrors();
        }
        public void GeneratePuzzleRooms()
        {
            List<GameObject> SecondaryRooms = new List<GameObject>();
            priorRoom = null;
            for (int i = 0; i < NumberOfPuzzleRooms; i++)
            {
                //Find potential doors
                List<Doorway> doorways = new List<Doorway>();
                foreach (GameObject room in GeneratedRooms)
                {
                    Room roomData = room.GetComponent<Room>();
                    foreach (Doorway item in roomData.doorways)
                    {
                        if (!item.IsUsed)
                        {
                            doorways.Add(item);
                        }
                    }
                }
                //check if there's any left
                if (doorways.Count == 0)
                {
                    break;
                }

                //Select a doorway
                Doorway doorTobuildOffOf = GetRandomDoorFromList(doorways);
                //Build a new room
                GameObject secondaryRoom = 
                    GenerateNonGenericRoom(doorTobuildOffOf,PuzzleRooms);
                if (Vector3.Distance(secondaryRoom.transform.position,
                    SpawnRoom.transform.position) < 5)
                {
                    GeneratedRooms.Remove(secondaryRoom);
                    secondaryRoom.SetActive(false);
                    print("Overlapped with spawn, removing!");
                }
                priorRoom = secondaryRoom;
                SecondaryRooms.Add(secondaryRoom);
            }

            foreach (GameObject roomToTest in SecondaryRooms)
            {
                List<GameObject> RoomsTocheckAgainst = GeneratedRooms;
                RoomsTocheckAgainst.Remove(roomToTest);
                foreach (GameObject Room in RoomsTocheckAgainst)
                {
                    float DistanceBetweenRooms = Vector3.Distance
                        (roomToTest.transform.position, 
                        Room.transform.position);
                    if (DistanceBetweenRooms < 5)
                    {
                        roomToTest.SetActive(false);
                        print("Set " + roomToTest 
                            + " to false as it overlapped with " + Room);
                    }
                }
            }
            //CheckForGenerationErrors();
        }
        public void GenerateSpecialRooms()
        {
            List<GameObject> SecondaryRooms = new List<GameObject>();
            priorRoom = null;
            for (int i = 0; i < NumberOfPuzzleRooms; i++)
            {
                //Find potential doors
                List<Doorway> doorways = new List<Doorway>();
                foreach (GameObject room in GeneratedRooms)
                {
                    Room roomData = room.GetComponent<Room>();
                    foreach (Doorway item in roomData.doorways)
                    {
                        if (!item.IsUsed)
                        {
                            doorways.Add(item);
                        }
                    }
                }
                //check if there's any left
                if (doorways.Count == 0)
                {
                    break;
                }

                //Select a doorway
                Doorway doorTobuildOffOf = GetRandomDoorFromList(doorways);
                //Build a new room
                GameObject secondaryRoom = 
                    GenerateNonGenericRoom(doorTobuildOffOf,
                    SpecialRooms);
                if (Vector3.Distance(secondaryRoom.transform.position,
                    SpawnRoom.transform.position) < 5)
                {
                    GeneratedRooms.Remove(secondaryRoom);
                    secondaryRoom.SetActive(false);
                    print("Overlapped with spawn, removing!");
                }
                priorRoom = secondaryRoom;
                SecondaryRooms.Add(secondaryRoom);
            }

            foreach (GameObject roomToTest in SecondaryRooms)
            {
                List<GameObject> RoomsTocheckAgainst = GeneratedRooms;
                RoomsTocheckAgainst.Remove(roomToTest);
                foreach (GameObject Room in RoomsTocheckAgainst)
                {
                    float DistanceBetweenRooms = Vector3.Distance
                        (roomToTest.transform.position,
                        Room.transform.position);
                    if (DistanceBetweenRooms < 5)
                    {
                        roomToTest.SetActive(false);
                        print("Set " + roomToTest 
                            + " to false as it overlapped with " + Room);
                    }
                }
            }
            //CheckForGenerationErrors();
        }
        #endregion

        #region Door Functions
        public Doorway GetMainPathStart() 
        {
            Room startingRoom = SpawnRoom.GetComponent<Room>();
            int randomDoorSelection = Random.Range
                (0, startingRoom.doorways.Count);
            return startingRoom.doorways[randomDoorSelection];
        }

        public Doorway GetRandomDoorway(Room room) 
        {
            int randomDoorSelection = Random.Range(0, room.doorways.Count);
            room.doorways[randomDoorSelection].DisableDoor();
            room.doorways[randomDoorSelection].IsUsed = true;
            return room.doorways[randomDoorSelection];
        }

        public Heading InvertDoorDirection(Heading heading) 
        {
            switch (heading)
            {
                case Heading.North:
                    return Heading.South;
                case Heading.East:
                    return Heading.West;
                case Heading.South:
                    return Heading.North;
                case Heading.West:
                    return Heading.East;
                default:
                    Debug.LogError("Returned a non-existant door!");
                    return Heading.North;
            }
        }

        public void DoorCheck()
        {
            //find doors that think they're unused
            List<Doorway> doorsTocheck = new List<Doorway>();
            foreach (GameObject RoomObject in GeneratedRooms)
            {
                Room RoomData = RoomObject.GetComponent<Room>();
                foreach (Doorway Door in RoomData.doorways)
                {
                    if (!Door.IsUsed)
                    {
                        doorsTocheck.Add(Door);
                    }
                }
            }

            //Check if they are
            foreach (Doorway door in doorsTocheck)
            {
                RaycastHit hit;
                if (Physics.Raycast
                    (door.gameObject.transform.position + Vector3.up,
                    door.gameObject.transform.forward * -1, out hit))
                {
                    if (hit.transform != null)
                    {
                        door.IsUsed = true;
                        door.DisableDoor();
                        //hit.transform.gameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion

        #region DeterminationFunctions     
        public GameObject GetRandomRoom(List<GameObject> candidateRooms) 
        {
            int randomSelection = Random.Range(0, candidateRooms.Count);
            return candidateRooms[randomSelection];
        }

        public Doorway GetRandomDoor(Room room) 
        {
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                if (!item.IsUsed)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            return validDoors[randomSelection];
        }

        public Doorway GetExclusionaryRandomDoor(Room room,
            Doorway doorToIgnore)
        {
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                if (!item.IsUsed && !doorToIgnore)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            return validDoors[randomSelection];
        }

        public Doorway GetRandomClearDoor(Room room)
        {
            List<Doorway> validDoors = new List<Doorway>();
            foreach (var item in room.doorways)
            {
                bool isClear = false;

                Collider[] colliders = Physics.OverlapBox
                    (item.GetNextRoomPlace().position, Vector3.one);
                if (colliders != null)
                {
                    isClear = true;
                }
                else
                {
                    print(room + " is not clear, on doorway " + item);
                }
                if (!item.IsUsed && isClear)
                {
                    validDoors.Add(item);
                }
            }
            int randomSelection = Random.Range(0, validDoors.Count);
            return validDoors[randomSelection];
        }

        public bool TestForOverlap(Vector3 loc) 
        {
            Collider[] colliders = Physics.OverlapBox
                (loc, Vector3.one);
            if (colliders != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Doorway GetRandomDoorFromList(List<Doorway> potentialDoors) 
        {
            int randomSelection = Random.Range(0, potentialDoors.Count);
            return potentialDoors[randomSelection];
        }

        #endregion

        #region GenerativeFunctions
        //0 criteria
        public void GenerateSuitableRoom(Doorway connection) 
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.Objects)
            {
                var RoomRef = item.GetComponent<Room>();
                if (RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide)))
                {
                    suitableRooms.Add(item);
                }
            }

            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            roomToSpawn.transform.rotation.eulerAngles.Set(0, 0, 0);
            roomToSpawn.transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            Room roomToSpawnData = roomToSpawn.GetComponent<Room>();
            foreach (Doorway item in roomToSpawnData.doorways)
            {
                if (item.Facing == InvertDoorDirection(connectionSide))
                {
                    item.IsUsed = true;
                    item.DisableDoor();
                    connection.IsUsed = true;
                    if (!priorRoom.GetComponent<Room>().RoomFlags
                        .Contains(RoomFlags.StartingRoom))
                    {
                        connection.DisableDoor();
                    }
                }
            }
            GeneratedRooms.Add(roomToSpawn);
        }

        //0 Criteria, returns GO
        public GameObject GenerateAndReturnRoom(Doorway connection)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.Objects)
            {
                var RoomRef = item.GetComponent<Room>();
                if (RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide)))
                {
                    suitableRooms.Add(item);
                }
            }
            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            roomToSpawn.transform.rotation.eulerAngles.Set(0, 0, 0);
            roomToSpawn.transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            Room roomToSpawnData = roomToSpawn.GetComponent<Room>();
            foreach (Doorway item in roomToSpawnData.doorways)
            {
                if (item.Facing == InvertDoorDirection(connectionSide))
                {
                    if (priorRoom == null)
                    {
                        break;
                    }
                    item.IsUsed = true;
                    item.DisableDoor();
                    connection.IsUsed = true;
                    if (!priorRoom.GetComponent<Room>().RoomFlags
                        .Contains(RoomFlags.StartingRoom))
                    {
                        connection.DisableDoor();
                    }
                }
            }
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }

        //Number of Doors criteria
        public void GenerateSuitableRoom(Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.Objects)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any(D => D.Facing == connectionSide)) 
                    && (RoomRef.doorways.Count >= minimumDoorNumber))
                {
                    suitableRooms.Add(item);
                }
            }

            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
        }

        //Number of Doors criteria, Returns GO
        public GameObject GenerateAndReturnSuitableRoom
            (Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.Objects)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide))
                    && (RoomRef.doorways.Count >= minimumDoorNumber)))
                {
                    suitableRooms.Add(item);
                }
            }
            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            roomToSpawn.transform.rotation.eulerAngles.Set(0, 0, 0);
            roomToSpawn.transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            Room roomToSpawnData = roomToSpawn.GetComponent<Room>();
            foreach (Doorway item in roomToSpawnData.doorways)
            {
                if (item.Facing == InvertDoorDirection(connectionSide))
                {
                    item.IsUsed = true;
                    item.ActivateDoorway();
                    connection.IsUsed = true;
                    if (!priorRoom.GetComponent<Room>().RoomFlags
                        .Contains(RoomFlags.StartingRoom))
                    {
                        connection.ActivateDoorway();
                    }
                }
            }
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }
        public GameObject TestGenerateAndReturnSuitableRoom
            (Doorway connection, int minimumDoorNumber)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in RoomList.Objects)
            {
                var RoomRef = item.GetComponent<Room>();
                if ((RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide))
                    && (RoomRef.doorways.Count >= minimumDoorNumber)))
                {
                    suitableRooms.Add(item);
                }
            }

            //Check if connection okay
            Collider[] colliders = Physics.OverlapBox
                (connection.GetNextRoomPlace().position, Vector3.one);
            if (colliders != null)
            {
                //GameObject roomToSpawn = Instantiate
                //    (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
                //roomToSpawn.transform.parent = null;
                //print(colliders);

                //foreach (Doorway item in roomToSpawn.GetComponent<Room>().doorways)
                //{
                //    if (item.Facing == InvertDoorDirection(connectionSide))
                //    {
                //        item.IsUsed = true;
                //    }
                //}
                //GeneratedRooms.Add(roomToSpawn);
                //roomToSpawn.GetComponent<Room>().isBroken = true;
                return null;
            }
            else
            {
                GameObject roomToSpawn = Instantiate
                    (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
                roomToSpawn.transform.parent = null;

                foreach (Doorway item in roomToSpawn.GetComponent<Room>().doorways)
                {
                    if (item.Facing == InvertDoorDirection(connectionSide))
                    {
                        item.IsUsed = true;
                    }
                }
                GeneratedRooms.Add(roomToSpawn);
                return roomToSpawn;
            }
        }

        //Non-Standard Room Generation
        public GameObject GenerateNonGenericRoom
            (Doorway connection, CardinalGameobjectList specialRoomList)
        {
            Heading connectionSide = connection.Facing;
            List<GameObject> suitableRooms = new List<GameObject>();
            foreach (GameObject item in specialRoomList.Objects)
            {
                var RoomRef = item.GetComponent<Room>();
                if (RoomRef.doorways.Any
                    (D => D.Facing == InvertDoorDirection(connectionSide)))
                {
                    suitableRooms.Add(item);
                }
            }

            GameObject roomToSpawn = Instantiate
                (GetRandomRoom(suitableRooms), connection.GetNextRoomPlace());
            roomToSpawn.transform.parent = null;
            roomToSpawn.transform.rotation.eulerAngles.Set(0, 0, 0);
            roomToSpawn.transform.rotation = Quaternion.EulerAngles(0, 0, 0);

            Room roomToSpawnData = roomToSpawn.GetComponent<Room>();
            foreach (Doorway item in roomToSpawnData.doorways)
            {
                if (item.Facing == InvertDoorDirection(connectionSide))
                {
                    item.IsUsed = true;
                    item.ActivateDoorway();
                    connection.IsUsed = true;
                    connection.ActivateDoorway();
                }
            }
            GeneratedRooms.Add(roomToSpawn);
            return roomToSpawn;
        }
        #endregion

        #region ContentGeneration
        public void SpreadObjects(CardinalGameobjectList sourceObjects,
            ResourceAvailability availability, MarkerType type)
        {
            GameObject holder = new GameObject();
            holder.name = type.ToString();

            spawnedLoot = new List<GameObject>();
            for (int i = 0; i < AvailablityCount(availability); i++)
            {
                List<GameObject> potentialLocations = new List<GameObject>();
                GameObject[] markers = GameObject.FindGameObjectsWithTag("NodeMarker");

                foreach (var item in markers)
                {
                    NodeMarker thisMarker = item.GetComponent<NodeMarker>();
                    if (!thisMarker.isUsed && thisMarker.type == type)
                    {
                        potentialLocations.Add(item);
                    }
                }

                //int RandomLootSelection = Random.Range(0, sourceObjects.LootNodes.Count);
                int RandomPlaceSelection = Random.Range(0, potentialLocations.Count);
                if (potentialLocations.Count == 0)
                {
                    break;
                }
                GameObject LocationToSpawn = potentialLocations[RandomPlaceSelection];
                GameObject LootToSpawn = Instantiate
                    (sourceObjects.GetRandomObject(),
                    LocationToSpawn.transform);
                LocationToSpawn.GetComponent<NodeMarker>().isUsed = true;
                //LootToSpawn.transform.parent = holder.transform;

                spawnedLoot.Add(LootToSpawn);
            }
        }

        public void SpawnEnemies(CardinalGameobjectList sourceObjects,
            ResourceAvailability availability, MarkerType type)
        {
            GameObject holder = new GameObject();
            holder.name = type.ToString();

            spawnedLoot = new List<GameObject>();
            for (int i = 0; i < AvailablityCount(availability); i++)
            {
                List<GameObject> potentialLocations = new List<GameObject>();
                GameObject[] markers = GameObject.FindGameObjectsWithTag("NodeMarker");

                foreach (var item in markers)
                {
                    NodeMarker thisMarker = item.GetComponent<NodeMarker>();
                    if (!thisMarker.isUsed && thisMarker.type == type)
                    {
                        potentialLocations.Add(item);
                    }
                }

                int RandomPlaceSelection = Random.Range(0, potentialLocations.Count);
                if (potentialLocations.Count == 0)
                {
                    break;
                }
                GameObject LocationToSpawn = potentialLocations[RandomPlaceSelection];
                GameObject EnemyToSpawn = Instantiate
                    (sourceObjects.GetRandomObject(),
                    LocationToSpawn.transform);
                LocationToSpawn.GetComponent<NodeMarker>().isUsed = true;

                spawnedEnemies.Add(EnemyToSpawn);
            }

        }

        int AvailablityCount(ResourceAvailability resource)
        {
            switch (resource)
            {
                case ResourceAvailability.None:
                    return 0;
                case ResourceAvailability.Sparse:
                    return (int)DungeonSize / 4;
                case ResourceAvailability.Regular:
                    return (int)DungeonSize / 2;
                case ResourceAvailability.Abundant:
                    return ((int)DungeonSize / 4) * 3;
                case ResourceAvailability.Overflowing:
                    return (int)DungeonSize;
                default:
                    return (int)DungeonSize / 2;
            }
        }

        public void SpawnBoss() 
        {
            GameObject Boss;
            if (BossList.Objects.Count != 1)
            {
                //int RandomSelection = Random.Range(0, BossList.AvailableEnemies.Count);
                Boss = Instantiate(BossList.GetRandomObject(), BossRoom.transform);
                //Boss.transform.parent = null;
                Boss.GetComponent<Health>().onEmpty.AddListener(delegate { BossRoom.GetComponentInChildren<HubAreaLoader>().OpenExit(); });
            }
            else
            {
                Boss = Instantiate(BossList.Objects[0], BossRoom.transform);
                //Boss.transform.parent = null;
                Boss.GetComponent<Health>().onEmpty.AddListener(delegate { BossRoom.GetComponentInChildren<HubAreaLoader>().OpenExit(); });
            }
            spawnedEnemies.Add(Boss);
            spawnedBoss = Boss;

        }
        #endregion

    }
}

