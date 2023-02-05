using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform m_Transform = default;
    [SerializeField] private float m_Speed = 1f;
    [SerializeField] private float maxPitch = 80;
    [SerializeField] private float minPitch = -5;

    private void Awake()
    {
        StartCoroutine(MouseCam());
    }

    IEnumerator MouseCam()
    {
        while (true)
        {
            yield return null;
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                
                var newEulers = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") ,0) * Time.deltaTime * m_Speed + m_Transform.rotation.eulerAngles;
                newEulers.x = Mathf.Clamp(newEulers.x, minPitch, maxPitch);
                m_Transform.rotation =  Quaternion.Euler( newEulers);
            } else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
