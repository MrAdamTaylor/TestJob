using System;
using UnityEngine;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "Cannon", menuName = "GameEntity/Cannon")]
    public class CannonCharacteristics : GameEntityStaticData
    {
        public GameObject ProjectTilePrefab;

        public TriggerConfigs TriggerConfigs;

        public float RotateSpeed;
        public string ShootPoinName;
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