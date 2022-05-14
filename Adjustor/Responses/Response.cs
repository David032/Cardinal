using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardinal.Adjustor
{
    public abstract class Response : MonoBehaviour
    {
        public ResponseWindow ResponseWindow;
        public abstract void Execute();

        public GameState GetTargetState(ResponseWindow window) 
        {
            switch (window)
            {
                case ResponseWindow.NextDungeonGeneration:
                    return GameState.Dungeon;
                case ResponseWindow.NextRespawn:
                    return StateManager.Instance.GameState;
                case ResponseWindow.NextReturnToHubArea:
                    return GameState.Hub;
                case ResponseWindow.Immediate:
                    return StateManager.Instance.GameState;
                default:
                    return StateManager.Instance.GameState;
            }
        }
    }
}

