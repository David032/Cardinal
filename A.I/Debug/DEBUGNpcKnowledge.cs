using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cardinal.AI.NPC;

namespace Cardinal.AI.Debug
{
    public class DEBUGNpcKnowledge : MonoBehaviour
    {
        public GameObject[] NPCS;
        public TextMeshProUGUI EventPane;

        // Start is called before the first frame update
        void Start()
        {
            NPCS = GameObject.FindGameObjectsWithTag("NPC");
            EventPane.text = "";
            InvokeRepeating("updateKnowledgeLog", 10f, 10f);
        }

        // Update is called once per frame
        void Update()
        {
        }

        void updateKnowledgeLog()
        {
            EventPane.text = "";
            foreach (GameObject npc in NPCS)
            {
                EventPane.text += npc.gameObject.name + " knows that ";
                foreach (NPCEventMemory memory in npc.GetComponent<NPCMentalModel>().eventMemories)
                {
                    EventPane.text += memory.learntEventName;
                    EventPane.text += "(" + memory.learntTime + ")";
                    EventPane.text += ", ";
                }
                EventPane.text += ". This means their opinion of the player is: " + npc.GetComponent<NPCMentalModel>().opinion;
                EventPane.text += "\n";
            }
        }
    }
}