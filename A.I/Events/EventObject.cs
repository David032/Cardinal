using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.Events
{
    public enum ObjectType
    {
        Visual,
        Audio
    }

    public class EventObject : MonoBehaviour
    {
        public ObjectType EventObjectType = ObjectType.Audio;
        public Event LinkedEvent;
        public string EventId;
    }
}
