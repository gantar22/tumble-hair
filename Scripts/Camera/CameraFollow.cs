using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_Target;

    private Vector3 m_Offset;
    // Start is called before the first frame update
    void Start()
    {
        m_Offset = transform.position - m_Target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Target.position + m_Offset;
    }
}
