using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "GameEntity/Spawner")]
public class SpawnerCharacteristics : ScriptableObject
 {
    public GameObject SpawnPrefab;
    
    [Space]
    [Header("This method is more optimazed, but less flexible")]
    public bool UseVectorPoint;
    
    [Header("This name will be finding in scene")]
    public string SpawnerPointName;

    public Vector3 SpawnerVectorPoint = new Vector3(2,2,2);

    [Space] 
    [Header("Configs for Spawn position")]
    public string SpawnPositionName;
}
