using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGun : MonoBehaviour
{
    [SerializeField] private Animator m_gunAnimator = null;
    [SerializeField] Transform m_EffectParent = null;

    public bool m_isGameStart = false;                // 총 회전 여부
    protected AudioSource m_Audio = null;


    private void Awake()
    {
        m_isGameStart = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isGameStart)
            RotationGun();
    }

    public void Initialize()
    {
        m_isGameStart = true;
    }

    public void SetIsCanFire(bool bState)
    {
        m_isGameStart = bState;
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
        return RayCastTest();
    }

    public virtual bool RayCastTest()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if( Physics.Raycast(ray, out hit, 1000))
        { 
            CreateDamageEffect(ray.direction, hit.point);
            if( hit.collider.gameObject.tag == "Enemy")
            {
                Enemy kEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                kEnemy.AddDamage(GameInfo.DATTACK_VAULE);
            }

            m_Audio.Play();

            //Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
            //Vector3 vDir = hit.point - m_BulletStartPos.position;
            //Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 3.0f);
            return true;
        }
        return false;
    }

    

    public void CreateDamageEffect(Vector3 rayDir, Vector3 vPos )
    {
       GameObject goPrefab = Resources.Load<GameObject>("Prefabs/FX/FxDamage");

        GameObject go = Instantiate(goPrefab, m_EffectParent);
        vPos -= (rayDir * 0.1f);        // 약간 앞쪽에 출력한다.
        go.transform.position = vPos;
        go.transform.localScale = goPrefab.transform.localScale;

    }


    //public bool RayCastTest2()
    //{
    //    int layer1 = LayerMask.NameToLayer("Background");
    //    int layer2 = LayerMask.NameToLayer("Target");
    //    int layerMask = (1 << layer1) | (1 << layer2);

    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, layerMask))
    //    {
    //        CreateDamageEffect(hit.point);
    //        //CreateBullet(hit.point);

    //        m_Audio.PlayOneShot(m_Audio.clip);

    //        Debug.LogFormat("hit point = ({0}, {1}, {2})", hit.point.x, hit.point.y, hit.point.z);
    //        Vector3 vDir = hit.point - m_BulletStartPos.position;
    //        Debug.DrawRay(m_BulletStartPos.position, vDir, Color.red, 1.0f);

    //        return true;
    //    }
    //    return false;
    //}

    //public Vector3 ScreenToWorldPos()
    //{
    //    Vector3 vMouse = Input.mousePosition;
    //    vMouse.z = -Camera.main.transform.position.z;
    //    return Camera.main.ScreenToWorldPoint(vMouse);
    //}

}
