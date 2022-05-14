using Runic;
using Runic.Items;
using Runic.Rewards;
using Runic.Tasks.Interfaces;
using Runic.Tasks.Jobs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public class CompletedJobsResponse : Response
    {
        public JobBoard JobBoard;
        public string BoardName = "JobBoard";
        public int NumberToGenerate = 3;
        public ItemList DesiredItems;
        public RewardList PotentialRewards;
        public void GenerateMore()
        {
            StateManager.Instance.OnStateChanged
                .AddListener(GenerateMoreJobs);
        }
        void GenerateMoreJobs()
        {
            if (StateManager.Instance.GameState != GetTargetState(ResponseWindow))
            {
                return;
            }
            JobBoard = GameObject.Find(BoardName)
                .GetComponent<JobBoard>();
            for (int i = 0; i < NumberToGenerate; i++)
            {
                int randomJobType = Random.Range(0, 3);
                if (randomJobType == 0)
                {
                    ProgressiveJob ProgJob = ScriptableObject.CreateInstance<ProgressiveJob>();
                    ProgJob.ProgressCriteria = SelectProgressCriteria();
                    ProgJob.TargetValue = Random.Range(3, 7);
                    ProgJob.Reward = PotentialRewards.Entries[Random.Range(0, PotentialRewards.Entries.Count)];
                    ProgJob.Name = ProgJob.ProgressCriteria + ", " +ProgJob.TargetValue;
                    ProgJob.Description = "Achieve" + ProgJob.ProgressCriteria + " " + ProgJob.TargetValue + " times";
                    JobBoard.PotentialTasks.Add(ProgJob);
                }
                else if (randomJobType == 1)
                {
                    KillJob KillJob = ScriptableObject.CreateInstance<KillJob>();
                    KillJob.ProgressCriteria = ProgressCriteria.EnemyKilled;
                    KillJob.TargetValue = Random.Range(3, 7);
                    KillJob.TypeToTrack = SelectEnemyType();
                    KillJob.Reward = PotentialRewards.Entries[Random.Range(0, PotentialRewards.Entries.Count)];
                    KillJob.Name = KillJob.ProgressCriteria + ", " + KillJob.TargetValue;
                    KillJob.Description = "Kill " + KillJob.TargetValue + " " + KillJob.TypeToTrack + " enemies";
                    JobBoard.PotentialTasks.Add(KillJob);
                }
                else if (randomJobType == 2)
                {
                    ItemJob ItemJob = ScriptableObject.CreateInstance<ItemJob>();
                    ItemJob.ProgressCriteria = ProgressCriteria.GatherItem;
                    ItemJob.TargetItem = DesiredItems.Entries[Random.Range(0, DesiredItems.Entries.Count)];
                    ItemJob.TargetValue = Random.Range(3, 7);
                    ItemJob.Reward = PotentialRewards.Entries[Random.Range(0, PotentialRewards.Entries.Count)];
                    ItemJob.Name = ItemJob.ProgressCriteria + ", " + ItemJob.TargetValue;
                    ItemJob.Description = "Collect " + ItemJob.TargetValue + " " + ItemJob.TargetItem;
                    JobBoard.PotentialTasks.Add(ItemJob);
                }
            }
            StateManager.Instance.OnStateChanged.RemoveListener(GenerateMoreJobs);
        }

        ProgressCriteria SelectProgressCriteria()
        {
            int randomProgressCriteria = Random.Range(0, 2);
            if (randomProgressCriteria == 1)
            {
                return ProgressCriteria.DungeonCompletion;
            }
            else
            {
                return ProgressCriteria.RoomEntered;
            }
        }
        TypeOfEntity SelectEnemyType() 
        {
            int randomSel = Random.Range(0, 4);
            if (randomSel == 0)
            {
                return TypeOfEntity.Undead;
            }
            else if (randomSel == 1)
            {
                return TypeOfEntity.Goblin;
            }
            else if (randomSel == 2)
            {
                return TypeOfEntity.Artificial;
            }
            else
            {
                return TypeOfEntity.Human;
            }
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

