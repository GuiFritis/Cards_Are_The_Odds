using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.StateMachine{
    public class StateMachineBase<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> statesDictionary;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState{
            get {return _currentState;}
        }

        public void Init(){
            statesDictionary = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            statesDictionary.Add(typeEnum, state);
        }    

        public void SwitchState(T state, params object[] objs)
        {
            if(_currentState != null)
            {
                _currentState.OnStateExit();
            }

            _currentState = statesDictionary[state];

            _currentState.OnStateEnter(objs);
        }

        private void Update()
        {
            if(_currentState != null)
            {
                _currentState.OnStateStay();
            }
        }

    }
}
