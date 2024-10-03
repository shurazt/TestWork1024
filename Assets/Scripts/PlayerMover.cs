using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Test1024
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] float _speed = 10f;
        [SerializeField] private DynamicJoystick _stick;
        [SerializeField] private NavMeshAgent _agent;
        public bool PlayerStop { get; private set; } = true;

        private const float speedRot = 0.3f;
        private OreStack _stack;

        private void Start()
        {
            _stack = GetComponent<OreStack>();
        }

        private void FixedUpdate()
        {
            if (_stick.Direction == Vector2.zero)
            {
                PlayerStop = true;
                return;
            }

            Move();
        }

        private void Move()
        {
            PlayerStop = false;
            Vector3 direction = Vector3.forward * _stick.Vertical + Vector3.right * _stick.Horizontal;
            
            Rotate(direction);
            
            _agent.Move(direction * _speed * Time.fixedDeltaTime);
            if(_stack!=null) _stack.DrawStack();
        }

        private void Rotate(Vector3 direction)
        {
            Quaternion targetDirection = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetDirection, speedRot);
        }
    }
}
