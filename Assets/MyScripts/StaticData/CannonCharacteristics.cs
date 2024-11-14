using UnityEngine;

[CreateAssetMenu(fileName = "BaffAmmo", menuName = "Baff/Ammo")]
public class CannonCharacteristics : ScriptableObject
{
    public GameObject SpawnPrefab;

    public float ShootInterval;

    public float Range;

    public GameObject ProjectTilePrefab;

    [Space]
    [Header("This method is more optimazed")]
    public bool IsVector;
    
    public string ShootPointTag;
    
    public Vector3 ShootPoint;
    
}