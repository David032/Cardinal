using Cardinal.Generative.Dungeon;
using Runic.Characteristics.Titles;
using Runic.Entities;
using Runic.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public class ExclusiveBossfightResponse : Response
    {
        public DungeonLoader DungeonLoader;
        public string DungeonName = "DemoDungeon";
        public BaseTitle TitleToGrant; 
        public override void Execute()
        {
            StateManager.Instance.OnStateChanged.AddListener(BossBoost);
        }
        public void BossBoost()
        {
            StartCoroutine(IncreaseBossPower());
        }

        IEnumerator IncreaseBossPower() 
        {
            if (StateManager.Instance.GameState != GetTargetState(ResponseWindow))
            {
                print("State was not dungeon! Returning");
                yield break;
            }
            DungeonGenerator Generator = (DungeonGenerator)DungeonGenerator.Instance;
            yield return new WaitUntil(() => Generator.State == Generative.BuildState.Built);
            if (Generator.spawnedBoss is null)
            {
                print("No Enemies!");
                yield break;
            }
            Generator.spawnedBoss.GetComponent<Entity>().Title = TitleToGrant;
            StateManager.Instance.OnStateChanged.RemoveListener(BossBoost);
        }
    }

}
