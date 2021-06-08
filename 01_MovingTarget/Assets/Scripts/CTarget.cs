using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTarget : MonoBehaviour
{
    public bool m_bMove = false;
    public float m_Speed = 1.0f;
    public FXParticle m_Explsion = null;

    public bool m_IsDead = false;           // 충돌 2번 되는것 방지

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10.0f);
    }

    public void Initialize(bool bMove)
    {
        m_bMove = bMove;
    }


    // Update is called once per frame
    void Update()
    {
        if(m_bMove )
            transform.Translate(Vector3.left * m_Speed * Time.deltaTime * 10);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //if (!m_IsDead)
            {
                m_Explsion.Play();
                GameMgr.Inst.AddScore(1);
                GameMgr.Inst.gameScene.m_HudUI.PrintScore();

                m_IsDead = true;
                Destroy(collision.gameObject, 0.001f);
                Destroy(gameObject, 0.001f);
            }
        }
    }




}
