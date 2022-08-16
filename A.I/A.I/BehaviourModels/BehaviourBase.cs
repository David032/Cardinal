using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Cardinal.AI.Behaviour
{
    public class BehaviourBase : MonoBehaviour
    {
        protected NavMeshAgent entityAgent;
        protected Entities.Entity Entity;
        protected Transform HomeLocation => Entity.Data.Home.transform;
        void Start()
        {
            entityAgent = GetComponent<NavMeshAgent>();
            Entity = GetComponent<Entities.Entity>();
        }


    }
}

