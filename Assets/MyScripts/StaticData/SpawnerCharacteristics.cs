using UnityEngine;
using UnityEngine.Serialization;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "GameEntity/Spawner")]
    public class SpawnerCharacteristics : GameEntityStaticData
    {

        [Header("Finish Point for Enemy")]
        public bool IsFinish;
        public string FinishPointName;
        public float FinishRadius;

        [Space] 
        public bool IsEnemy;

        public float Interval;

    }
}