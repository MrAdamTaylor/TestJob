using System;
using MyScripts.Logic;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class MoveDirection : MonoBehaviour, IMoveComponent
    {
        private const float XDIRECTION = 1f; 
        private const float YDIRECTION = 0f; 
        private const float ZDIRECTION = 1f;

        private float _speed;

        private Vector3 _vec;
        
        public void Construct(float speed)
        {
            _speed = speed;
            _vec = new Vector3(XDIRECTION, YDIRECTION, ZDIRECTION);
        }

        public Action DestinationAction { get; set; }

        public void Move()
        {
            transform.Translate(_vec*_speed*Time.deltaTime);
        }

        public void StopMove()
        {
            
        }
    }
}