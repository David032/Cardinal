using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.NPC
{
    public enum options
    {
        Two,
        Three,
        Four,
        Five,
        Six
    }


    public class NPCMultiDialogueController : MonoBehaviour
    {
        public List<NPCDialogue> DialogueOptions;
        public options HowManyOptions = options.Two;


        NPCMentalModel model;

        void Awake()
        {
            UpdateOptions();
            model = GetComponent<NPCMentalModel>();
        }

        private void Update()
        {
            UpdateOptions();
        }

        public void UpdateOptions()
        {
            switch (HowManyOptions)
            {
                case options.Two:
                    twoOptions();
                    break;
                case options.Three:
                    break;
                case options.Four:
                    break;
                case options.Five:
                    break;
                case options.Six:
                    break;
                default:
                    break;
            }
        }

        void twoOptions()
        {
            if (model.opinion < 0)
            {
                DialogueOptions[0].enabled = false;
                DialogueOptions[1].enabled = true;
            }
            else
            {
                DialogueOptions[0].enabled = true;
                DialogueOptions[1].enabled = false;
            }
        }
    }
}
