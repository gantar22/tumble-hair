using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockLouse : MonoBehaviour, ILouse
{
    [SerializeField] private GameObject m_Icon = default;

    private void Awake()
    {
        m_Icon.SetActive(false);
    }

    public bool isAlive => m_Icon.activeSelf;
    public void Spawn(Vector3 position)
    {
        m_Icon.transform.position = position;
        m_Icon.SetActive(true);
    }

    public Vector3 position => m_Icon.transform.position;

    public EnemyMovement movementcontroller => throw new NotImplementedException();
}
