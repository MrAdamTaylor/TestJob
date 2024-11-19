using System;
using UnityEngine;

namespace MyScripts.Logic.Monster
{
    public interface IMoveComponent
    {
        public float Speed { get; }

        Action DestinationAction { get; set; }
        void Move();

        void StopMove();

        Vector3 GetDirection();
    }
}