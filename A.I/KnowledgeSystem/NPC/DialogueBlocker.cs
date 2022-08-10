using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.NPC
{
    public class DialogueBlocker : MonoBehaviour
    {
        public bool hasKnowledgeOfEvent = false;
        public int requiredEventId;
        public string altDialogue = "";

        public NPCDialogue dialogue;

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
                dialogue.dialogue = altDialogue;
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
