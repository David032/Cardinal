using Cardinal.AI.NPC;
using Runic;
using Runic.Dialogue;
using Runic.Entities.Player;
using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Events
{
    /// <summary>
    /// Actions where the player having something and interacting with an NPC creates an event
    /// </summary>
    /// 
    public class InventoryAction : BaseEvent
    {
        public DialogueObject Request;
        public DialogueObject ThanksMessage;
        public DialogueObject RepeatDialogueOption;
        public Item requiredItem;
        bool isComplete = false;


        // Start is called before the first frame update
        void Start()
        {
            AssignElements();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            if (other.GetComponent<PlayerControls>().isInteracting)
            {
                other.GetComponent<PlayerControls>().Interact
                    (InteractionTypes.Person);
                transform.LookAt(other.transform);
                if (!other.GetComponent<Player>().inventory.Inventory
                    .Contains(requiredItem) && !isComplete)
                {
                    DialogueManager.Instance.ConfigureDialogue(Request);
                    DialogueManager.Instance.ShowWindow();
                }
                else if (!isComplete && other.GetComponent<Player>()
                    .inventory.Inventory.Contains(requiredItem))
                {
                    DialogueManager.Instance.ConfigureDialogue(ThanksMessage);
                    DialogueManager.Instance.ShowWindow();
                    CreateEvent();
                    Event @event = GetComponent<EventObject>().LinkedEvent;
                    MentalModel.events.Add(@event);
                    MentalModel.eventMemories.Add(new NPCEventMemory(@event));
                    isComplete = true;
                    other.GetComponent<Player>().inventory.RemoveItem(requiredItem);
                }
                else
                {
                    DialogueManager.Instance.ConfigureDialogue(RepeatDialogueOption);
                    DialogueManager.Instance.ShowWindow();
                }


            }
        }

    }
}
