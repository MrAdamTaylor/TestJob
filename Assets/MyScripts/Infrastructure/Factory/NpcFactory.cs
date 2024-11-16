using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class NpcFactory : IFactory
    {
        

        public NpcFactory(IAssert assert, ObjectData data)
        {
            
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(NpcCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
            throw new Exception("Not implemented");
        }
    }
}