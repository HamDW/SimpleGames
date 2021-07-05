#define DUSE_TEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    public Animator m_Animator = null;
    public FXParticle m_FxDamage = null;

    public Transform m_Target = null;
    private Vector3 m_vDir;


    private string m_Name = "Zombi_01";
    public int m_HP = 1000;
    public float m_Speed = 0.5f;

    private bool m_bDead = false;
    private bool m_bAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Animator == null)
            m_Animator = GetComponent<Animator>();

       // Destroy(gameObject, 60.0f);

        if (m_Target != null)
        {
            m_vDir = m_Target.transform.position - transform.position;
            m_vDir.Normalize();
            transform.LookAt(m_Target);
        }

        Initialize();
    }



    public void Initialize()
    {
        //m_Target = target;
        //m_HP = 1000;
        //m_Speed = 0.5f;
        //m_Name = "Zombi";

        m_vDir = m_Target.transform.position - transform.position;
        m_vDir.Normalize();
        transform.LookAt(m_Target);

        m_Animator.SetFloat("MoveSpeed", 0.2f);
    }


    private void Move()
    {
        if (IsAni_Walk())
            transform.Translate(m_vDir * m_Speed * Time.deltaTime, Space.World);

        if (IsAni_Damage())
            transform.Translate(-m_vDir * m_Speed * Time.deltaTime * 3, Space.World);       // 총 맞으면 뒤로 물러나기

    }

    public void AddDamage(int nDamage)
    {
        if (m_bDead)
            return;

        m_HP -= nDamage;

        if (m_HP <= 0)
        {
            m_bDead = true;
            GameMgr.Inst.m_GameInfo.AddScore(1);

            m_Animator.SetTrigger("Dead");
            //GameMgr.Inst.gameScene.m_HudUI.PrintScore();

            Debug.Log("4.Dead.....!");


            Destroy(gameObject, 2.5f);
        }
        else
        {
            m_Animator.SetTrigger("Damage");
            Debug.Log("3.Damgae.....!");
        }
    }

    private void CheckPlayer()
    {
        int nLayer = LayerMask.NameToLayer("Player");
        int nLayerMask = 1 << nLayer;
        Collider[] cols = Physics.OverlapSphere(transform.position, 1.1f, nLayerMask);

        if (cols.Length > 0)
        {
            if (IsAni_Idle())
            {
                if (!m_bAttack)
                {
                    if (GameMgr.Inst.m_GameInfo.AddDamage(GameInfo.DENEMY_ATTACK) == false)
                    {
                        GameMgr.Inst.gameScene.m_BattleFSM.SetResultState();
                        return;
                    }

                    m_bAttack = true;
                    m_Animator.SetTrigger("Attack");
                    Debug.Log("5.Attack.....!");


                    // 공격 애니메이션 길이 구하기
                    AnimatorClipInfo[] kClipInfos = m_Animator.GetCurrentAnimatorClipInfo(0);
                    float fLength = kClipInfos[0].clip.length;

                    Invoke("Callback_Attack", fLength);
                }
            }
            m_Animator.SetFloat("MoveSpeed", 0.01f);
        }
        else
        {
            if (IsAni_Attack())
                m_Animator.SetFloat("MoveSpeed", 0.01f);
            else
                m_Animator.SetFloat("MoveSpeed", 0.2f);
        }
    }

    void Callback_Attack()
    {
        m_bAttack = false;
    }

    public bool IsAni_Attack()
    {
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Attack");
    }

    public bool IsAni_Walk()
    {
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Walk");
    }

    public bool IsAni_Damage()
    {
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Damage");
    }


    public bool IsAni_Idle()
    {
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Idle");
    }


    // Update is called once per frame
    void Update()
    {
        //CheckPlayer();
        //Move();

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
            m_Animator.SetTrigger("Damage");
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            m_Animator.SetTrigger("Dead");
        }

#endif

    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {

            Debug.Log("Collision  : attack ani start");

            Destroy(other.gameObject, 0.01f);
            m_FxDamage.Play();


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
