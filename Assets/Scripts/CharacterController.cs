using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_RB = default;

    [SerializeField] private float m_Speed = 25f;

    [SerializeField] private float m_Deadzone = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var input = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        if(input.magnitude > m_Deadzone)
            m_RB.velocity = input * m_Speed;
        else
        {
            m_RB.velocity -= m_RB.velocity * Time.deltaTime;
        }
    }
}
