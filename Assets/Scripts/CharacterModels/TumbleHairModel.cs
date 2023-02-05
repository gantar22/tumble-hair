using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleHairModel : CharacterModel
{
    [SerializeField] private Transform m_Root = default;
    [SerializeField] private Animator m_Animator = default;
    [SerializeField] private float m_SpeedMult = .1f;
    
    private float m_Speed = default;
    public override void Show()
    {
        m_Root.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        m_Animator.SetBool("jumping",false);
        m_Animator.SetBool("hurt",false);
        m_Root.gameObject.SetActive(false);
    }

    public override void SetSpeed(Vector3 inVel)
    {
        m_Speed = inVel.magnitude * m_SpeedMult;
        m_Animator.SetFloat("speed",m_Speed);
        if (inVel.magnitude > .01f)
        {
            var desiredRot = Quaternion.LookRotation(inVel.normalized, Vector3.up);
            m_Root.rotation = Quaternion.Slerp(m_Root.rotation, desiredRot, Time.deltaTime * 10);
        }
    }

    public override void SetSliding(bool inIsSliding)
    {
        if(inIsSliding)
            m_Animator.SetFloat("speed",0);
        else
            m_Animator.SetFloat("speed",m_Speed);
    }

    public override void StartJump()
    {
        m_Animator.SetBool("jumping",true);
    }

    public override void StartFall()
    {
        m_Animator.SetTrigger("fall");
    }

    public override void EndFall()
    {
        m_Animator.SetBool("jumping",false);
    }

    public override void StartSquish()
    {
        m_Animator.SetBool("hurt",true);
    }

    public override void EndSquish()
    {
        m_Animator.SetBool("hurt",false);
    }
}
