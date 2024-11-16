using UnityEngine;

namespace MyScripts.StaticData
{
    [CreateAssetMenu(fileName = "CommonConfigs", menuName = "GameConfigs")]
    public class CommonConfigs : ScriptableObject
    {
        [Header("Load prefabs by Name")]
        public bool IsLoadByName;
    }
}
