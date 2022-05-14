using Runic.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public class DungeonIgnoredResponse : Response
    {
        public DungeonLoader DungeonLoader;
        public string DungeonName = "DemoDungeon";
        public override void Execute()
        {
            StateManager.Instance.OnStateChanged.AddListener(AdjustDungeon);
        }
        public void AdjustDungeon()
        {
            if (StateManager.Instance.GameState != GetTargetState(ResponseWindow))
            {
                print("State was not hub area! Returning");
                return;
            }
            DungeonLoader = GameObject.Find(DungeonName).GetComponent<DungeonLoader>();
            switch (DungeonLoader.RequestedDungeonSize)
            {
                case Generative.SizeOfDungeon.Small:
                    break;
                case Generative.SizeOfDungeon.Medium:
                    DungeonLoader.RequestedDungeonSize = Generative.SizeOfDungeon.Small;
                    break;
                case Generative.SizeOfDungeon.Large:
                    DungeonLoader.RequestedDungeonSize = Generative.SizeOfDungeon.Medium;
                    break;
                default:
                    break;
            }
            StateManager.Instance.OnStateChanged.RemoveListener(AdjustDungeon);
        }
    }

}
