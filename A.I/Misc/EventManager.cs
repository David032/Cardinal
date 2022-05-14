using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.AI.Events;
using Event = Cardinal.AI.Events.Event;
using Runic;

namespace Cardinal.AI
{
    public class EventManager : MonoSingleton<EventManager>
    {
        public List<Event> Events;

        // Update is called once per frame
        void Update()
        {
            foreach (Event item in gameObject.GetComponents<Event>())
            {
                if (!Events.Contains(item))
                {
                    Events.Add(item);
                    item.name = item.EventId;
                }
            }
        }
    }
}
