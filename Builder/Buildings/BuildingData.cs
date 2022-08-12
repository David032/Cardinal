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
        List<Runic.Entities.Entity> residentEntities;
        [SerializeField]
        int MaxBeds;
        int CurrentlyOccupiedBeds;
        //[SerializeField]
        //int MaxOccupants;
        //int CurrentOccupantCount;

        //Assume suitablity check has already been passed
        public void AddOccupant(Runic.Entities.Entity resident)
        {
            residentEntities.Add(resident);
            CurrentlyOccupiedBeds++;
        }

        public int AvailableBeds()
        {
            return MaxBeds - CurrentlyOccupiedBeds;
        }

        public bool HasSpace()
        {
            if (CurrentlyOccupiedBeds == MaxBeds)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class BuildingData : MonoBehaviour
    {
        public string Name;
        public double buildTime;
        public GameObject Foundation;
        public GameObject Structure;
        public DateTime StartDate;
        public DateTime CompletionDate;
        public bool built = false;
        public Size BuildingSize;
        public OccupancyData BuildingOccupants;


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
            StartDate = System.DateTime.Now;
            CompletionDate = StartDate.AddSeconds(buildTime);
        }

        public void BuildingUpdate()
        {
            if (!built)
            {
                if (DateTime.Compare(DateTime.Now, CompletionDate) > 0)
                {
                    Foundation.SetActive(false);
                    Structure.SetActive(true);
                    built = true;
                }
            }

        }
    }
}

