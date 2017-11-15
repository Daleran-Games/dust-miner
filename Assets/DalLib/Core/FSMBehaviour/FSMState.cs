using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaleranGames
{
    public abstract class FSMState : MonoBehaviour
    {

        [SerializeField]
        protected FSMBehavior fsm;
        public virtual  FSMBehavior FSM { get { return fsm; } } 

        public Action<FSMState> StateEnabled;
        public Action<FSMState> StateDisabled;

        public abstract bool CanTransitionTo(FSMState state);

        protected virtual void Awake()
        {
            enabled = false;
        }

        protected virtual void OnEnable()
        {
            if (StateEnabled != null)
                StateEnabled(this);
        }

        protected virtual void OnDisable()
        {
            if (StateDisabled != null)
                StateDisabled(this);
        }


    }
}
