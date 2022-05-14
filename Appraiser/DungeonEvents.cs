using Runic;
using Runic.Entities;
using Runic.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Appraiser
{
    //@event.Name = "Player entered " + gameObject;
    //@event.Time = Time.realtimeSinceStartup.ToString();
    //@event.EventPriority = Cardinal.Priority.Low;
    public class RoomEnteredEvent:EventData
    {
        public RoomType RoomType = RoomType.MainRoom;
        public bool IsFirstEntry = false;
    }
    public class NodeInteractedWith : EventData 
    {
        public NodeType NodeType = NodeType.Chest;
        public List<Item> items;
        public RoomType RoomType = RoomType.MainRoom;
    }
    public class PlayerDeathEvent : EventData 
    {
        public GameObject RoomOfDeath;
        public Entity Slayer;
    }
    public class CompletedDungeonEvent : EventData
    {
    }

    public class EnemyKilledEvent : MultiEventData
    {
        public TypeOfEntity TypeOfEntity;
        public EnemyCategory EnemyCategory;
        public RoomType RoomType;
    }
}

