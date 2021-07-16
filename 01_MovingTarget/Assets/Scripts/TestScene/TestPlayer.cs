using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    //[SerializeField] GameObject m_PrefabBullet = null;      // 총알 프리팹
    //[SerializeField] Transform m_BulletParent = null;       // 총알들 부모 노드
    //public Transform m_BulletStartPos = null;               // 총알 발사 시작점
    [SerializeField] private Animator m_gunAnimator = null;


    private bool m_IsCanFire = false;                       // 사격 가능한가?
    private AudioSource m_Audio = null;

    private Vector3 m_OffsetPos;                        // 총의 초기 위치

    void Awake()
    {
        m_OffsetPos = transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
        m_IsCanFire = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsCanFire)
            RotationGun();
    }

    public void Initialize()
    {
        m_IsCanFire = true;
    }

    public void ResetPosition()
    {
        transform.position = m_OffsetPos;
    }

    public void SetIsCanFire(bool bState)
    {
        m_IsCanFire = bState;
    }

    public void RotationGun()
    {
        Vector3 vMouse = Input.mousePosition;
        vMouse.z = -Camera.main.transform.position.z;
        Vector3 vWorld = Camera.main.ScreenToWorldPoint(vMouse);

        transform.LookAt(vWorld);
    }

    public bool Fire()
    {
        m_gunAnimator.SetTrigger("Fire");
        return RayCastTest2();
    }


    //public void CreateBullet(Vector3 vTargetPos)
    //{
    //    GameObject go = Instantiate(m_PrefabBullet, m_BulletParent);
    //    go.transform.localScale = m_PrefabBullet.transform.localScale;
    //    go.transform.position = m_BulletStartPos.position;

    //    Bullet kBullet = go.GetComponent<Bullet>();
    //    kBullet.Initialize(vTargetPos, 1.0f);
    //}

    public bool RayCastTest()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //CreateBullet(hit.point);
            if( hit.collider.tag == "Target" )
            {
                TestTarget kTarget = hit.collider.GetComponent<TestTarget>();
                kTarget.CheckHit(hit.point);


                //Destroy(hit.collider.gameObject, 0.02f);
            }

            

            m_Audio.Play();

            //Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            //Vector3 vDir = hit.point - m_BulletStartPos.position;
            //Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 3.0f);
            return true;
        }
        return false;
    }

    public class SHitInfo
    {
        public Vector3 vPos;
        public int nScore = 0;

        public SHitInfo(Vector3 v, int score)
        {
            vPos = v;
            nScore = score;
        }

    }

    public bool RayCastTest2()
    {
        //RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray, Mathf.Infinity);
        {
            TestTarget2 kTarget = null;
            List<SHitInfo> list = new List<SHitInfo>();
            for (int i = 0; i < hits.Length; i++ )
            {
                //CreateBullet(hit.point);
                if (hits[i].collider.tag == "Target")
                {
                    kTarget = hits[i].collider.GetComponent<TestTarget2>();

                    SHitInfo kInfo = new SHitInfo(hits[i].point, 100);
                    list.Add(kInfo);
                }
                else if (hits[i].collider.tag == "Target2")
                {
                    kTarget = hits[i].collider.transform.parent.GetComponent<TestTarget2>();

                    SHitInfo kInfo = new SHitInfo(hits[i].point, 70);
                    list.Add(kInfo);
                }
                if (hits[i].collider.tag == "Target3")
                {
                    kTarget = hits[i].collider.transform.parent.GetComponent<TestTarget2>();

                    
                    SHitInfo kInfo = new SHitInfo(hits[i].point, 50);
                    list.Add(kInfo);
                }

            }

            if (list.Count > 0)
            {
                list.Sort((a, b) => { return (a.nScore < b.nScore) ? 1 : -1; });   // 점수가 큰 순으로 정렬
                kTarget.CheckHit2(list[0].vPos, list[0].nScore);
            }

            m_Audio.Play();

            Debug.Log("Hit Count = " + list.Count);

            //Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            //Vector3 vDir = hit.point - m_BulletStartPos.position;
            //Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 3.0f);
            return true;
        }
        return false;
    }



    public bool RayCastTest3()
    {
        int layer1 = LayerMask.NameToLayer("Background");
        int layer2 = LayerMask.NameToLayer("Target");
        int layerMask = (1 << layer1) | (1 << layer2);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            //CreateBullet(hit.point);

            m_Audio.PlayOneShot(m_Audio.clip);

            Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            //Vector3 vDir = hit.point - m_BulletStartPos.position;
            //Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 1.0f);

            return true;
        }
        return false;
    }

}
