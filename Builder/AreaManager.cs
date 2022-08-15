using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;
using System.Linq;
using CardinalSystems.PlayFab;
using Unity.AI.Navigation;

namespace Cardinal.Builder
{
    public class AreaManager : CardinalSingleton<AreaManager>
    {
        NavMeshSurface navMeshController;

        // Start is called before the first frame update
        void Start()
        {
            navMeshController = GetComponent<NavMeshSurface>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SaveAreaData()
        {
            var saveData = DataManager.Instance.GetSaveData();
            foreach (var item in CardinalBuilder.Instance.GetTiles())
            {
                RegionFragment fragment = new();
                fragment.SetLocation(item.yPos, item.xPos);
                if (item.construct != null)
                {
                    var data = item.construct.GetComponent<BuildingData>();
                    fragment.buildingName = data.Name;
                    fragment.BuildTime = data.buildTime;

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
                    item.buildingName != string.Empty ||
                    item.buildingName != "")
                {
                    _ = cardinalBuilder.PlaceBuilding(item.buildingName);
                    var tileData =
                        cardinalBuilder.SelectedObject.GetComponent<TileData>();

                    try
                    {
                        var buildingData =
                            tileData.construct.GetComponent<BuildingData>();
                        buildingData.buildTime = item.BuildTime;
                    }
                    catch (Exception)
                    {
                        print("Oi!");
                    }

                }
            }
        }

        public void BuildNavMesh()
        {
            navMeshController.BuildNavMesh();
        }
    }
}

