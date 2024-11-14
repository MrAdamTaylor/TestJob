using System;
using UnityEngine;

public class Factory : IFactory
{
    private IAssert _assert;

    public Factory(IAssert assert)
    {
        _assert = assert;
    }

    public GameObject CreateCannon(ScriptableObject configs, Vector3 position, Transform parent)
    {
        if (configs.GetType() != typeof(CannonCharacteristics))
        {
            throw new Exception("Erros in type cast in SimpleFactory class");
        }

        throw new Exception("Not implemented");
    }
    
    public GameObject CreateSpawner(ScriptableObject configs,  Vector3 position, Transform parent)
    {
        if (configs.GetType() != typeof(SpawnerCharacteristics))
        {
            throw new Exception("Erros in type cast in SimpleFactory class");
        }
        
        SpawnerCharacteristics characteristics = (SpawnerCharacteristics)configs;
        GameObject obj = _assert.Assert(characteristics.SpawnPrefab, position, parent);
        obj.AddComponent<SpawnerComponent>();
        return obj;
    }

    public GameObject CreateNPC(ScriptableObject scriptableObject,  Vector3 position, Transform parent)
    {
        throw new Exception();
    }
}

public interface IFactory
{
    public GameObject CreateCannon(ScriptableObject configs, Vector3 position, Transform parent);

    public GameObject CreateSpawner(ScriptableObject configs, Vector3 position, Transform parent);

    public GameObject CreateNPC(ScriptableObject configs, Vector3 position, Transform parent);
}

