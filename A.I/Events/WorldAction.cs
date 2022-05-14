using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Runic.Dialogue;
using Runic;

namespace Cardinal.AI.Events
{
    /// <summary>
    /// Action where interacting within range creates event
    /// </summary>
    public class WorldAction : BaseEvent
    {
        public DialogueObject CompletionMessage;
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
                DialogueManager.Instance.ConfigureDialogue(CompletionMessage);
                DialogueManager.Instance.ShowWindow();
                CreateEvent();
            }
        }
    }
}



