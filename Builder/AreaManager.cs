using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;
using CardinalSystems.PlayFab;

namespace Cardinal.Builder
{


    public class AreaManager : CardinalSingleton<AreaManager>
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SaveAreaData()
        {
            var saveData = DataManager.Instance.CreateNewSave();
            foreach (var item in CardinalBuilder.Instance.GetTiles())
            {
                RegionFragment fragment = new();
                fragment.SetLocation(item.xPos, item.yPos);
                if (item.construct != null)
                {
                    var data = item.construct.GetComponent<BuildingData>();
                    fragment.buildingName = data.Name;
                    fragment.startDate = data.StartDate;
                    fragment.completionDate = data.CompletionDate;
                }
                saveData.Tiles.Add(fragment);
            }
            DataManager.Instance.SaveDataToFile();
        }

        public void LoadAreaData()
        {
            var saveData = DataManager.Instance.LoadDataFromFile();
            var cardinalBuilder = CardinalBuilder.Instance;
            var worldTiles = cardinalBuilder.GetTiles();
            var buildingsSource = cardinalBuilder.BuildingsList();

            foreach (var item in saveData.Tiles)
            {
                var selectedTile = worldTiles.Where(x => x.xPos == item.xPos
                && x.yPos == item.yPos).FirstOrDefault();
                cardinalBuilder.SelectedObject = selectedTile.gameObject;
                if (item.buildingName != null ||
                    item.buildingName != string.Empty)
                {
                    _ = cardinalBuilder.PlaceBuilding(item.buildingName);
                    var tileData = cardinalBuilder.SelectedObject.GetComponent<TileData>();
                    var buildingData = tileData.construct.GetComponent<BuildingData>();

                    buildingData.StartDate = item.startDate;
                    buildingData.CompletionDate = item.completionDate;
                }
            }
        }
    }
}

