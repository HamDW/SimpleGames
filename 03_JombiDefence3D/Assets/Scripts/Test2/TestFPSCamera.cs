using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFPSCamera : FPSCamera
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        m_rotY = rot.y;
        m_rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsFPS)
        {
            //float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");
            m_rotY = 0;
           // m_rotY += mouseX * m_Speed * Time.deltaTime * 200;
            m_rotX += mouseY * m_Speed * Time.deltaTime * 200;

            m_rotX = Mathf.Clamp(m_rotX, -m_ClampAngle, m_ClampAngle);

            Quaternion localRotation = Quaternion.Euler(m_rotX, m_rotY, 0.0f);
            transform.localRotation = localRotation;
        }
    }
}

