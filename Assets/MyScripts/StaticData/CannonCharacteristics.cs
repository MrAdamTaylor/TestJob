using System;
using UnityEngine;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "Cannon", menuName = "GameEntity/Cannon")]
    public class CannonCharacteristics : GameEntityStaticData
    {
        [Space]
        public float ShootInterval;

        public float Range;

        public GameObject ProjectTilePrefab;

        public TriggerConfigs TriggerConfigs;

        public float RotateSpeed;
    }
    
    [Serializable]
    public struct TriggerConfigs
    {
        public float High;
        public float Radius;
        [Range(-1,1)]
        public float AngThresh;
    }
}