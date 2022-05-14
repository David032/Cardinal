using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.AI.World;
using Runic.Dialogue;
using Runic.Items;
using Runic.Entities.Player;
using Runic;

namespace Cardinal.AI.Events
{
    /// <summary>
    /// Action where interacting within range 
    /// & having the required object creates event & a physical world change
    /// </summary>
    public class WorldQuestAction : BaseEvent
    {
        public DialogueObject InteractionMessage;
        public DialogueObject CompletionMessage;
        public Item RequiredItem;
        bool hasCompleted = false;
        [Header("World Objects to change")]
        public GameObject DefaultState;
        public GameObject ChangedState;
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player"))
            {
                return;
            }
            if (other.GetComponent<PlayerControls>().isInteracting && !hasCompleted)
            {
                if (!other.GetComponent<Player>().inventory.Inventory.Contains(RequiredItem))
                {
                    DialogueManager.Instance.ConfigureDialogue(InteractionMessage);
                    DialogueManager.Instance.ShowWindow();
                }
                else
                {
                    other.GetComponent<PlayerControls>().Interact
                        (InteractionTypes.Person);
                    DialogueManager.Instance.ConfigureDialogue(CompletionMessage);
                    DialogueManager.Instance.ShowWindow();
                    CreateEvent();
                    other.GetComponent<Player>().inventory.RemoveItem(RequiredItem);
                    DefaultState.SetActive(false);
                    ChangedState.SetActive(true);
                }

            }
        }
    }
}
