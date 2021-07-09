using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public const float DFIRE_DELAY_MAX = 0.3f;

    public TestGun m_Gun = null;
    float m_fFireDelay = 0;
    bool m_bFire = false;

    public float m_Speed = 5.0f;      // ĳ���� ������ ���ǵ�.
    public float m_JumpSpeedF = 8.0f; // ĳ���� ���� ��.
    public float m_Gravity = 20.0f;    // ĳ���Ϳ��� �ۿ��ϴ� �߷�.
    public Animator m_Animator = null;
    
    private CharacterController m_Controller = null; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ� �ݶ��̴�.
    private Vector3 m_MoveDir;                          // ĳ������ �����̴� ����.

    public bool m_bMove = false;

    void Start()
    {
        //m_Speed = 5.0f;
        //m_JumpSpeedF = 8.0f;
        //m_Gravity = 20.0f;
        FPSCamera.LockedMouse();

        //m_MoveDir = Vector3.zero;
        m_Controller = GetComponent<CharacterController>();

        Initialize();
    }

    void Update()
    {
        PlayerRoation();
        Move();
        Update_Fire();
    }


    public void PlayerRoation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");

        Vector3 vRot = transform.localEulerAngles;
        vRot.y += mouseX * m_Speed * Time.deltaTime * 100;
        //vRot.x += mouseY * m_Speed * Time.deltaTime * 100;
        

        transform.localRotation = Quaternion.Euler(vRot);
    }

    public void Move()
    {
        // ���� ĳ���Ͱ� ���� �ִ°�?
        if (m_Controller.isGrounded)
        {
            //float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");

            if (yAxis != 0 )
            {
                float speed = m_Speed;
                if (yAxis == -1)
                    speed = m_Speed * 0.5f;

                m_MoveDir = transform.forward * speed * yAxis;
                m_Animator.SetFloat("MoveSpeed",0.2f);
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



    public void Initialize()
    {
       // m_Gun.Initialize();
    }

    public void Fire()
    {
        m_Gun.Fire();
    }


    public void SetIsCanFire(bool bState)
    {
        m_Gun.SetIsCanFire(bState);
    }

    // Update is called once per frame


    private void Update_Fire()
    {
        m_fFireDelay += Time.deltaTime;
        if (m_fFireDelay >= DFIRE_DELAY_MAX)
        {
            m_bFire = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_bFire)
            {
                // 0.3�� �������� ����� ����
                this.Fire();
                m_bFire = false;
                m_fFireDelay = 0;
            }
        }
    }

}
