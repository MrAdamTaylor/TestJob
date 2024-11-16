using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.Logic;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class SpawnerFactory : IFactory
    {
        private IAssert _assert;
        private ObjectData _objectData;

        public SpawnerFactory(IAssert assert, ObjectData objectData)
        {
            _assert = assert;
            _objectData = objectData;
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(SpawnerCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
        
            Debug.Log("Cannon Position: "+_objectData.PositionData);
            SpawnerCharacteristics characteristics = (SpawnerCharacteristics)configs;
            GameObject obj = _assert.Assert(_objectData.ModelData, _objectData.PositionData, parent);
            obj.AddComponent<SpawnerComponent>();
            return obj;
        }
    }
}