using System;
using UnityEngine;

namespace Enemy.AttackComponent
{
    public class ProjectileMissile : MonoBehaviour
    {
        private Transform playerTransform;
    
        public void Init(float force, Transform player)
        {
            playerTransform = player;
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            if (rigidbody)
            {
                rigidbody.MovePosition(playerTransform.position);
            }
        }
    }
}