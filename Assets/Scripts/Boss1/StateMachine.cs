using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossLogic { 
    public class StateMachine<T>
    {
        public State<T> currentState { get;  set; }
        public T Owner;

        public StateMachine(T _o)
        {
            Owner = _o;
            currentState = null;
        }

        public void ChangeState(State<T> nextState)
        {

            if (currentState != null)  currentState.ExitState(Owner);
            currentState = nextState;
            currentState.EnterState(Owner);

        }

        public void Update()
        {
            if (currentState != null) currentState.UpdateState(Owner);
 
        }
        
    }

    public abstract class State<T>
    {
        public abstract void EnterState(T _owner);
        public abstract void ExitState(T _owner);
        public abstract void UpdateState(T _owner);

    }
}