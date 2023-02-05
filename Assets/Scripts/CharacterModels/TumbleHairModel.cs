using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumbleHairModel : CharacterModel
{
    [SerializeField] private Transform m_Root = default;

    public override void Show()
    {
        m_Root.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        m_Root.gameObject.SetActive(false);
    }

    public override void SetSpeed(Vector3 inVel)
    {  
        if (inVel.magnitude > .01f)
        {
            var desiredRot = Quaternion.LookRotation(inVel.normalized, Vector3.up);
            m_Root.rotation = Quaternion.Slerp(m_Root.rotation, desiredRot, Time.deltaTime * 10);
        }
    }

    public override void SetSliding(bool inIsSliding)
    {
    }

    public override void StartJump()
    {
    }

    public override void StartFall()
    {
    }

    public override void EndFall()
    {
    }

    public override void StartSquish()
    {
    }

    public override void EndSquish()
    {
    }
}
