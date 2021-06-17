using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public CGun m_Gun = null;
    public Transform m_trOffstPos = null;

    public GameObject m_prefabTarget;
    public Transform m_TargetParent;

    public float m_FireDelay = 2.0f;
    [HideInInspector] public bool m_bMoveStart = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize()
    {
        m_Gun.Initialize();

        // 타겟 생성 시작
        m_bMoveStart = true;
        //StartCoroutine(EnumFunc_TargetCreate());
    }


    IEnumerator EnumFunc_TargetCreate()
    {
        while( m_bMoveStart )
        {
            CreateTarget();
            
            yield return new WaitForSeconds(m_FireDelay); //0.3f
        }
    }


    public void CreateTarget()
    {
        GameObject go = Instantiate(m_prefabTarget, m_TargetParent);
        go.transform.position = m_trOffstPos.position;
        go.transform.localScale = m_prefabTarget.transform.localScale;

        CTarget kTarget = go.GetComponent<CTarget>();
        kTarget.Initialize(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameMgr.Inst.gameScene.m_BattleFSM.IsGameState())
            return;

        if( Input.GetMouseButtonDown(0))
        {
            m_Gun.Fire();
        }
    }

  
    public void SetGameResultEnter()
    {
        m_bMoveStart = false;
        DestroyAllTarget();
        m_Gun.SetIsCanFire(false);
    }


    public void DestroyAllTarget()
    {
        //CTarget[] listTarget = m_TargetParent.GetComponentsInChildren<CTarget>();
        //for( int i = 0; i < listTarget.Length; i++ )
        //{
        //    if(listTarget[i].gameObject != null)
        //        Destroy(listTarget[i].gameObject, 0.1f);
        //}
    }


    //public void RayCastTestForMouseCursor()
    //{
    //    RaycastHit hit;
    //    Vector3 vPos = Input.mousePosition;
    //    //vPos.z = -Camera.main.transform.position.z;
    //    Ray ray = Camera.main.ScreenPointToRay(vPos);
    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        m_Gun.Fire();

    //        Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
    //        Vector3 vDir = hit.point - m_Gun.m_BulletStartPos.position;
    //        Debug.DrawRay(m_Gun.m_BulletStartPos.position, vDir, Color.red, 3.0f);
    //    }
    //}


}
