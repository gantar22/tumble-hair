using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_RB = default;

    [SerializeField] private float m_Speed = 10f;
    [SerializeField] private float m_Jump = 5f;

    [SerializeField] private float m_Deadzone = .1f;
    [SerializeField] private float m_Drag = 3;
    
    private bool m_CanJump = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_CanJump)
        {
            m_RB.velocity += new Vector3(0f, m_Jump, 0f);
        }

        var input = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        if (input.magnitude > m_Deadzone)
        {
            m_RB.velocity = new Vector3(input.x * m_Speed,m_RB.velocity.y, input.z * m_Speed);
        }
        else
        {
            var curVel = m_RB.velocity;
            if (curVel.magnitude > 1)
            {
                m_RB.velocity = curVel * (1 - Time.deltaTime * m_Drag);
            }
            else
            {
                m_RB.velocity = curVel.normalized * (curVel.magnitude * curVel.magnitude * curVel.magnitude * curVel.magnitude);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_CanJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_CanJump = false;
        }
    }

}
