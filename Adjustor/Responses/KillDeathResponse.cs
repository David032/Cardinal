using Cardinal.Generative.Dungeon;
using Runic.Characteristics;
using Runic.Characteristics.Titles;
using Runic.Entities;
using Runic.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public class KillDeathResponse : Response
    {
        public BaseTitle Buff;
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteIncrease() 
        {
            IncreaseEnemyDifficulty();
        }

        void IncreaseEnemyDifficulty() 
        {
            DungeonGenerator generator = (DungeonGenerator)DungeonGenerator.Instance;
            foreach (GameObject item in generator.spawnedEnemies)
            {
                item.GetComponent<Entity>().Title = Buff;
            }
        }
        public void ExecuteDecrease() 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().onEmpty.AddListener(BuffPlayer);
        }

        void BuffPlayer() 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Title = Buff;
        }
    }

}
