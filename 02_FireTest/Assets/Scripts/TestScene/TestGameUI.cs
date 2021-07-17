using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameUI : MonoBehaviour
{
    public TestPlayer m_Gun = null;
    public Transform m_trOffstPos = null;

    public GameObject m_prefabTarget;
    public Transform m_TargetParent;

    public float m_CreateDelay = 2.0f;
    [HideInInspector] public bool m_bMoveStart = false;


    [SerializeField] SpriteRenderer m_sprMouseCursor = null;
    float m_fFireDelay = 0;
    bool m_bFire = false;

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
        while (m_bMoveStart)
        {
            CreateTarget();

            yield return new WaitForSeconds(m_CreateDelay); //0.3f
        }
    }


    public void CreateTarget()
    {
        GameObject go = Instantiate(m_prefabTarget, m_TargetParent);
        go.transform.position = m_trOffstPos.position;
        go.transform.localScale = m_prefabTarget.transform.localScale;

        TestTarget kTarget = go.GetComponent<TestTarget>();
        kTarget.Initialize(true);
    }


    // Update is called once per frame
    void Update()
    {
        Update_MouseCursor();

        m_fFireDelay += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            if (m_fFireDelay > 0.2f)
            {
                m_bFire = true;
            }

            if (m_bFire)
            {
                m_Gun.Fire();

                m_fFireDelay = 0;
                m_bFire = false;
            }
        }
    }


    public void SetGameResultEnter()
    {
        m_bMoveStart = false;
        DestroyAllTarget();
        m_Gun.SetIsCanFire(false);
        m_Gun.ResetPosition();
    }


    public void DestroyAllTarget()
    {
        CTarget[] listTarget = m_TargetParent.GetComponentsInChildren<CTarget>();
        for (int i = 0; i < listTarget.Length; i++)
        {
            if (listTarget[i].gameObject != null)
                Destroy(listTarget[i].gameObject, 0.1f);
        }
    }

    void Update_MouseCursor()
    {
        Vector3 vPos = Input.mousePosition;
        Camera kCamera = Camera.main;
        vPos.z = -kCamera.transform.position.z;

        Vector3 vWorld = kCamera.ScreenToWorldPoint(vPos);
        m_sprMouseCursor.transform.position = vWorld;
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
