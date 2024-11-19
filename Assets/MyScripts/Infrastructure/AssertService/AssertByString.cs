using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyScripts.Infrastructure.AssertService
{
    public class AssertByString : IAssert
    {
        public GameObject Assert(object assertObject)
        {
            string path = CheckCast(assertObject);
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Assert(object assertObject, Vector3 position)
        {
            string path = CheckCast(assertObject);
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion)
        {
            string path = CheckCast(assertObject);
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, quaternion);
        }

        public GameObject Assert(object assertObject, Vector3 position, Transform parent)
        {
            string path = CheckCast(assertObject);
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }

        public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion, Transform parent)
        {
            string path = CheckCast(assertObject);
            GameObject prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, quaternion, parent);
        }
    
        private string CheckCast(object assertObject)
        {
            if (assertObject.GetType() != typeof(string))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }

            return (string)assertObject;
        }
    }
}