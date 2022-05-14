using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.NPC
{
    public enum Type
    {
        Blocker,
    }
    public class NPCOpinionDecision : MonoBehaviour
    {
        NPCMentalModel mentalModel;
        public Type decisionType = Type.Blocker;

        public GameObject BlockerObject;

        void Start()
        {
            mentalModel = GetComponent<NPCMentalModel>();
        }

        // Update is called once per frame
        void Update()
        {
            if (decisionType == Type.Blocker)
            {
                if (mentalModel.opinion > 0)
                {
                    BlockerObject.SetActive(false);
                }
                if (mentalModel.opinion < 0)
                {
                    BlockerObject.SetActive(true);
                }
            }
        }
    }
}
