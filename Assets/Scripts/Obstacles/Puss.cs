using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puss : MonoBehaviour
{
    [SerializeField] private Shape m_Flat;
    [SerializeField] private Shape m_Blob;
    [SerializeField] private float m_Duration = .25f;
    [SerializeField]
    private UnityEvent<Puss> m_OnDeath;
    public UnityEvent<Puss> OnDeath => m_OnDeath;
    private bool m_GroundCheck = false;

    [Serializable]
    private struct Shape
    {
       public Collider col;
       public MeshRenderer renderer;
    }

    private void Flatten()
    {
        m_Blob.col.enabled = false;
        m_Blob.renderer.gameObject.SetActive(false);
        m_Flat.col.enabled = true;
        m_Flat.renderer.gameObject.SetActive(true);
    }

    private void ResetShape()
    {
        m_Blob.col.enabled = true;
        m_Blob.renderer.gameObject.SetActive(true);
        m_Flat.col.enabled = false;
        m_Flat.renderer.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        m_GroundCheck = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<CharacterController>();
            player.PussEffect(m_Duration);
            m_OnDeath?.Invoke(this);
            m_OnDeath.RemoveAllListeners();
            gameObject.SetActive(false);
            ResetShape();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (m_GroundCheck && collision.collider.CompareTag("Ground"))
        {
            Flatten();
            m_GroundCheck = false;
        }
    }
}
