using Cardinal.Generative;
using Runic.Entities;
using Runic.Items;
using Runic.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{    
    //@event.Name = "Player entered " + gameObject;
    //@event.Time = Time.realtimeSinceStartup.ToString();
    //@event.EventPriority = Cardinal.Priority.Low;
    public abstract class NPCEvent:MultiEventData
    {
        public Entity NPC;
    }
    public class NPCInteractionEvent : NPCEvent
    {

    }
    public class NPCTradeEvent : NPCEvent 
    {
        public InventoryChangeData ChangeData;
    }

    [System.Serializable]
    public class InventoryChangeData 
    {
        //The change that's occuring
        public InventoryChange Change;
        //What's changing hands
        public Item Item;
        //How much is it work
        public int Amount;

        public InventoryChangeData(InventoryChange WhatSortOfChangeOccured, Item WhatChangedHands, int WhatWasItsValue) 
        {
            Change = WhatSortOfChangeOccured;
            Item = WhatChangedHands;
            Amount = WhatWasItsValue;
        }
    }
    public class BuildingEnteredEvent:EventData
    {
        public string BuildingName;
    }
    public class CollectibleFoundEvent : EventData 
    {
        public Item Item;
        public int SeriesNumber;
    }
    public class TaskTakenEvent : EventData
    {
        public Task Task;
    }

    public class DungeonEnteredEvent:EventData
    {
        public SizeOfDungeon Size;
        public TypeOfDungeon Type;
        public bool RequiresBoss;
        public int PuzzleRooms;
        public int SpecialRooms;
        public ResourceAvailability ResourceNodeSpread;
        public ResourceAvailability LootNodeSpread;
        public ResourceAvailability EnemyAmount;
    }
}
