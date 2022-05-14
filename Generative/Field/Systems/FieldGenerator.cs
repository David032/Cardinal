using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cardinal.Generative.Field
{
    public class FieldGenerator : Generator
    {
        [Header("Variables")]
        //Harvestable resources
        public ResourceAvailability ResourceNodeSpread = ResourceAvailability.Regular;
        //Lootable Objects
        public ResourceAvailability LootNodeSpread = ResourceAvailability.Regular;
        //How many enemies to spawn?
        public ResourceAvailability EnemyAmount = ResourceAvailability.Regular;

        [Header("Data")]
        public InterestPlaceList POISource;
        public FieldList PotentialFields;
        public LootableList ResourceNodes;
        public LootableList LootNodes;
        public EnemyList EnemyList;

        [Header("Internals")]
        GameObject field;
        List<GameObject> FieldStructures = new List<GameObject>();
        List<GameObject> SpawnedContent = new List<GameObject>();
        List<GameObject> SpawnedEnemies = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            GenerateField();
            PopulateFieldStructures();
            FillFieldStructures();
        }

        void GenerateField() 
        {
            var AvailableFields = PotentialFields.PotentialFields;
            int RandomSelection = Random.Range(0, AvailableFields.Count);
            field = Instantiate(AvailableFields[RandomSelection]);
        }

        void PopulateFieldStructures() 
        {
            var PlacesToFill = GameObject.FindGameObjectsWithTag("NodeMarker");
            foreach (GameObject item in PlacesToFill)
            {
                FieldNode nodeData = item.GetComponent<FieldNode>();
                List<GameObject> potentialFillers = new List<GameObject>();
                foreach (GameObject filler in POISource.PotentialPOIs)
                {
                    if (filler.GetComponent<InterestNode>().Size <= nodeData.Size)
                    {
                        potentialFillers.Add(filler);
                    }
                }
                int randomSelection = Random.Range(0, potentialFillers.Count);
                GameObject spawnedPOI = Instantiate(potentialFillers[randomSelection], item.transform);
                FieldStructures.Add(spawnedPOI);
            }
        }

        void FillFieldStructures() 
        {
            var Nodes = field.GetComponentsInChildren<FieldResource>();
            List<GameObject> NodeObjects = new List<GameObject>();
            foreach (var item in Nodes)
            {
                NodeObjects.Add(item.gameObject);
            }

            SpawnContent(ResourceNodes, ResourceNodeSpread, 
                MarkerType.Resource, NodeObjects);
            SpawnContent(LootNodes, LootNodeSpread, MarkerType.Resource, NodeObjects);
            SpawnEnemies(EnemyList, EnemyAmount, MarkerType.Enemy, NodeObjects);
        }

        public void SpawnContent(LootableList sourceObjects,
                ResourceAvailability availability, MarkerType type, 
                List<GameObject> nodes)
        {
            GameObject holder = new GameObject();
            holder.name = type.ToString();

            for (int i = 0; i < AvailablityCount(availability, nodes.Count); i++)
            {
                List<GameObject> potentialLocations = new List<GameObject>();
                foreach (var item in nodes)
                {
                    FieldResource thisMarker = item.GetComponent<FieldResource>();
                    if (!thisMarker.isUsed && thisMarker.Type == type)
                    {
                        potentialLocations.Add(item);
                    }
                }

                int RandomLootSelection = Random.Range(0, sourceObjects.LootNodes.Count);
                int RandomPlaceSelection = Random.Range(0, potentialLocations.Count);
                if (potentialLocations.Count == 0)
                {
                    break;
                }
                GameObject LocationToSpawn = potentialLocations[RandomPlaceSelection];
                GameObject LootToSpawn = Instantiate
                    (sourceObjects.LootNodes[RandomLootSelection],
                    LocationToSpawn.transform);
                LocationToSpawn.GetComponent<FieldResource>().isUsed = true;
                LootToSpawn.transform.parent = holder.transform;

                SpawnedContent.Add(LootToSpawn);
            }
        }

        public void SpawnEnemies(EnemyList sourceObjects,
            ResourceAvailability availability, MarkerType type, List<GameObject> nodes)
        {
            GameObject holder = new GameObject();
            holder.name = type.ToString();

            for (int i = 0; i < AvailablityCount(availability, nodes.Count); i++)
            {
                List<GameObject> potentialLocations = new List<GameObject>();

                foreach (var item in nodes)
                {
                    FieldResource thisMarker = item.GetComponent<FieldResource>();
                    if (!thisMarker.isUsed && thisMarker.Type == type)
                    {
                        potentialLocations.Add(item);
                    }
                }

                int RandomLootSelection = Random.Range(0,
                    sourceObjects.AvailableEnemies.Count);
                int RandomPlaceSelection = Random.Range(0, potentialLocations.Count);
                if (potentialLocations.Count == 0)
                {
                    break;
                }
                GameObject LocationToSpawn = potentialLocations[RandomPlaceSelection];
                GameObject EnemyToSpawn = Instantiate
                    (sourceObjects.AvailableEnemies[RandomLootSelection],
                    LocationToSpawn.transform);
                LocationToSpawn.GetComponent<FieldResource>().isUsed = true;
                EnemyToSpawn.transform.parent = holder.transform;

                SpawnedEnemies.Add(EnemyToSpawn);
            }

        }

        int AvailablityCount(ResourceAvailability resource, int numberOfNodes)
        {
            switch (resource)
            {
                case ResourceAvailability.None:
                    return 0;
                case ResourceAvailability.Sparse:
                    return numberOfNodes / 4;
                case ResourceAvailability.Regular:
                    return numberOfNodes / 2;
                case ResourceAvailability.Abundant:
                    return (numberOfNodes / 4) * 3;
                case ResourceAvailability.Overflowing:
                    return numberOfNodes;
                default:
                    return numberOfNodes / 2;
            }
        }


    }
}

