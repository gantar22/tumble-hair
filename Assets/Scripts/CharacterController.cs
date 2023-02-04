using System.ComponentModel;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody m_RB = default;

    [SerializeField] private float m_Speed = 10f;
    [SerializeField] private float m_Jump = 5f;

    [SerializeField] private float m_Deadzone = .1f;
    [SerializeField] private float m_Drag = 3;
    [SerializeField,Range(0,1)] private float m_JumpVelFactor = .25f;
    
    [SerializeField]
    private bool m_CanJump = true;
    private float m_SpeedBoostTimer = 0f;
    private float m_SpeedBoostDuration = 0f;
    private float m_CurrSpeed = 1f;
    private bool m_Boost = false;
    private bool m_IsSliding = false;
    private Vector3 m_SlideVector;

    private void Start()
    {
        m_CurrSpeed = m_Speed;
    }

    void Update()
    {
        if (m_IsSliding)//Only occurs when entering a grease puddle.
        {
            m_RB.velocity = m_SlideVector;
            return;
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_CanJump)
        {
            m_RB.velocity += new Vector3(0f, m_Jump, 0f);
        } else if (m_RB.velocity.y < -.1f)
        {
            m_RB.velocity += Physics.gravity * (Time.deltaTime * m_RB.mass);
        }

        //Movement
        var input = Vector3.right * Input.GetAxis("Horizontal") + Vector3.forward * Input.GetAxis("Vertical");
        if (input.magnitude > m_Deadzone)
        {
            var desiredVel = new Vector3(input.x * m_CurrSpeed, m_RB.velocity.y, input.z * m_CurrSpeed);
            if (m_CanJump)
                m_RB.velocity = desiredVel;
            else
                m_RB.velocity = Vector3.Lerp(m_RB.velocity, desiredVel, m_JumpVelFactor);
        }
        else
        {
            var planeVelocity = Vector3.ProjectOnPlane(m_RB.velocity,Vector3.up);
            planeVelocity = ApplyDrag(planeVelocity, m_Drag * (m_CanJump ? 1 : m_JumpVelFactor));
            m_RB.velocity = planeVelocity + Vector3.up * m_RB.velocity.y;
        }

        //Speed boost
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

    Vector3 ApplyDrag(Vector3 velocity,float drag)
    {
        if (velocity.magnitude > 1)
        {
            return velocity * (1 - Time.deltaTime * drag);
        }
        else
        {
            return velocity.normalized * (velocity.magnitude * velocity.magnitude * velocity.magnitude * velocity.magnitude);
        }
    }

    //Ground checking for jumping
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

    public void Slide(bool inSlide, float inSlideMult)
    {
        m_IsSliding = inSlide;
        m_SlideVector = new Vector3(m_RB.velocity.x * inSlideMult,m_RB.velocity.y,m_RB.velocity.z * inSlideMult);

        if(m_SlideVector.magnitude < inSlideMult)
        {
            m_SlideVector *= inSlideMult / m_SlideVector.magnitude;
        }

        if(m_IsSliding)
        {
            m_RB.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            m_RB.constraints = RigidbodyConstraints.None;
        }
    }


    public void Bounce(float inBounce)
    {
        m_RB.velocity += new Vector3(0f, inBounce, 0f);
    }
}
