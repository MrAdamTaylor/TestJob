using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyScripts.Infrastructure.AssertService
{
    public class AssertByObject : IAssert
    {
        public GameObject Assert(object assertObject)
        {
            CheckCast(assertObject);
            GameObject prefab = (GameObject)assertObject;
        
            GameObject obj = Object.Instantiate(prefab);
            return obj;
        }

        public GameObject Assert(object assertObject, Vector3 position)
        {
            CheckCast(assertObject);
            GameObject prefab = (GameObject)assertObject;
        
            GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity);
            return obj;
        }

        public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion)
        {
            CheckCast(assertObject);
            GameObject prefab = (GameObject)assertObject;
        
            GameObject obj = Object.Instantiate(prefab, position, quaternion);
            return obj;
        }

        public GameObject Assert(object assertObject, Vector3 position, Transform parent)
        {
            CheckCast(assertObject);
        
            GameObject prefab = (GameObject)assertObject;
        
            GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity, parent);
            return obj;
        }

        public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion, Transform parent)
        {
            CheckCast(assertObject);
            GameObject prefab = (GameObject)assertObject;
        
            GameObject obj = Object.Instantiate(prefab, position, quaternion, parent);
            return obj;
        }

        private void CheckCast(object assertObject)
        {
            if (assertObject.GetType() != typeof(GameObject))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
        }
    }
}