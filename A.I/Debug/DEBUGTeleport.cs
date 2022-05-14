using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Cardinal.AI.Debug
{
    public class DEBUGTeleport : MonoBehaviour
    {
        public GameObject Target;

        GameObject player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public void Teleport()
        {
            player.GetComponent<NavMeshAgent>().Warp(Target.transform.position);
            player.transform.position = Target.transform.position;
        }
    }
}
