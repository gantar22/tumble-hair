using UnityEngine;
public class Controller : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Rigidbody m_RB;
    [SerializeField] private float m_RollSpeed = 10;
    [SerializeField] private float m_Deadzone = .1f;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        movement = (input.z * m_Camera.transform.forward) + (input.x * m_Camera.transform.right);

        if (movement.magnitude > m_Deadzone)
        {
            m_RB.velocity = movement * m_RollSpeed;
        }
        else
        {
            m_RB.velocity -= m_RB.velocity * Time.deltaTime;
        }
    }
}
