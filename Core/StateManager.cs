using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace Cardinal
{
    public class StateManager : CardinalSingleton<StateManager>
    {
        public GameState GameState = GameState.Hub;
        public UnityEvent OnStateChanged;
        public void ChangeState(GameState newState) 
        {
            GameState = newState;
            print("State changed to: " + GameState);
            OnStateChanged.Invoke();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }
    }

}
