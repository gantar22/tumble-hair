using UnityEngine;
public class Controller : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;
    [SerializeField] private Rigidbody m_RB;
    [SerializeField] private float m_RollSpeed;
    [SerializeField] private ControlMode m_Mode;

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        if (m_Mode == ControlMode.WASD)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            movement = (input.z * m_Camera.transform.forward) + (input.x * m_Camera.transform.right);
        }
        else
        {
            movement = MouseInput();
        }

        m_RB.AddForce(movement * m_RollSpeed * Time.deltaTime);
    }

    private Vector3 MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray,out var hit) && hit.collider)
            {
                return (hit.point - m_RB.position).normalized;
            }
        }

        return Vector3.zero;
    }

    private enum ControlMode
    {
        WASD,Mouse
    }
}
