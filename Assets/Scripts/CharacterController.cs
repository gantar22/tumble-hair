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
        } else if (m_RB.velocity.y < -.1f)
        {
            m_RB.velocity -= Physics.gravity * (Time.deltaTime * m_RB.mass);
        }

        var input = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        if (input.magnitude > m_Deadzone)
        {
            m_RB.velocity = new Vector3(input.x * m_Speed,m_RB.velocity.y, input.z * m_Speed);
        }
        else
        {
            var planeVelocity = Vector3.ProjectOnPlane(m_RB.velocity,Vector3.up);
            planeVelocity = ApplyDrag(planeVelocity);
            m_RB.velocity = planeVelocity + Vector3.up * m_RB.velocity.y;
        }
    }

    Vector3 ApplyDrag(Vector3 velocity)
    {
        if (velocity.magnitude > 1)
        {
            return velocity * (1 - Time.deltaTime * m_Drag);
        }
        else
        {
            return velocity.normalized * (velocity.magnitude * velocity.magnitude * velocity.magnitude * velocity.magnitude);
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
