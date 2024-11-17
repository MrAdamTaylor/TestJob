using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class NpcFactory : IFactory
    {
        private IAssert _assert;
        private ObjectData _data;

        public NpcFactory(IAssert assert, ObjectData data)
        {
            _assert = assert;
            _data = data;
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(NpcCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }

            GameObject obj = _assert.Assert(_data.ModelData, _data.PositionData);
            MonsterLogic logic = obj.AddComponent<MonsterLogic>();
            logic.Construct();
            obj.AddComponent<SelfDestroyByTime>();
            return obj;
        }
        
        
    }
}

