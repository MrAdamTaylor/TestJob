using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public interface IFactory
    {
        public GameObject Create(ScriptableObject configs, Transform parent);
    
    }
}