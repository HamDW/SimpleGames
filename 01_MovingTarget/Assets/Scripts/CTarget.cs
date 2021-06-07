using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTarget : MonoBehaviour
{
    public bool m_bMove = false;
    public float m_Speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(bool bMove)
    {
        m_bMove = bMove;
    }




    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * m_Speed * Time.deltaTime * 100);
    }
}
