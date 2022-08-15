using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal;
using Cardinal.World;
using Cardinal.Builder;
using System.Linq;
using CardinalSystems.PlayFab;

namespace Cardinal.AI.Entities
{
    [System.Serializable]
    public class Character
    {
        [SerializeField]
        public string CharacterName;
        [SerializeField]
        public GameObject CharacterGO;
    }

    [System.Serializable]
    public class SpawnedCharacter
    {
        public string IdentifierName;
        public EntityData CharacterData;
    }

    public class EntityManager : CardinalSingleton<EntityManager>
    {
        [SerializeField]
        List<Character> npcList;
        [SerializeField]
        List<GameObject> spawnedCharacters;
        // Start is called before the first frame update
        void Start()
        {
            TimeManager.Instance.DayChange.AddListener(PopulationDailyUpdate);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void PopulationDailyUpdate()
        {
            int bedTotal = 0;
            float freeBeds = 0;
            List<ResidentialBuilding> potentialResidences = new();

            var builder = CardinalBuilder.Instance;
            var tiles = builder.GetTiles();
            List<TileData> ResidentialTiles = tiles.Where(x => x.construct
            != null && x.construct.GetComponent<ResidentialBuilding>()).ToList();

            foreach (var item in ResidentialTiles)
            {
                var residenceData = item.construct.GetComponent<ResidentialBuilding>();
                bedTotal += residenceData.BuildingOccupants.MaxBeds;
                freeBeds += residenceData.BuildingOccupants.AvailableBeds();
                potentialResidences.Add(residenceData);
            }

            if (freeBeds == 0)
            {
                return;
            }

            int bedsToBeFilled = Mathf.RoundToInt(freeBeds / 10);
            if (bedsToBeFilled < 1)
            {
                bedsToBeFilled = 1;
            }
            SpawnNewCharacters(bedsToBeFilled, potentialResidences);
        }

        bool CheckForAvailableBeds()
        {
            return true;
        }

        void SpawnNewCharacters(int numberToSpawn,
            List<ResidentialBuilding> residences)
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                int randomCharSelection = Random.Range(0, npcList.Count);
                var selectedNewCharacter = npcList[randomCharSelection];
                var newCharacter =
                    Instantiate(selectedNewCharacter.CharacterGO);
                List<ResidentialBuilding> potentialResidences = residences.Where
                    (x => x.BuildingOccupants.AvailableBeds() != 0).ToList();
                int randomHomeSelection = Random.Range
                    (0, potentialResidences.Count);
                var characterHome = potentialResidences[randomHomeSelection];
                newCharacter.GetComponent<Entity>().Data.HomeLocation =
                    characterHome.AttachedTile.Position;
                newCharacter.GetComponent<Entity>().Identifier =
                    selectedNewCharacter.CharacterName;

                spawnedCharacters.Add(newCharacter);
            }
        }

        void SaveCharacters()
        {
            var dataSystem = DataManager.Instance;
            var saveGame = dataSystem.GetSaveData();
            foreach (var item in spawnedCharacters)
            {
                SpawnedCharacter thisCharacter = new()
                {
                    IdentifierName = item.GetComponent<Entity>().Identifier,
                    CharacterData = item.GetComponent<Entity>().Data
                };
                saveGame.Characters.Add(thisCharacter);
            }
            dataSystem.SaveDataToFile();
        }
    }
}

