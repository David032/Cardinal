using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.NPC
{
    [RequireComponent(typeof(SphereCollider))]
    public class NPCHearing : Sense
    {
        SphereCollider hearingRange;

        public bool amInteracting = false;
        public bool canInteract = true;
        protected override void Initialize()
        {
            hearingRange = GetComponent<SphereCollider>();
        }
        protected override void UpdateSense()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            if (canInteract)
            {
                canInteract = true;
                if (!amInteracting)
                {
                    if (other.gameObject.tag == "NPC")
                    {
                        if (other.gameObject.GetComponent<NPCMentalModel>()
                            .events.Capacity != 0)
                        {
                            amInteracting = true;
                            GetComponent<InteractionSystem>()
                                .ShareEvent(MentalModel, 
                                other.GetComponent<NPCMentalModel>(), 
                                MentalModel.mood);
                            amInteracting = false;
                        }
                    }
                }
            }
        }
    }
}


