using Runic.Items;
using Runic.Rewards;
using Runic.Tasks;
using Runic.Tasks.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public class CompletedEndeavoursResponse : Response
    {
        public JobBoard JobBoard;
        public string BoardName = "JobBoard";
        public int NumberToGenerate = 3;
        public ItemList DesiredItems;
        public RewardList PotentialRewards;
        public void GenerateMore() 
        {
            StateManager.Instance.OnStateChanged
                .AddListener(GenerateMoreEndeavours);
        }
        void GenerateMoreEndeavours() 
        {
            if (StateManager.Instance.GameState != GetTargetState(ResponseWindow))
            {
                return;
            }
            JobBoard = GameObject.Find(BoardName)
                .GetComponent<JobBoard>();
            for (int i = 0; i < NumberToGenerate; i++)
            {
                ItemEndeavour endeavour = ScriptableObject.CreateInstance<ItemEndeavour>();
                endeavour.DesiredItem = DesiredItems.Entries[Random.Range(0, DesiredItems.Entries.Count)];
                endeavour.Reward = PotentialRewards.Entries[Random.Range(0, PotentialRewards.Entries.Count)];
                endeavour.Name = "Find " + endeavour.DesiredItem.ToString();
                endeavour.Description = "Find a " + endeavour.DesiredItem.ToString() + " to get " + endeavour.Reward.ToString();
                JobBoard.PotentialTasks.Add(endeavour);
            }
            StateManager.Instance.OnStateChanged.RemoveListener(GenerateMoreEndeavours);
        }


        public void GenerateLess() 
        {
        
        }
        void ReduceAvailableEndeavours() 
        {
            
        }
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}

