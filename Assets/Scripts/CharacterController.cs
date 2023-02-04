using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_RB = default;

    [SerializeField] private float m_Speed = 10f;
    [SerializeField] private float m_Jump = 5f;

    [SerializeField] private float m_Deadzone = .1f;
    [SerializeField] private float m_Drag = 3;
    
    private bool m_CanJump = true;
    private float m_SpeedBoostTimer = 0f;
    private float m_SpeedBoostDuration = 0f;
    private float m_CurrSpeed = 1f;
    private bool m_Boost = false;

    private void Start()
    {
        m_CurrSpeed = m_Speed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_CanJump)
        {
            m_RB.velocity += new Vector3(0f, m_Jump, 0f);
        } else if (m_RB.velocity.y < -.1f)
        {
            m_RB.velocity -= Physics.gravity * (Time.deltaTime * m_RB.mass); //This still makes jumping take forever to land. Feels better without it.
        }

        var input = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        if (input.magnitude > m_Deadzone)
        {
            m_RB.velocity = new Vector3(input.x * m_CurrSpeed,m_RB.velocity.y, input.z * m_CurrSpeed);
        }
        else
        {
            var planeVelocity = Vector3.ProjectOnPlane(m_RB.velocity,Vector3.up);
            planeVelocity = ApplyDrag(planeVelocity);
            m_RB.velocity = planeVelocity + Vector3.up * m_RB.velocity.y;
        }

        if(m_Boost)
        {
            if (m_SpeedBoostTimer >= m_SpeedBoostDuration)
            {
                m_Boost = false;
                m_CurrSpeed = m_Speed;
            }
            else
            {
                m_SpeedBoostTimer += Time.deltaTime;
            }
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

    public void SpeedBoost(float inMult, float inDur)
    {
        m_Boost = true;
        m_SpeedBoostDuration = inDur;
        m_SpeedBoostTimer = 0;
        m_CurrSpeed = m_Speed * inMult;
    }

}
