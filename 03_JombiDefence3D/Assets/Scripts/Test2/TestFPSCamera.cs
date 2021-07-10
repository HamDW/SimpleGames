using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFPSCamera : FPSCamera
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsFPS)
        {
            Vector3 vRot = transform.localEulerAngles;

            float mouseY = -Input.GetAxis("Mouse Y");
            m_rotX += mouseY * m_Speed * Time.deltaTime * 200;      // -값이 나올수 있게 멤버변수를 활용한다.

            m_rotX = Mathf.Clamp(m_rotX, -m_ClampAngle, m_ClampAngle);
            vRot.x = m_rotX;                                        // 주의) vRot.x는 값이 -값이 안나온다.(0 -> 360도로 넘어감)

            Quaternion localRotation = Quaternion.Euler(vRot);
            transform.localRotation = localRotation;
        }
    }
}

