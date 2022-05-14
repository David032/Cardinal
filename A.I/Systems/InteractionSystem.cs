using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using Cardinal.AI.Events;
using Cardinal.AI.NPC;
using Event = Cardinal.AI.Events.Event;
using Runic.Managers;

namespace Cardinal.AI
{
    public class InteractionSystem : MonoBehaviour
    {
        InteractionSystemController controller;
        //SpawnableController spawnables;
        TimeManager timekeeper;
        EventManager cardinal;

        void Start()
        {
            controller = GameObject.FindGameObjectWithTag("GameController").
                GetComponent<InteractionSystemController>();
            //spawnables = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnableController>();
            timekeeper = GameObject.FindGameObjectWithTag("GameController").
                GetComponent<TimeManager>();
            cardinal = GameObject.FindGameObjectWithTag("GameController").
                GetComponentInChildren<EventManager>();
        }

        //Calculate trust between 2 NPCs
        public float CalculateTrust
            (NPCMentalModel npcA, NPCMentalModel npcB, float moodA)
        {
            int commonLikes = 0;
            int commonDislikes = 0;
            int elementsA = npcA.likes.Capacity + npcA.dislikes.Capacity;
            int elementsB = npcB.likes.Capacity + npcB.dislikes.Capacity;
            float mood = moodA;
            float minimumTrust = controller.minimumTrustLevel;

            npcA.likes.Sort();
            npcA.dislikes.Sort();
            npcB.likes.Sort();
            npcB.dislikes.Sort();

            foreach (Categories item in npcA.likes)
            {
                if (npcB.likes.Contains(item))
                {
                    commonLikes += 1;
                }
            }
            foreach (Categories item in npcA.dislikes)
            {
                if (npcB.dislikes.Contains(item))
                {
                    commonDislikes += 1;
                }
            }

            int commonalities = commonLikes + commonDislikes;
            int elements = elementsA + elementsB;

            float trustVal = mood * (commonalities / elements + minimumTrust);
            return trustVal;
        }

        //Calculate common likes & dislikes between 2 NPCs
        public int CalculateCommonalities(NPCMentalModel npc, Event eventId)
        {
            int likesInEvent = 0;
            int dislikesInEvent = 0;

            foreach (Categories item in npc.likes)
            {
                if (eventId.Categories.Contains(item))
                {
                    likesInEvent += 1;
                }
            }
            foreach (Categories item in npc.dislikes)
            {
                if (eventId.Categories.Contains(item))
                {
                    dislikesInEvent += 1;
                }
            }

            int commonalities = likesInEvent - dislikesInEvent;
            return commonalities;
        }

        //Calculate the decay value of an event
        public float CalculateDecay(NPCEventMemory eventMemory)
        {
            float currentTime = 
                GameObject.FindGameObjectWithTag("GameController")
                .GetComponent<TimeManager>().getRawTime();

            float decayTime = 1 - 
                (currentTime - eventMemory.learntTime) / controller.MEMORYDECAY;

            return decayTime;
        }

        //Calculate the value of an event
        public float CalculateValue(NPCMentalModel npc, Event eventId)
        {
            float fValue = -1;

            if (npc.eventMemories.Capacity == 0)
            {
                fValue = -2;
                return fValue;
            }

            NPCEventMemory thisEventKnowledge;
            foreach (NPCEventMemory item in npc.eventMemories)
            {
                if (item.learntEvent == eventId)
                {
                    thisEventKnowledge = item;
                    fValue = CalculateDecay(thisEventKnowledge) 
                        * (CalculateCommonalities(npc, eventId)) * eventId.weight;
                    thisEventKnowledge.fValue = fValue;
                    return fValue;
                }
            }

            fValue = -3;
            return fValue;
        }

        //Calculate an NPCs opinion of their known events
        public float CalculateOpinion(NPCMentalModel npc)
        {
            float sumOfValueFunctions = 0;
            float opinion = 0;

            foreach (NPCEventMemory item in npc.eventMemories)
            {
                sumOfValueFunctions += item.fValue;
            }
            if (sumOfValueFunctions != 0)
            {
                opinion = Mathf.InverseLerp(-100, 100, sumOfValueFunctions);
            }

            return opinion;
        }

        //Share knowledge of an event
        public void ShareEvent(NPCMentalModel npcA, NPCMentalModel npcB, float mood)
        {
            float trustVal = CalculateTrust(npcA, npcB, mood);
            float commChance = Random.Range(0f, 1f);
            float reliabilityChance = Random.Range(0f, 1f);

            //Prior interaction check
            foreach (NPCInteractionMemory item in npcA.interactedNPCS)
            {
                if (item.interactedWith == npcB)
                {
                    trustVal += item.lastTrustValue / controller.repeatedConnectionRatio;
                }
            }
            //Factional relationship check
            if (npcA.FactionId == npcB.FactionId)
            {
                trustVal += controller.repeatedConnectionRatio / 100;
            }


            if (trustVal > commChance)
            {
                Event eventToShare;
                if (npcA.events.Count == 0 && npcB.events.Count != 0)
                {
                    print(npcA.gameObject + " events 0! Force sharing!");
                    eventToShare = npcB.events[0];
                    npcA.events.Add(eventToShare);
                    npcA.eventMemories.Add(new NPCEventMemory(eventToShare));                        //Instantiate(spawnables.NPCSharingIcon, npcB.transform);
                    return;
                }
                eventToShare = npcA.events[Random.Range(0, npcA.events.Count)];

                if (npcA.memoryReliability > reliabilityChance)
                {
                    if (!npcB.events.Contains(eventToShare))
                    {
                        npcB.events.Add(eventToShare);
                        npcB.eventMemories.Add(new NPCEventMemory(eventToShare));                        //Instantiate(spawnables.NPCSharingIcon, npcB.transform);
                    }
                }
                else if (npcA.memoryReliability < reliabilityChance)
                {
                    print("DEBUG: EVENT MUTATED! " + npcA.gameObject + "'s attempt to tell " + npcB.gameObject + " about " + eventToShare.EventId + " went wrong. " +
                        "The trust value was" + trustVal + " , the comm value " + commChance + " and the mutation value " + reliabilityChance);

                    Event mutatedEvent = cardinal.gameObject.AddComponent<Event>();
                    mutatedEvent.EventId = "Mutated: " + eventToShare.EventId;
                    mutatedEvent.weight = eventToShare.weight;
                    mutatedEvent.memorable = eventToShare.memorable;

                    List<Categories> originalEventsCats = new List<Categories>();
                    foreach (Categories item in eventToShare.Categories)
                    {
                        originalEventsCats.Add(item);
                    }

                    int randomReplacment = Random.Range(0, originalEventsCats.Capacity);
                    originalEventsCats.RemoveAt(randomReplacment);
                    Categories replacment = (Categories)Random.Range(0, Enum.GetNames(typeof(Categories)).Length);
                    originalEventsCats.Add(replacment);

                    mutatedEvent.Categories = originalEventsCats;


                    cardinal.Events.Add(mutatedEvent);
                    if (!npcB.events.Contains(mutatedEvent))
                    {
                        npcB.events.Add(mutatedEvent);
                        npcB.eventMemories.Add(new NPCEventMemory(mutatedEvent));
                        //Instantiate(spawnables.NPCSharingIcon, npcB.transform);
                    }
                }

                AddInteraction(npcA, npcB, trustVal);
            }
            else if (trustVal > commChance)
            {
                print("DEBUG: EVENT FAILED - Trust value was " + trustVal + " and Comm chance was " + commChance);
            }

        }

        private void AddInteraction(NPCMentalModel npcA, NPCMentalModel npcB, float trustVal)
        {
            npcA.interactedNPCS.Add(new NPCInteractionMemory(npcB, trustVal, timekeeper.getRawTime()));
            npcB.interactedNPCS.Add(new NPCInteractionMemory(npcA, trustVal, timekeeper.getRawTime()));
        }
    }
}
