using UnityEngine;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "GameEntity/Spawner")]
    public class SpawnerCharacteristics : GameEntityStaticData
    {

        [Space] 
        public bool IsEnemy;

        public float Interval;

    }
}