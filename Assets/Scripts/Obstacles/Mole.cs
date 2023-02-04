using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField] private float m_Bounce = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<CharacterController>();
            player.Bounce(m_Bounce);
        }
    }
}
