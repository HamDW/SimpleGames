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
            m_rotX += mouseY * m_Speed * Time.deltaTime * 200;      // -���� ���ü� �ְ� ��������� Ȱ���Ѵ�.

            m_rotX = Mathf.Clamp(m_rotX, -m_ClampAngle, m_ClampAngle);
            vRot.x = m_rotX;                                        // ����) vRot.x�� ���� -���� �ȳ��´�.(0 -> 360���� �Ѿ)

            Quaternion localRotation = Quaternion.Euler(vRot);
            transform.localRotation = localRotation;
        }
    }
}

