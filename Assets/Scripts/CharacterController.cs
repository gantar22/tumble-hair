using System;
using System.Collections;
using System.ComponentModel;
using UnityEngine;

public abstract class CharacterModel : MonoBehaviour
{
    public abstract void Show();
    public abstract void Hide();
    public abstract void SetSpeed(Vector3 inVel);
    public abstract void SetSliding(bool inIsSliding);
    public abstract void StartJump();
    public abstract void StartFall();
    public abstract void EndFall();

    public abstract void StartSquish();
    public abstract void EndSquish();
}

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float m_PussSpeedMult = .75f;
    [SerializeField] private float m_FleaSpeedMult = 1.5f;
    [SerializeField] private Rigidbody m_RB = default;

    [SerializeField] private float m_Speed = 10f;
    [SerializeField] private float m_Jump = 5f;

    [SerializeField] private float m_Deadzone = .1f;
    [SerializeField] private float m_Drag = 3;
    [SerializeField,Range(0,1)] private float m_JumpVelFactor = .25f;

    [SerializeField]
    private bool m_Grounded = true;

    [SerializeField] private Camera m_Camera = default;
    [SerializeField] private CharacterModel m_FleaModel = default;
    [SerializeField] private CharacterModel m_TumbleHairModel = default;
    [SerializeField,Range(0,5)] private float m_DamageDuration = 2;
    [SerializeField] private float m_DamageSpeedMult = .075f;
    
    private CharacterModel _ActiveModel;

    private CharacterModel activeModel
    {
        get => _ActiveModel;
        set
        {
            if (value != _ActiveModel)
            {
                if(_ActiveModel != null)
                    _ActiveModel.Hide();
                _ActiveModel = value;
                if(_ActiveModel != null)
                    _ActiveModel.Show();
            }
        }
    }
    private Coroutine m_PussRoutine = null;
    private Coroutine m_FleaRoutine = null;
    private Coroutine m_DamageRoutine = null;
    private bool m_IsSliding = false;
    private Vector3 m_SlideVector;

    private void Awake()
    {
        activeModel = m_TumbleHairModel;
    }

    void NormalMovement()
    {
        var effectiveSpeed = 
            (m_PussRoutine   != null ? m_PussSpeedMult   : 1) *
            (m_FleaRoutine   != null ? m_FleaSpeedMult   : 1) *
            (m_DamageRoutine != null ? m_DamageSpeedMult : 1) * 
             m_Speed;
        
        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && m_Grounded)
        {
            m_RB.velocity += new Vector3(0f, m_Jump, 0f);
            activeModel.StartJump();
        } else if (m_RB.velocity.y < -.1f && !m_Grounded)
        {
            activeModel.StartFall();
            m_RB.velocity += Physics.gravity * (Time.deltaTime * m_RB.mass);
        }

        //Movement
        var camRight = Vector3.ProjectOnPlane(m_Camera.transform.right,Vector3.up);
        var camForward= Vector3.ProjectOnPlane(m_Camera.transform.forward,Vector3.up);
        var input = camRight * Input.GetAxis("Horizontal") + camForward * Input.GetAxis("Vertical");
        if (input.magnitude > m_Deadzone)
        {
            var desiredVel = new Vector3(input.x * effectiveSpeed, m_RB.velocity.y, input.z * effectiveSpeed);
            if (m_Grounded)
                m_RB.velocity = desiredVel;
            else
                m_RB.velocity = Vector3.Lerp(m_RB.velocity, desiredVel, m_JumpVelFactor);
        }
        else
        {
            var planeVelocity = Vector3.ProjectOnPlane(m_RB.velocity,Vector3.up);
            planeVelocity = ApplyDrag(planeVelocity, m_Drag * (m_Grounded ? 1 : m_JumpVelFactor));
            m_RB.velocity = planeVelocity + Vector3.up * m_RB.velocity.y;
        }
    }

    void Update()
    {
        if (m_IsSliding)//Only occurs when entering a grease puddle.
        {
            m_RB.velocity = m_SlideVector;
        }
        else
        {
            NormalMovement();
        }
        activeModel.SetSpeed(m_RB.velocity);
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
            if(!m_Grounded)
                activeModel.EndFall();
            m_Grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            m_Grounded = false;
        }
    }

    public void PussEffect(float inDur)
    {
        m_PussRoutine = StartCoroutine(PussEffectImpl(inDur));
    }

    IEnumerator PussEffectImpl(float inDur)
    {
        yield return new WaitForSeconds(inDur);
        m_PussRoutine = null;
    }
    
    public void FleaEffect(float inDur)
    {
        m_FleaRoutine = StartCoroutine(FleaEffectImpl(inDur));
    }

    IEnumerator FleaEffectImpl(float inDur)
    {
        activeModel = m_FleaModel;
        yield return new WaitForSeconds(inDur);
        activeModel = m_TumbleHairModel;
        m_FleaRoutine = null;
    }
    

    public void Slide(bool inSlide, float inSlideMult)
    {
        m_IsSliding = inSlide;
        activeModel.SetSliding(inSlide);
        m_SlideVector = new Vector3(m_RB.velocity.x * inSlideMult,m_RB.velocity.y,m_RB.velocity.z * inSlideMult);

        if(m_SlideVector.magnitude < inSlideMult)
        {
            m_SlideVector *= inSlideMult / m_SlideVector.magnitude;
        }
    }


    public void Bounce(float inBounce)
    {
        m_RB.velocity += new Vector3(0f, inBounce, 0f);
        activeModel.StartJump();
    }

    public void TakeDamage()
    {
        if(m_FleaRoutine != null)   
            StopCoroutine(m_FleaRoutine);
        activeModel = m_TumbleHairModel;
        m_DamageRoutine = StartCoroutine(DamageImpl());
    }

    IEnumerator DamageImpl()
    {
        activeModel.StartSquish();
        yield return new WaitForSeconds(m_DamageDuration);
        activeModel.EndSquish();
        m_DamageRoutine = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScratchZone"))
        {
            TakeDamage();    
        }
    }
}
