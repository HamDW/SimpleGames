using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Vector3 m_vDir = Vector3.zero;    // 총알 이동 방향
    //private Vector3 m_vTargetPos = Vector3.zero;
    float m_Speed = 1.0f;

    void Start()
    {
        Destroy(gameObject, 5.0f);
    }


    public void Initialize(Vector3 targetPos, float fSpeed)
    {
        m_Speed = fSpeed;

        RotationToTarget(targetPos);
        m_vDir = targetPos - transform.position;
        m_vDir.Normalize();

    }

    // Update is called once per frame
    void Update()
    {
        Move(m_vDir);
    }

    // 타겟을 향해 회전하기
    void RotationToTarget(Vector3 vTargetPos)
    {
        transform.LookAt(vTargetPos);
    }

    // 이동
    public void Move(Vector3 vDir)
    {
        transform.Translate(vDir * m_Speed * Time.deltaTime * 100, Space.World);
    }
}
