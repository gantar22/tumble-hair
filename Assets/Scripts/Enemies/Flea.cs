using UnityEngine;

public class Flea : MonoBehaviour
{
    [SerializeField] private float m_SpeedMult = 1.5f;
    [SerializeField] private float m_Duration = 1f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.GetComponent<CharacterController>();
            player.SpeedBoost(m_SpeedMult,m_Duration);
            Destroy(this.gameObject);
        }
    }
}
