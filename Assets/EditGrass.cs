using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditGrass : MonoBehaviour
{

    [Range(0, .05f),SerializeField] private float m_Amount = 1;

    [SerializeField] private Renderer m_Renderer = default;

    private MaterialPropertyBlock m_Block = default ;

    private static readonly int k_Amount = Shader.PropertyToID("_Amount");

    // Start is called before the first frame update
    void Start()
    {
        m_Block = new MaterialPropertyBlock();
        m_Renderer.GetPropertyBlock(m_Block);
    }

    // Update is called once per frame
    void Update()
    {
        m_Block.SetFloat(k_Amount,m_Amount);
        m_Renderer.SetPropertyBlock(m_Block);
    }
}
