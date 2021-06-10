using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFrame : MonoBehaviour
{
    public const float DBASE_SPEED = 10;
    public bool m_IsRotate = true;
    public float m_Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        if (m_IsRotate){
            RotateObject();
        }
    }
    public void RotateObject()
    {
        float fSpeed = m_Speed * Time.deltaTime * DBASE_SPEED;
        transform.Rotate(Vector3.up * fSpeed);
        //transform.Rotate(0, fSpeed, 0);
    }

    public void SetIsRotate(bool bRotate)
    {
        m_IsRotate = bRotate;
    }
}
