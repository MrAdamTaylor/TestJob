using UnityEngine;

public interface IAssert
{
    public GameObject Assert(object assertObject);
    public GameObject Assert(object assertObject, Vector3 position);
    public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion);
    public GameObject Assert(object assertObject, Vector3 position, Transform parent);
    public GameObject Assert(object assertObject, Vector3 position, Quaternion quaternion, Transform parent);
}