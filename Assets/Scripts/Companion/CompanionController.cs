using System;
using UnityEngine;

namespace Companion
{
    public class CompanionController : MonoBehaviour
    {
        public GameObject player;
        public float rangeFromPlayer = 10f;
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            transform.LookAt(player.transform);
            // transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            if (rangeFromPlayer <= Vector2.Distance(transform.position, player.transform.position))
            {
                Vector3 playerPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1f, 0);
                transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime);
            }

        }

        private void SetDiactivate()
        {
            this.gameObject.SetActive(false);
        }
    }
}