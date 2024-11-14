using UnityEngine;

[CreateAssetMenu(fileName = "Spawner", menuName = "GameEntity/Spawner")]
public class SpawnerCharacteristics : ScriptableObject
{
    public GameObject SpawnPrefab;
    
    [Space]
    [Header("This method is more optimazed, but less flexible")]
    public bool UseVectorPoint;
    
    public string SpawnPointName;

    public Vector3 SpawnPoin;
}