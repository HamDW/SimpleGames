using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
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

        m_MoveDir = Vector3.zero;
        m_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerRoation();
        Move();
        AnimationControll();
    }


    public void PlayerRoation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = -Input.GetAxis("Mouse Y");

        Vector3 vRot = transform.localEulerAngles;
        vRot.y += mouseX * m_Speed * Time.deltaTime * 200;

        transform.localRotation = Quaternion.Euler(vRot);
    }

    public void Move()
    {
        // ���� ĳ���Ͱ� ���� �ִ°�?
        if (m_Controller.isGrounded)
        {
            // ��, �Ʒ� ������ ����. 
            m_MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // ���͸� ���� ��ǥ�� ���ؿ��� ���� ��ǥ�� �������� ��ȯ�Ѵ�.
            //m_MoveDir = transform.TransformDirection(m_MoveDir);

            m_MoveDir = transform.forward;
            // ���ǵ� ����.
            m_MoveDir *= m_Speed;

            // ĳ���� ����
            if (Input.GetButton("Jump"))  
                m_MoveDir.y = m_JumpSpeedF;

        }

        // ĳ���Ϳ� �߷� ����.
        m_MoveDir.y -= m_Gravity * Time.deltaTime;

        if (m_bMove)
        {
            // ĳ���� ������.
            m_Controller.Move(m_MoveDir * Time.deltaTime);
        }
    }

    public void AnimationControll()
    {
        


        if( Input.GetKeyDown(KeyCode.W) )
        {
            m_Animator.SetFloat("MoveSpeed", 0.2f);
            m_Speed = 6.0f;
            m_bMove = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_Animator.SetFloat("MoveSpeed", 0.2f);
            m_Speed = -3.0f;
            m_bMove = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            m_Animator.SetFloat("MoveSpeed", 0.01f);
            m_bMove = false;
        }

        if( Input.GetButtonDown("Jump"))
        {
            m_bMove = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            m_bMove = false;
        }

    }
}
