using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

namespace Cardinal.Builder
{
    [System.Serializable]
    public class Region
    {
        public List<RegionFragment> Tiles = new();
    }
    [System.Serializable]
    public class RegionFragment
    {
        public int xPos;
        public int yPos;
        public TileData tileData;
        public BuildingData buildingData;

        public bool SetLocation(int row, int column)
        {
            try
            {
                xPos = column;
                yPos = row;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool SetTileData(TileData tileToSave)
        {
            try
            {
                tileData = tileToSave;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool SetBuildingData(BuildingData buildingToSave)
        {
            try
            {
                buildingData = buildingToSave;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }


    public class AreaManager : CardinalSingleton<AreaManager>
    {
        [SerializeField]
        string _data;
        private string dataPath;
        // Start is called before the first frame update
        void Start()
        {
#if UNITY_EDITOR_64
            dataPath = Directory.GetCurrentDirectory() + "/Assets";
#endif
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SaveData()
        {
            Region saveData = new();
            foreach (var item in CardinalBuilder.Instance.GetTiles())
            {
                RegionFragment fragment = new();
                fragment.SetLocation(item.xPos, item.yPos);
                fragment.SetTileData(item);
                if (item.construct != null)
                {
                    fragment.SetBuildingData
                        (item.construct.GetComponent<BuildingData>());
                }
                saveData.Tiles.Add(fragment);
            }

            _data = JsonUtility.ToJson(saveData);
            //_data = JsonConvert.SerializeObject(saveData);
            File.WriteAllText(dataPath + ".json", _data);

        }
    }
}

