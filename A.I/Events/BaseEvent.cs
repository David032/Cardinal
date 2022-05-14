using Cardinal.AI.NPC;
using Runic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// Base components required for event creation
/// </summary>

namespace Cardinal.AI.Events
{
    public class BaseEvent : MonoBehaviour
    {
        protected EventManager Manager;
        protected GameObject player;
        protected GameObject dialogueWindow;
        protected GameObject dialogueSpot;

        public string EventName = "";
        public List<Categories> EventCategories;
        public bool IsEventUnforgetabble = false;
        [Range(0f, 1f)]
        public float EventWeight = 0.75f;
        public int refremceId = 0;
        public ObjectType EventType = ObjectType.Visual;

        protected GameObject spawnedDialogue;
        protected NPCMentalModel MentalModel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CreateEvent()
        {
            if (!gameObject.GetComponent<EventObject>())
            {
                Event thisEventEntry = EventManager.Instance.gameObject
                    .AddComponent<Event>();
                if (refremceId != 0)
                {
                    thisEventEntry.CreateEvent
                        (EventName, EventCategories, EventWeight, 
                        IsEventUnforgetabble, refremceId);

                }
                else
                {
                    thisEventEntry.CreateEvent(EventName, EventCategories, 
                        EventWeight, IsEventUnforgetabble);

                }
                EventManager.Instance.Events.Add(thisEventEntry);
                EventObject thisEvent = this.gameObject.AddComponent<EventObject>();
                thisEvent.EventId = EventName;
                thisEvent.EventObjectType = EventType;
                thisEvent.LinkedEvent = thisEventEntry;
            }
        }

        public void CreateEvent(ObjectType type)
        {
            if (!gameObject.GetComponent<EventObject>())
            {
                Event thisEventEntry = Manager.gameObject.AddComponent<Event>();
                if (refremceId != 0)
                {
                    thisEventEntry.CreateEvent(EventName, EventCategories, EventWeight, IsEventUnforgetabble, refremceId);

                }
                else
                {
                    thisEventEntry.CreateEvent(EventName, EventCategories, EventWeight, IsEventUnforgetabble);

                }
                Manager.Events.Add(thisEventEntry);
                EventObject thisEvent = this.gameObject.AddComponent<EventObject>();
                thisEvent.EventId = EventName;
                thisEvent.EventObjectType = type;
                thisEvent.LinkedEvent = thisEventEntry;
            }
        }

        public void AssignElements()
        {
            Manager = EventManager.Instance;
            player = GameObject.FindGameObjectWithTag("Player");
            MentalModel = GetComponent<NPCMentalModel>();
        }
    }
}
