using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;
    public float x;
    public float z;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this.gameObject);
        }else
        {
            instance = this;
        }
    }
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 sp = Vector3.zero;

        sp.x = Random.Range(-x, x);
        sp.z = Random.Range(-z, z);

        return sp;
    }
}
