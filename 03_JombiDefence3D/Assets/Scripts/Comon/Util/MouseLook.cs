using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float m_Speed = 1.0f;
    public float m_ClampAngle = 80.0f;
    public bool m_IsFPS = true;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
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

            rotY += mouseX * m_Speed * Time.deltaTime * 200;
            rotX += mouseY * m_Speed * Time.deltaTime * 200;

            rotX = Mathf.Clamp(rotX, -m_ClampAngle, m_ClampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            transform.rotation = localRotation;
        }
    }

}
