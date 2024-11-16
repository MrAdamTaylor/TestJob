using UnityEngine;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "NPC", menuName = "GameEntity/NPC")]
    public class NpcCharacteristics : ObjectStaticData
    {
        public const float STANDART_SPEED = 0.1f;
        public const int MAX_HP = 30;
        public const int HP = 30;
        public const string STANDART_MOVE_TARG_NAME = "";
    
        
        [Space]
        [Header("Enemy Characteristics ")]
        public string MoveTargetName;
        public float Speed;
        public int MaxHp;
        public int Hp;
    }
}