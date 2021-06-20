#define DUSE_TEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator m_Animator = null;
    private Transform m_Target = null;
    private Vector3 m_vDir;

    private string m_Name;
    private int m_HP = 0;
    private float m_Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        if( m_Animator == null)
            m_Animator = GetComponent<Animator>();

        Destroy(gameObject, 30.0f);
        
    }

    public void Initialize(CZombiData.SDataInfo kData, Transform target)
    {
        m_Target = target;
        m_HP = kData.m_Hp;
        m_Speed = kData.m_Speed;
        m_Name = kData.m_Name;

        m_vDir = m_Target.transform.position - transform.position;
        m_vDir.Normalize();
        transform.LookAt(target);

        m_Animator.SetFloat("MoveSpeed", 0.2f);
    }


    private void Move()
    {
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Walk"))
            transform.Translate(m_vDir * m_Speed * Time.deltaTime * 1, Space.World);
    }


    // Update is called once per frame
    void Update()
    {
        Move();

#if DUSE_TEST

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            m_Animator.SetFloat("MoveSpeed", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            m_Animator.SetFloat("MoveSpeed", 0.01f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            m_Animator.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            m_Animator.SetTrigger("Reset");
        }

#endif

    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if( collision.gameObject.tag.Equals("Bullet"))
    //    {
    //        m_Animator.SetTrigger("Attack");

    //        Debug.Log("Collision  : attack ani start");

    //        Destroy(collision.gameObject, 0.05f);

    //        m_HP -= GameInfo.DATTACK_VAULE;
    //        if( m_HP <= 0)
    //        {
    //            GameMgr.Inst.m_GameInfo.AddScore(1);

    //            m_Animator.SetTrigger("Dead");
    //            Destroy(gameObject, 2.0f);
    //        }

    //    }
            
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            //m_Animator.ResetTrigger(1);
            

            Debug.Log("Collision  : attack ani start");

            Destroy(other.gameObject, 0.01f);

            m_HP -= GameInfo.DATTACK_VAULE;
            if (m_HP <= 0)
            {
                GameMgr.Inst.m_GameInfo.AddScore(1);

                m_Animator.SetTrigger("Dead");
                Destroy(gameObject, 2.0f);
            }
            else
            {
                if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Damage"))
                    m_Animator.SetTrigger("Damage");
            }

        }
    }

}
