using System;
using State.Interface;
using UnityEngine;

namespace StateMachine.Data
{
    [Serializable]
    public class ActorData : MonoBehaviour
    {
        public Animator _animator;
        public IStateable currentState;
        public IStateable previousState;
        public IStateable nextState;
        [HideInInspector]
        public GameObject actorGameObject;
        public ControllerState controllerState;
        public int currentJumpAvailable = 1;
    }
}