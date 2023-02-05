using Unity.Collections;
using UnityEngine;

public class Hair : MonoBehaviour, IHairFolicule
{
    [SerializeField] private SphereCollider m_Collider = default;
    [SerializeField] private Transform m_Base;
    [SerializeField] private Transform m_Tip;
    [SerializeField] private float m_YScale;
    [SerializeField] private float m_XZScale;
    [SerializeField, ReadOnly]
    private float m_Height;
    [SerializeField] Transform m_Nub;
    [SerializeField] Transform m_Hair;
    [SerializeField] HairBone[] m_Bones;

    public float height
    {
        get => m_Height;
        set
        {
            m_Height = value;
            UpdateVisual(m_Height, m_Tangent);
        }
    }
    [SerializeField, ReadOnly]
    private Vector2 m_Tangent;

    public Vector2 tangent
    {

        get => m_Tangent;
        set
        {
            m_Tangent = value;
            UpdateVisual(m_Height, m_Tangent);
        }
    }

    public Vector3 position() => transform.position;
    public float radius() => m_Collider.radius;

    void Start()
    {
        UpdateVisual(0, Vector2.zero);
    }

    void UpdateVisual(float height, Vector2 tangent)
    {
        tangent *= m_XZScale;
        m_Tip.position = m_Base.position + new Vector3(tangent.x, height * m_YScale, tangent.y);

        //Scale up the hair
        m_Hair.localScale = new Vector3(m_Hair.localScale.x, height * 20f, m_Hair.localScale.z);

        //Rotate up directio towards tip
        var direction = (m_Tip.position - m_Hair.position).normalized;
        m_Hair.up = direction;

        //Rotate bones
        for(int i = 0; i < m_Bones.Length; i++)
        {
            m_Bones[i].transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Min(m_Bones[i].Max, Vector3.Cross(Vector3.up, tangent).magnitude), Vector3.Cross(Vector3.up, tangent));
        }

        if (height <= .1f)
        {
            m_Nub.gameObject.SetActive(true);
            m_Hair.gameObject.SetActive(false);
            return;
        }
        else
        {
            m_Nub.gameObject.SetActive(false);
            m_Hair.gameObject.SetActive(true);
        }
    }
}