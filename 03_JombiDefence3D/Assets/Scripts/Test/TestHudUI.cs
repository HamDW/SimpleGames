using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHudUI : MonoBehaviour
{
    [SerializeField] Image m_MouseCursor = null;
    [SerializeField] Canvas m_RootCanvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Initialize()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Update_MouseCursor();
    }

    void Update_MouseCursor()
    {
        Vector3 vPos = Input.mousePosition;
        Camera kCamera = m_RootCanvas.worldCamera;

        Vector3 vWorld;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
                  m_RootCanvas.transform as RectTransform, vPos, kCamera, out vWorld);

        m_MouseCursor.transform.position = vWorld;
    }
}
