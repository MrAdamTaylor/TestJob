using UnityEngine;

namespace MyScripts.StaticData
{
    public class ObjectStaticData : ScriptableObject
    {
        public string PrefabPath;
        public GameObject PrefabObject;

        [Space]
        [Header("This method is more optimazed")]
        public bool IsUseVector;
        public string ScenePointName;
        public Vector3 Position;
    }
}