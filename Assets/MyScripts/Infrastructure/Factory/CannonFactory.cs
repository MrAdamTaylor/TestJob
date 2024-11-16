using System;
using MyScripts.Data;
using MyScripts.Infrastructure.AssertService;
using MyScripts.StaticData;
using UnityEngine;

namespace MyScripts.Infrastructure.Factory
{
    public class CannonFactory : IFactory
    {
        private IAssert _assert;
        private ObjectData _objectData;
    
        public CannonFactory(IAssert assert, ObjectData data)
        {
            _assert = assert;
            _objectData = data;
        }

        public GameObject Create(ScriptableObject configs,  Transform parent)
        {
            if (configs.GetType() != typeof(CannonCharacteristics))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
        
            CannonCharacteristics characteristics = (CannonCharacteristics)configs;
        
            Debug.Log("Cannon Position: "+_objectData.PositionData);
            GameObject obj = _assert.Assert(_objectData.ModelData, _objectData.PositionData, parent);
            return obj;
        }
    }
}