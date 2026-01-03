using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MKUtil.SM
{
    [SerializeField]
    public abstract class IState
    {
        // States. Implement it for use with StateMachine<T>.

        internal virtual void Enter() { }
        internal virtual void Execute() { }
        internal virtual void Exit() { }
    }

    public interface IStateMachine<T>
    {
        // Doesn't necessarily take IState.

        T CurrentState { get; set; }
        void Execute();
        void ChangeState(T newState);
    }

    public class StateMachine<T> : IStateMachine<T> where T : IState
    {

        // Simple state machine. Takes something that implements IState.
        // Call Execute() to run it.

        public T CurrentState { get; set; }

        public void Execute()
        {
            if (CurrentState != null)
            {
                OnExecute();
            }
        }
        public virtual void ChangeState(T newState)
        {
            if (CurrentState != null)
            {
                OnExit();
            }
            CurrentState = newState;
            OnEnter();
        }

        public virtual void OnExecute()
        {
            CurrentState.Execute();
        }
        public virtual void OnExit()
        {
            CurrentState.Exit();
        }
        public virtual void OnEnter()
        {
            CurrentState.Enter();
        }

    }

}