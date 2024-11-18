using UnityEngine;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "Shell", menuName = "GameEntity/Shell")]
    public class ShellStaticData : ObjectStaticData
    {
        public float Speed;
        public float Damage;

        public bool IsSelfDestory;
        public float SecondsByDestoying;
        
        [HideInInspector] public Transform Turret;
    }
}