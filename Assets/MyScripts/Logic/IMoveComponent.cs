using System;

namespace MyScripts.Logic
{
    public interface IMoveComponent
    {
        Action DestinationAction { get; set; }
        void Move();

        void StopMove();
    }
}