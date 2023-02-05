using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleaRidingModel : CharacterModel
{
    [SerializeField] private Transform m_Flea;
    [SerializeField] private Transform m_Rider;
    public override void Show()
    {
        m_Flea.gameObject.SetActive(true);
        m_Rider.gameObject.SetActive(true);
    }

    public override void Hide()
    {
        m_Flea.gameObject.SetActive(false);
        m_Rider.gameObject.SetActive(false);
    }

    public override void SetSpeed(Vector3 inVel)
    {
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
