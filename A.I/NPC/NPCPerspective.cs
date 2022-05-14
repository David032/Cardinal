using Cardinal.AI.Events;
using Shapes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Event = Cardinal.AI.Events.Event;

namespace Cardinal.AI.NPC
{
    public class NPCPerspective : Sense
    {
        public int fieldOfView = 25;
        public int viewDistance = 25;
        protected override void UpdateSense()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward,
                out hit, viewDistance, ~(1 << 8), QueryTriggerInteraction.Ignore))
            {
                if (hit.collider.GetComponent<EventObject>())
                {
                    //UnityEngine.Debug.DrawLine(transform.position, hit.transform.position);
                    if (hit.collider.GetComponent<EventObject>().
                        EventObjectType == ObjectType.Visual)
                    {
                        if (!MentalModel.events.Contains(hit.collider
                            .GetComponent<EventObject>().LinkedEvent))
                        {
                            Event eventBeingAdded = hit.collider
                                .GetComponent<EventObject>().LinkedEvent;
                            MentalModel.events.Add(eventBeingAdded);

                            MentalModel.eventMemories.Add(new NPCEventMemory
                                (hit.collider.GetComponent<EventObject>().LinkedEvent));
                        }
                    }
                    else
                    {
                        //print("That's not a visual event!");
                    }
                }
                else
                {
                    //print("That's not an event @" + gameObject.name);
                }
            }
        }
        void OnDrawGizmos()
        {
            Vector3 frontRayPoint = transform.position + (transform.forward *
            viewDistance);
            //UnityEngine.Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        }
    }

}