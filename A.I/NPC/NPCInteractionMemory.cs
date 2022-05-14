using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.AI.NPC
{
    public class NPCInteractionMemory : ScriptableObject, IComparable
    {
        public NPCMentalModel interactedWith;
        public float lastTrustValue;
        public float timeAdded;

        public NPCInteractionMemory(NPCMentalModel whoDidITalkTo, float howMuchDoITrustThem, float whenDidILearnAboutThis)
        {
            interactedWith = whoDidITalkTo;
            lastTrustValue = howMuchDoITrustThem;
            timeAdded = whenDidILearnAboutThis;
        }

        public int CompareTo(object obj)
        {
            return timeAdded.CompareTo(obj);
        }
    }
}
