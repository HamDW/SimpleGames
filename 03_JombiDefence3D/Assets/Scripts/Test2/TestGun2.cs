using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun2 : CGun
{
    // Start is called before the first frame update
    void Start()
    {
        m_Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool RayCastTest()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            CreateDamageEffect( ray.direction, hit.point);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                TestEnemy kEnemy = hit.collider.gameObject.GetComponent<TestEnemy>();
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

}
