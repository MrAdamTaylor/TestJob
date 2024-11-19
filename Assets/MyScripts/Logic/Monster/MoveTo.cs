using System;
using System.Collections;
using UnityEngine;

namespace MyScripts.Logic.Monster
{
    public class MoveTo : MonoBehaviour, IMoveComponent
    {
        private const float REACH_DISTANCE = 0.5f;

        public float Speed { get; private set; }

        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;


        public Action DestinationAction { get; set; }

        private bool _readyToMove;
        private Coroutine _moveRoutine;


        public void Construct(float speed, Transform target = null)
        {
            _speed = speed;
            _target = target;
            _readyToMove = true;
            Speed = speed;
        }

        public void Move()
        {
            if(_target == null || _moveRoutine != null)
                return;

            if (Vector3.Distance(transform.position, _target.position) <= REACH_DISTANCE)
            {
                DestinationAction?.Invoke();
                return;
            }

            _moveRoutine = StartCoroutine(MakeStep());
        }

        public void StopMove()
        {
            if(_moveRoutine == null)
                return;
            StopCoroutine(_moveRoutine);
        }

        public Vector3 GetDirection()
        {
            return (_target.transform.position - transform.position).normalized;
        }

        private IEnumerator MakeStep()
        {
            while (_readyToMove)
            {
                yield return true;
                Step();
            }
        }

        private void Step()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.Translate(direction * (_speed * Time.deltaTime));
        }
    }
}