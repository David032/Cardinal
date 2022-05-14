using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Runic;
using Runic.Items;
using Runic.Rewards;
using Runic.Tasks.Interfaces;
using Runic.Tasks.Jobs;
using Runic.Tasks;

namespace Cardinal.Adjustor
{
    public class CompletedQuestResponse : Response
    {
        public JobBoard JobBoard;
        public string BoardName = "JobBoard";
        public int NumberOfEndeavours = 2;
        public int NumberOfJobs = 3;

        public override void Execute()
        {
            StateManager.Instance.OnStateChanged
                .AddListener(GenerateNewQuest);
        }

        public void GenerateNewQuest() 
        {
            if (StateManager.Instance.GameState != GameState.Hub)
            {
                return;
            }
            JobBoard = GameObject.Find(BoardName)
                .GetComponent<JobBoard>();

            Quest NewQuest = ScriptableObject.CreateInstance<Quest>();
            
        }
    }
}

