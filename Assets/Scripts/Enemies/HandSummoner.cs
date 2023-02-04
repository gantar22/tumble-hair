using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class HandSummoner : MonoBehaviour
{
    [SerializeField] private MasterTuningSO tuningAsset = default;

    private void OnEnable()
    {
        StartCoroutine(SummonTick());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SummonTick()
    {
        var retries = 0;
        var random = new Random(gameObject.GetInstanceID());
        while (true)
        {
            yield return new WaitForSeconds(1/tuningAsset.handSummonTickRate);
            var expectedTicks = tuningAsset.handSummonTime * tuningAsset.handSummonTickRate;
            double p = 1 / expectedTicks;
            var rand = random.NextDouble();
            for (int i = 0; i < Mathf.Max(retries,5); i++)
                rand = Math.Max(rand, random.NextDouble());
            if (random.NextDouble() < p)
            {
                if (!HandManager.I.Summon(transform.position))
                    retries++;
                else
                    retries = 0;
            }
        }
    }
}
