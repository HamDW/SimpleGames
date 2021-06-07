using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGun : MonoBehaviour
{
    [SerializeField] GameObject m_PrefabBullet = null;      // ÃÑ¾Ë ÇÁ¸®ÆÕ
    [SerializeField] Transform m_BulletParent = null;       // ÃÑ¾Ëµé ºÎ¸ð ³ëµå
    public Transform m_BulletStartPos = null;     // ÃÑ¾Ë ¹ß»ç ½ÃÀÛÁ¡

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 vTargetPos)
    {
        GameObject go = Instantiate(m_PrefabBullet, m_BulletParent);
        go.transform.localScale = m_PrefabBullet.transform.localScale;
        go.transform.localPosition = m_BulletStartPos.localPosition;

        Bullet kBullet = go.GetComponent<Bullet>();
        kBullet.Initialize(vTargetPos, 10.0f);
    }
}
