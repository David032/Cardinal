using Runic.Achievements;
using Runic.Characteristics.Titles;
using Runic.Items;
using Runic.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    public class TaskCompletedEvent : EventData
    {
        public Task Task;
    }
    public class AchievementCompletedEvent : MultiEventData
    {
        public Achievement Achievement;
    }
    public class InventoryChangeEvent : EventData 
    {
        public Item Item;
        public InventoryChange Change;
    }
    public class CurrencyChangeEvent : EventData 
    {
        public int Amount;
        public InventoryChange Change;
    }
    public class TitleEarntEvent : EventData 
    {
        public BaseTitle Title;
    }
}

