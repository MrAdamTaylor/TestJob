using MyScripts;
using UnityEngine;

public class GameLauncher : MonoBehaviour
{
    private IAssert _assert;
    private Factory _factory;
    
    public void Start()
    {
        
        CommonConfigs commonConfigs = Resources.Load<CommonConfigs>("StaticData/CommonConfigs");

        if (commonConfigs.IsLoadByName)
        {
            _assert = new AssertByString();
        }
        else
        {
            _assert = new AssertByObject();
        }

        _factory = new Factory(_assert);

        CannonCharacteristics cannonCharacteristics = Resources.Load<CannonCharacteristics>("StaticData/Cannon");
        SpawnerCharacteristics spawnerCharacteristics = Resources.Load<SpawnerCharacteristics>("StaticData/Spawner");

        GameObject gameObject = GameObject.Find(SceneConstants.GAME_OBJECTS);
        GameObject spawnPoint = GameObject.Find(spawnerCharacteristics.SpawnerPointName);

        _factory.CreateSpawner(spawnerCharacteristics, spawnPoint.transform.position, gameObject.transform);
    }
}
