using Cardinal.AI.NPC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Cardinal.AI.Events
{
    //For enabling NPCs to pass through/change opinion of an area when they learn about something
    public class EventBlocker : MonoBehaviour
    {
        public bool hasKnowledgeOfEvent = false;
        public int requiredEventId;
        public int indexOfAreaToChange = 5;
        public float costToSetItTo = 1;

        NPCMentalModel MentalModel;

        // Start is called before the first frame update
        void Start()
        {
            MentalModel = GetComponent<NPCMentalModel>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckKnowledge();
            if (hasKnowledgeOfEvent)
            {
                GetComponent<NavMeshAgent>().SetAreaCost(5, 1);
            }
        }

        void CheckKnowledge()
        {
            foreach (NPCEventMemory item in MentalModel.eventMemories)
            {
                if (item.learntEvent.refId == requiredEventId)
                {
                    hasKnowledgeOfEvent = true;
                }
            }
        }
    }
}
