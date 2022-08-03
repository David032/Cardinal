using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Cardinal.Builder
{
    [System.Serializable]
    public class Building
    {
        [SerializeField]
        public string BuildingName;
        [SerializeField]
        public GameObject Structure;

    }

    public class CardinalBuilder : CardinalSingleton<CardinalBuilder>
    {
        [SerializeField]
        List<Building> Buildings;
        [SerializeField]
        List<GameObject> tiles = new();
        public int AreaSize = 5;

        GameObject _selectedTile;
        public GameObject SelectedTile
        {
            get => _selectedTile;
            set => _selectedTile = value;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public bool PlaceBuilding(string Name)
        {
            var tile = _selectedTile.GetComponent<TileData>();
            if (tile.construct == null)
            {
                var building = Buildings.Where
                    (x => x.BuildingName == Name).FirstOrDefault();
                if (building is null)
                {
                    return false;
                }
                var newBuild = Instantiate(building.Structure);
                tile.construct = newBuild;
                newBuild.transform.parent = tile.transform;
                newBuild.transform.localPosition = new Vector3(5, 0, 0);
                UpdateTiles();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<GameObject> GetTileObjects()
        {
            return tiles;
        }
        public List<TileData> GetTiles()
        {
            List<TileData> tileDatas = new();
            foreach (var item in tiles)
            {
                tileDatas.Add(item.GetComponent<TileData>());
            }
            return tileDatas;
        }

        void UpdateTiles()
        {
            foreach (var item in GetTiles())
            {
                item.UpdateAdjacentTiles();
            }
        }
    }
}

