using System;
using State.Interface;
using UnityEngine;

namespace StateMachine.Data
{
    [Serializable]
    public class ActorData : MonoBehaviour
    {
        public Animator _animator;
        public IStateable currentState = null;
        public IStateable previousState = null;
        public IStateable nextState = null;
        [HideInInspector]
        public GameObject actorGameObject;
        public ControllerState controllerState;
        public int currentJumpAvailable = 1;
    }
}