using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.NPC
{
    public enum AnimalState
    {
        Idle,
        Sitting,
        Walking
    }

    public class NPCAnimalController : MonoBehaviour
    {
        public AnimalState State = AnimalState.Idle;
        Animator anim;

        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            switch (State)
            {
                case AnimalState.Idle:
                    anim.SetFloat("Speed_f", 0);
                    break;
                case AnimalState.Sitting:
                    break;
                case AnimalState.Walking:
                    break;
                default:
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
