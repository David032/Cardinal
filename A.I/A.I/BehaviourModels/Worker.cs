using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Behaviour
{
    public class Worker : BehaviourBase
    {
        public Transform workLocation =>
            Entity.Data.Work.WorkStations[0].transform;
        public void GoToWorkLocation()
        {
            entityAgent.SetDestination(workLocation.position);
        }
    }
}

