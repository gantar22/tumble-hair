using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class Chunk : MonoBehaviour
{
    public MasterTuningSO tuningAsset = default;
    public IHairFolicule[] folicules = default;

    public ILouse[] lice = default;
    private Vector3[] liceSpawnPoints = default;

    private Random random = new Random();
    void Commence()
    {
        StartCoroutine(SpawnLice());
    }

    IEnumerator SpawnLice()
    {
        while (true)
        {
            yield return new WaitForSeconds(1/tuningAsset.louseSpawnTickRate);
            double p = 1 / (tuningAsset.louseSpawnTime * tuningAsset.louseSpawnTickRate);
            if (random.NextDouble() > p)
            {
                for (int i = 0; i < lice.Length; i++)
                {
                    if (!lice[i].isAlive)
                    {
                        lice[i].Spawn(liceSpawnPoints[i]);
                        break;
                    }
                }
            }
            
        }
    }
    
    private void OnValidate()
    {
        folicules = GetComponentsInChildren<IHairFolicule>();
        lice = GetComponentsInChildren<ILouse>();
        liceSpawnPoints = lice.Select(_ => _.position).ToArray();
    }
}
