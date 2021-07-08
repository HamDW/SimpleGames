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


    void Start()
    {
        //m_Speed = 5.0f;
        //m_JumpSpeedF = 8.0f;
        //m_Gravity = 20.0f;

        m_MoveDir = Vector3.zero;
        m_Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        AnimationControll();
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

            // ���ǵ� ����.
            m_MoveDir *= m_Speed;

            // ĳ���� ����
            if (Input.GetButton("Jump"))
                m_MoveDir.y = m_JumpSpeedF;

        }

        // ĳ���Ϳ� �߷� ����.
        m_MoveDir.y -= m_Gravity * Time.deltaTime;

        // ĳ���� ������.
        m_Controller.Move(m_MoveDir * Time.deltaTime);
    }

    public void AnimationControll()
    {
        if( Input.GetKeyDown(KeyCode.W) )
        {
            m_Animator.SetFloat("MoveSpeed", 0.2f);
            m_Speed = 6.0f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            m_Animator.SetFloat("MoveSpeed", 0.2f);
            m_Speed = 3.0f;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            m_Animator.SetFloat("MoveSpeed", 0.01f);
        }

    }
}
