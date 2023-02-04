using UnityEngine;

public class Puss : MonoBehaviour
{
    [SerializeField] private float m_SpeedMult = -1.5f;
    [SerializeField] private float m_Duration = .25f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<CharacterController>();
            player.SpeedBoost(m_SpeedMult, m_Duration);
            Destroy(this.gameObject);
        }
    }
}
