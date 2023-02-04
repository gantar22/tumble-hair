using UnityEngine;
using UnityEngine.Events;

public class Puss : MonoBehaviour
{
    [SerializeField] private float m_SpeedMult = -1.5f;
    [SerializeField] private float m_Duration = .25f;
    [SerializeField]
    private UnityEvent<Puss> m_OnDeath;
    public UnityEvent<Puss> OnDeath => m_OnDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<CharacterController>();
            player.SpeedBoost(m_SpeedMult, m_Duration);
            m_OnDeath?.Invoke(this);
            m_OnDeath.RemoveAllListeners();
            gameObject.SetActive(false);
        }
    }
}
