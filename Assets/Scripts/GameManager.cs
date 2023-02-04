using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void Awake()
    {
        foreach (var chunk in GetComponentsInChildren<Chunk>())
        {
            chunk.Commence();
        }
    }
}
