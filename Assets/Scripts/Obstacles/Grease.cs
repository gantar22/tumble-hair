using UnityEngine;

public class Grease : MonoBehaviour
{
    [SerializeField]
    private float m_SlideMult = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var player = other.GetComponent<CharacterController>();
            player.Slide(true, m_SlideMult);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<CharacterController>();
            player.Slide(false, m_SlideMult);
        }
    }
}
