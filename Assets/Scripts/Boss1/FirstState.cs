using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossLogic
{
    public class FirstState : State<AI>
    {
        private static FirstState _instance;
        private FirstState()
        {
            if (_instance != null) return;
            _instance = this;

        }

        public static FirstState Instance
        {
            get
            {
                if(_instance == null)
                {
                    new FirstState();
                }
                return _instance;
            }

        }

        public override void EnterState(AI _owner)
        {
            Debug.Log("I'm in First State");
        }

        public override void ExitState(AI _owner)
        {
            Debug.Log("Exiting First State");
        }

        public override void UpdateState(AI _owner)
        {
            if(_owner.switchState)
            {
                _owner.stateMachine.ChangeState(SecondState.Instance);
                _owner.switchState = !_owner.switchState;

            }
            
        }
    }
}