using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal.AI.Events;
using Event = Cardinal.AI.Events.Event;
using Runic.Managers;

namespace Cardinal.AI.NPC
{
    public class NPCEventMemory : ScriptableObject
    {
        public Event learntEvent;
        public float learntTime;
        public string learntEventName;
        public float fValue;


        public NPCEventMemory(Event eventToAdd)
        {
            learntTime = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeManager>().getRawTime();
            learntEvent = eventToAdd;
            learntEventName = eventToAdd.EventId;
        }
    }
}
