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
    
    }
}