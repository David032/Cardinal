using Runic.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cardinal;

namespace Cardinal.Adjustor
{
    public class DungeonProgressionResponse : Response
    {
        public DungeonLoader DungeonLoader;
        public string DungeonName = "DemoDungeon";
        public override void Execute()
        {
            StateManager.Instance.OnStateChanged
                .AddListener(IncreaseDensityOfDungeon);
        }

        public void IncreaseDensityOfDungeon() 
        {
            if (StateManager.Instance.GameState != GetTargetState(ResponseWindow))
            {
                return;
            }
            DungeonLoader = GameObject.Find(DungeonName)
                .GetComponent<DungeonLoader>();
            int randomSelection = Random.Range(0, 2);
            if (randomSelection == 0)
            {
                if (DungeonLoader.RequestedLootNodeSpread != 
                    Generative.ResourceAvailability.Overflowing)
                {
                    DungeonLoader.RequestedLootNodeSpread++;
                }
                print(DungeonName + "'s requested loot spread is now "
                    + DungeonLoader.RequestedLootNodeSpread);
            }
            else
            {
                if (DungeonLoader.RequestedResourceNodeSpread != 
                    Generative.ResourceAvailability.Overflowing)
                {
                    DungeonLoader.RequestedResourceNodeSpread++;
                }
                print(DungeonName + "'s requested resource spread is now "
                        + DungeonLoader.RequestedResourceNodeSpread);
            }
            StateManager.Instance.OnStateChanged
                .RemoveListener(IncreaseDensityOfDungeon);
        }
    }

}
