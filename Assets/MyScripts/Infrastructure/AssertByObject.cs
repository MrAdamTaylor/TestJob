using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class AssertByObject : IAssert
{
    public GameObject Assert(object assertObject)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Assert(object assertObject, Vector3 position)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Assert(object assertObject, Vector3 position, Transform parent)
    {
        if (assertObject.GetType() != typeof(GameObject))
        {
            throw new Exception("Erros in type cast in SimpleFactory class");
        }
        
        GameObject prefab = (GameObject)assertObject;
        
        GameObject obj = Object.Instantiate(prefab, position, Quaternion.identity, parent);
        return obj;
    }

    public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion, Transform parent)
    {
        throw new System.NotImplementedException();
    }
}