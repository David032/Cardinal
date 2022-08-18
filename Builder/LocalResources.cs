using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Cardinal.Builder
{
    [System.Serializable]
    public class Resource
    {
        [SerializeField]
        string Name;
        [SerializeField]
        int _amount;
        [SerializeField]
        int _capacity;
        public int Amount
        {
            get => _amount;
            set => Mathf.Clamp(_amount += value, 0, _capacity);
        }
        public int Capacity
        {
            get => _capacity;
            set => _capacity += value;
        }

        public Resource(string name)
        {
            Name = name;
            _amount = 0;
            _capacity = 100;
        }

        public Resource(string name, int amount, int capacity)
        {
            Name = name;
            _amount = amount;
            _capacity = capacity;
        }
    }
    [System.Serializable]
    public class LocalResourceData
    {
        public List<Resource> Resources;
    }
    public class LocalResources : MonoBehaviour
    {
        public List<Resource> Resources;

        // Start is called before the first frame update
        void Start()
        {
            if (CardinalSystems.PlayFab.PlayerManager.Instance.IsSignedIn())
            {
                Destroy(this);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void SaveLocalResources()
        {
#if UNITY_EDITOR_64
            string dataPath = Directory.GetCurrentDirectory() + "/Assets";
#endif
            LocalResourceData resourceSaveData = new();
            foreach (var item in Resources)
            {
                resourceSaveData.Resources.Add(item);
            }

            string _data = JsonUtility.ToJson(resourceSaveData, true);
            File.WriteAllText(dataPath + "/LocalResources.json", _data);
        }

        void LoadLocalResources()
        {
#if UNITY_EDITOR_64
            string dataPath = Directory.GetCurrentDirectory() + "/Assets";
#endif
            string rawData = File.ReadAllText(dataPath + "/LocalResources.json");
            var saveData = JsonUtility.FromJson<LocalResourceData>(rawData);
            foreach (var item in saveData.Resources)
            {
                Resources.Add(item);
            }
        }
    }
}

