using System;
using UnityEngine;

namespace MyScripts.Infrastructure.AssertService
{
    public class AssertByString : IAssert
    {
        public GameObject Assert(object assertObject)
        {
            CheckCast(assertObject);
            throw new System.NotImplementedException();
        }

        public GameObject Assert(object assertObject, Vector3 position)
        {
            CheckCast(assertObject);
            throw new System.NotImplementedException();
        }

        public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion)
        {
            CheckCast(assertObject);
            throw new System.NotImplementedException();
        }

        public GameObject Assert(object assertObject, Vector3 position, Transform parent)
        {
            CheckCast(assertObject);
            throw new System.NotImplementedException();
        }

        public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion, Transform parent)
        {
            CheckCast(assertObject);
            throw new System.NotImplementedException();
        }
    
        private void CheckCast(object assertObject)
        {
            if (assertObject.GetType() != typeof(string))
            {
                throw new Exception("Erros in type cast in SimpleFactory class");
            }
        }
    }
}