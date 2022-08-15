using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Builder
{
    [Serializable]
    public class OccupancyData
    {
        public List<AI.Entities.Entity> residents;
        public int MaxBeds;
        public int CurrentlyOccupiedBeds;
        public bool HasSpace
        {
            get
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

        //Assume suitablity check has already been passed
        public void AddOccupant(AI.Entities.Entity resident)
        {
            residents.Add(resident);
            CurrentlyOccupiedBeds++;
        }

        public int AvailableBeds()
        {
            return MaxBeds - CurrentlyOccupiedBeds;
        }

    }

    public class ResidentialBuilding : BuildingData
    {
        public OccupancyData BuildingOccupants;
        public List<GameObject> Beds;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

