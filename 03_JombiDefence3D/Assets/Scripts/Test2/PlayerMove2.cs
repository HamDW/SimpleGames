using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    public Animator m_Animator = null;
    public float m_Speed = 2.0f;        // 캐릭터 이동 속도
    public float m_JumpSpeedF = 8.0f;   // 캐릭터 점프시 올라가는 속도.
    public float m_Gravity = 20.0f;     // 캐릭터에게 작용하는 중력. ( 점프시 내려오는 속도 )

    private CharacterController m_Controller = null;    // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 m_MoveDir;                          // 캐릭터의 움직이는 방향.

    public Camera m_Camera;
    private float m_CamRotX = 0;    // 카메라 x축 회전 값

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

    // 플레이어 회전 ( 좌우 회전 )
    public void PlayerRoation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        Vector3 vRot = transform.localEulerAngles;
        vRot.y += mouseX * m_Speed * Time.deltaTime * 100;
        //vRot.x += mouseY * m_Speed * Time.deltaTime * 100;

        transform.localRotation = Quaternion.Euler(vRot);
    }

    // 카메라 회전 ( 상하 회전 )
    public void CameraRotation()
    {
        float mouseY = -Input.GetAxis("Mouse Y");
        m_CamRotX += mouseY * Time.deltaTime * 200;    // -값이 나올수 있게 멤버변수를 활용한다.

        m_CamRotX = Mathf.Clamp(m_CamRotX, -60, 60);   // 주의) vRot.x는 값이 -값이 안나온다.(0도에서 바로 360도로 넘어감)

        Vector3 vRot = m_Camera.transform.localEulerAngles;
        vRot.x = m_CamRotX;

        Quaternion localRotation = Quaternion.Euler(vRot);
        m_Camera.transform.localRotation = localRotation;
    }

    public void Move()
    {
        // 현재 캐릭터가 땅에 있는가?
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

            // 캐릭터 점프
            if (Input.GetButton("Jump"))
                m_MoveDir.y = m_JumpSpeedF;
        }

        // 캐릭터에 중력 적용.
        m_MoveDir.y -= m_Gravity * Time.deltaTime;

        // 캐릭터 움직임.
        m_Controller.Move(m_MoveDir * Time.deltaTime);
    }

}
