using Cardinal.AI.NPC;
using Runic;
using Runic.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.World
{
    public class OpinionDialogueController : MonoBehaviour
    {
        public DialogueObject DefaultDialogue;
        public DialogueObject AlternativeDialogue;
        public Requirement RequiredState = Requirement.GreaterThan;
        [Range(0f, 1f)]
        public float OpinionLevelRequired = 0.5f;
        NPCMentalModel ControllerNPC;


        private void Start()
        {
            ControllerNPC = GetComponent<NPCMentalModel>();
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
                switch (RequiredState)
                {
                    case Requirement.GreaterThan:
                        if (OpinionLevelRequired <= ControllerNPC.opinion)
                        {
                            DialogueManager.Instance.
                                ConfigureDialogue(AlternativeDialogue);
                            DialogueManager.Instance.ShowWindow();
                        }
                        else
                        {
                            DialogueManager.Instance.
                                ConfigureDialogue(DefaultDialogue);
                            DialogueManager.Instance.ShowWindow();
                        }
                        break;
                    case Requirement.LessThan:
                        if (OpinionLevelRequired >= ControllerNPC.opinion)
                        {
                            DialogueManager.Instance.
                                ConfigureDialogue(AlternativeDialogue);
                            DialogueManager.Instance.ShowWindow();
                        }
                        else
                        {
                            DialogueManager.Instance.
                                ConfigureDialogue(DefaultDialogue);
                            DialogueManager.Instance.ShowWindow();
                        }
                        break;
                    default:
                        break;
                }

            }
        }
    }
}

