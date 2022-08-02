using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Builder
{
    public class CardinalBuilder : CardinalSingleton<CardinalBuilder>
    {
        [SerializeField]
        List<GameObject> tiles = new();
        [SerializeField]
        GameObject WoodFoundations;
        [SerializeField]
        GameObject StoneFoundations;

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

        public void PlaceWoodFoundation(TileData tile)
        {
            //needs lock to prevent overplacing
            var newFloor = Instantiate(WoodFoundations);
            tile.construct = newFloor;
            newFloor.transform.parent = tile.transform;
            newFloor.transform.position = Vector3.zero;
        }
    }
}

