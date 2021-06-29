using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAutoDestroy : MonoBehaviour
{
    [SerializeField] bool m_IsOnlyHide = false;   // ¼û±â±â
    [SerializeField] float m_DestroyDelay = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, m_DestroyDelay);

        if (m_IsOnlyHide)
        {
            Invoke("HideObject", m_DestroyDelay);
        }
    }


    void HideObject()
    {
        gameObject.SetActive(false);
    }

}
