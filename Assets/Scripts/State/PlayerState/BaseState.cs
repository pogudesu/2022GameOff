using State.Interface;
using StateMachine.Data;
using UnityEngine;

namespace StateMachine.PlayerState
{
    public abstract class BaseState : IStateable
    {
        protected ActorData _actor;

        protected virtual bool IsCurrentStateThis()
        {
            if (_actor.currentState != this)
            {
                Exit(_actor);
                return false;
            }

            return true;
        }

        public virtual void Enter(object obj)
        {
        }
        public virtual void Update(object obj){}

        public virtual void Exit(object obj)
        {
            _actor.previousState = this;
        }

        public virtual void ChangeState(IStateable state)
        {
            Exit(_actor);
            _actor.currentState = state;
            state.Enter(_actor);
        }

        protected virtual void RunAnimation(string str, bool active)
        {
            if(_actor?._animator == null) Debug.LogError("Animator is null on Actor " + this);
            if (_actor?._animator == null) return;
            _actor._animator.SetBool(str, active);
        }
    
        protected virtual bool SetActor(object obj)
        {
            if (_actor == null)
            {
                _actor = (ActorData)obj;
                if (_actor == null) return false;
            }

            return true;
        }
    }
}