using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cardinal.Builder
{
    [Serializable]
    public class Size
    {
        [SerializeField]
        int x;
        [SerializeField]
        int y;

        public Size() { x = 0; y = 0; }

        public Size(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    public class OccupancyData
    {
        public List<AI.Entities.Entity> occupants;
        public int MaxOccupants;
        public int CurrentOccupants;
        public bool HasSpace
        {
            get
            {
                if (CurrentOccupants == MaxOccupants)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //Assume suitablity check has already been passed
        public void AddOccupant(AI.Entities.Entity character)
        {
            occupants.Add(character);
            CurrentOccupants++;
        }

        public int OccupancyAvailability()
        {
            return MaxOccupants - CurrentOccupants;
        }

    }

    public class BuildingData : MonoBehaviour
    {
        public string Name;
        public double buildTime;
        public GameObject Foundation;
        public GameObject Structure;

        public bool built = false;
        public Size BuildingSize;
        public TileData AttachedTile;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPlace()
        {

        }

        public void BuildingUpdate()
        {
            if (!built)
            {
                buildTime -= Time.deltaTime;
                if (buildTime <= 0)
                {
                    Foundation.SetActive(false);
                    Structure.SetActive(true);
                    built = true;
                }
            }

        }
    }
}

