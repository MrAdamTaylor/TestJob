using System;
using UnityEngine;

public class Factory 
{
    private IAssert _assert;

    public GameObject CreateCannon()
    {
        throw new Exception();
    }
    
    public GameObject CreateSpawner()
    {
        throw new Exception();
    }
}