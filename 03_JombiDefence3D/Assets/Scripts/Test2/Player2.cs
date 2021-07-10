using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 1인칭 시점에서 캐릭터의 좌우 회전 및 상하 회전
//  - 좌우회전 : Player 회전 ( 캐릭터가 상하회전하면 앞,뒤로 누워서 이상하게 보이기 때문에 좌우만 회전한다.)
//  - 상하회전 : Camera 회전

public class Player2 : MonoBehaviour
{
    public float DFIRE_DELAY_MAX = 0.3f;
    public TestGun2 m_Gun = null;
    private PlayerMove2 m_Move = null;

    private float m_fFireTime = 0;          // 발사시간을 계산하기 위한 변수  
    private bool m_bCanFire = false;        // 발사 가능 여부

    void Start()
    {
        m_Move = GetComponent<PlayerMove2>();
        Initialize();
    }

    void Update()
    {
        Update_Fire();
    }


    public void Initialize()
    {
        m_Move.Initialize();      
    }



    public void Fire()
    {
        m_Gun.Fire();
        m_Move.m_Animator.SetTrigger("Attack");
    }


    public void SetIsCanFire(bool bState)
    {
        m_Gun.SetIsCanFire(bState);
    }

    private void Update_Fire()
    {
        m_fFireTime += Time.deltaTime;
        if (m_fFireTime >= DFIRE_DELAY_MAX)
        {
            m_bCanFire = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (m_bCanFire)
            {
                // 0.3초 간격으로 사격을 하자
                this.Fire();
                m_bCanFire = false;
                m_fFireTime = 0;
            }
        }
    }

}
