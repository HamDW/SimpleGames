using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float m_Speed = 5.0f;      // 캐릭터 움직임 스피드.
    public float m_JumpSpeedF = 8.0f; // 캐릭터 점프 힘.
    public float m_Gravity = 20.0f;    // 캐릭터에게 작용하는 중력.
    public Animator m_Animator = null;

    private CharacterController m_Controller = null; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private Vector3 m_MoveDir;                          // 캐릭터의 움직이는 방향.


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
        // 현재 캐릭터가 땅에 있는가?
        if (m_Controller.isGrounded)
        {
            // 위, 아래 움직임 셋팅. 
            m_MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환한다.
            //m_MoveDir = transform.TransformDirection(m_MoveDir);

            // 스피드 증가.
            m_MoveDir *= m_Speed;

            // 캐릭터 점프
            if (Input.GetButton("Jump"))
                m_MoveDir.y = m_JumpSpeedF;

        }

        // 캐릭터에 중력 적용.
        m_MoveDir.y -= m_Gravity * Time.deltaTime;

        // 캐릭터 움직임.
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
