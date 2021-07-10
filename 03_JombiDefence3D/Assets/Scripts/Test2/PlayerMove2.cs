using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public Animator m_Animator = null;
    public float m_Speed = 2.0f;        // ĳ���� �̵� �ӵ�
    public float m_JumpSpeedF = 8.0f;   // ĳ���� ������ �ö󰡴� �ӵ�.
    public float m_Gravity = 20.0f;     // ĳ���Ϳ��� �ۿ��ϴ� �߷�. ( ������ �������� �ӵ� )

    private CharacterController m_Controller = null;    // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�.
    private Vector3 m_MoveDir;                          // ĳ������ �����̴� ����.

    public Camera m_Camera;
    private float m_CamRotX = 0;    // ī�޶� x�� ȸ�� ��

    // Start is called before the first frame update
    void Start()
    {
        FPSCamera.LockedMouse();
        m_Controller = GetComponent<CharacterController>();
    }


    public void Initialize()
    {
        transform.localEulerAngles = Vector3.zero;
        m_Camera.transform.localEulerAngles = Vector3.zero;
    }

    void Update()
    {
        PlayerRoation();
        CameraRotation();
        Move();
    }

    // �÷��̾� ȸ�� ( �¿� ȸ�� )
    public void PlayerRoation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        Vector3 vRot = transform.localEulerAngles;
        vRot.y += mouseX * m_Speed * Time.deltaTime * 100;
        //vRot.x += mouseY * m_Speed * Time.deltaTime * 100;

        transform.localRotation = Quaternion.Euler(vRot);
    }

    // ī�޶� ȸ�� ( ���� ȸ�� )
    public void CameraRotation()
    {
        float mouseY = -Input.GetAxis("Mouse Y");
        m_CamRotX += mouseY * Time.deltaTime * 200;    // -���� ���ü� �ְ� ��������� Ȱ���Ѵ�.

        m_CamRotX = Mathf.Clamp(m_CamRotX, -60, 60);   // ����) vRot.x�� ���� -���� �ȳ��´�.(0������ �ٷ� 360���� �Ѿ)

        Vector3 vRot = m_Camera.transform.localEulerAngles;
        vRot.x = m_CamRotX;

        Quaternion localRotation = Quaternion.Euler(vRot);
        m_Camera.transform.localRotation = localRotation;
    }

    public void Move()
    {
        // ���� ĳ���Ͱ� ���� �ִ°�?
        if (m_Controller.isGrounded)
        {
            //float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            if (yAxis != 0)
            {
                float speed = m_Speed;
                if (yAxis == -1)
                    speed = m_Speed * 0.5f;

                m_MoveDir = transform.forward * speed * yAxis;
                m_Animator.SetFloat("MoveSpeed", 0.2f);
            }
            else
            {
                m_MoveDir = Vector3.zero;
                m_Animator.SetFloat("MoveSpeed", 0.01f);
            }

            // ĳ���� ����
            if (Input.GetButton("Jump"))
                m_MoveDir.y = m_JumpSpeedF;
        }

        // ĳ���Ϳ� �߷� ����.
        m_MoveDir.y -= m_Gravity * Time.deltaTime;

        // ĳ���� ������.
        m_Controller.Move(m_MoveDir * Time.deltaTime);
    }

}
