using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cardinal.Builder
{
    [System.Serializable]
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

    public class BuildingData : MonoBehaviour
    {
        public double buildTime;
        public GameObject Foundation;
        public GameObject Structure;

        DateTime StartDate;
        DateTime CompletionDate;
        bool built = false;
        [SerializeField]
        Size BuildingSize;

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
            print("StartDate: " + StartDate);
            CompletionDate = StartDate.AddSeconds(buildTime);
            print("CompletionDate: " + CompletionDate);
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

