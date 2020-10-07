using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class object_spawner : MonoBehaviour
{
    PoolManager poolManager;
    private void Start()
    {
        poolManager = PoolManager.Instance;
    }

    private void FixedUpdate()
    {
        poolManager.spawnFromPool("Cube", transform.position, Quaternion.identity);
    }
}
