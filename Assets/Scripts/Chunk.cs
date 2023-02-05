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
    

    public void Commence()
    {
        StartCoroutine(SpawnLice());
    }

    public float HairFill()//0-1
    {
        return folicules.Sum(_=>_.height) / folicules.Length;
    }

    IEnumerator SpawnLice()
    {
        var hairAmount = 0f;
        var fillVel = 0f;
        var random = new Random(gameObject.GetInstanceID());
        while (true)
        {
            yield return new WaitForSeconds(1/tuningAsset.louseSpawnTickRate);
            var expectedTicks = tuningAsset.louseSpawnTime * tuningAsset.louseSpawnTickRate;
            hairAmount = Mathf.SmoothDamp(hairAmount, HairFill(), ref fillVel, 2, Mathf.Infinity,1 / tuningAsset.louseSpawnTickRate);
            double p = hairAmount / expectedTicks;
            if (random.NextDouble() < p)
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
