using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tuning Asset")]
public class MasterTuningSO : ScriptableObject
{
    [Header("Spawn Rates")] 
    [SerializeField, Tooltip("How often spawn calculations are made")]
    public float louseSpawnTickRate = 10;
    [SerializeField,Tooltip("Expected number of seconds to spawn a louse when hair in chunk is full.")]
    public float louseSpawnTime = 2f;
    
    [SerializeField, Tooltip("How often scratch calculations are made")]
    public float handSummonTickRate = 10;
    [SerializeField, Tooltip("Expected time to spawn a hand stritch per louse")]
    public float handSummonTime = 10;

    [SerializeField, Tooltip("The duration (seconds) of the sustain portion of the scratch animation")]
    public float handScratchDuration = 2;

    [SerializeField, Tooltip("Min distance different hand scratch locations.")]
    public float handZoneRadius = 10;
}
