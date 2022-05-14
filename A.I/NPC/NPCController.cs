using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Cardinal.AI.NPC
{
    public enum NPCAnimationState
    {
        None,
        Sitting,
    }

    public class NPCController : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;
        public NPCAnimationState animationState = NPCAnimationState.None;
        Rigidbody rb;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();

            anim.SetInteger("WeaponType_int", 0);
            anim.SetInteger("MeleeType_int", 0);
            //
            switch (animationState)
            {
                case NPCAnimationState.None:
                    break;
                case NPCAnimationState.Sitting:
                    anim.SetInteger("Animation_int", 9);
                    anim.SetFloat("Speed_f", 0);
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if (animationState == NPCAnimationState.None)
            {
                if (agent.velocity != Vector3.zero)
                {
                    anim.SetInteger("Animation_int", 0);
                    anim.SetFloat("Speed_f", agent.speed);
                }
                else
                {
                    anim.SetFloat("Speed_f", 0);
                    anim.SetInteger("Animation_int", 1);
                }
            }

        }
    }
}
