using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour
{
    public float m_Speed = 1.0f;
    public float m_ClampAngle = 80.0f;
    public bool m_IsFPS = true;

    protected float m_rotY = 0.0f; // rotation around the up/y axis
    protected float m_rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        m_rotY = rot.y;
        m_rotX = rot.x;
    }

    public static void LockedMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void UnlockedMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (m_IsFPS)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            m_rotY += mouseX * m_Speed * Time.deltaTime * 200;
            m_rotX += mouseY * m_Speed * Time.deltaTime * 200;

            m_rotX = Mathf.Clamp(m_rotX, -m_ClampAngle, m_ClampAngle);

            Quaternion localRotation = Quaternion.Euler(m_rotX, m_rotY, 0.0f);
            transform.localRotation = localRotation;
        }
    }

}
