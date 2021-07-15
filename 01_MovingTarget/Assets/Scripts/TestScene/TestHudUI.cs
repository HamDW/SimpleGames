using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestHudUI : MonoBehaviour
{
    //[SerializeField] Text m_txtTime = null;
    [SerializeField] Text m_txtScore = null;
    [SerializeField] Text m_txtScore2 = null;

    [SerializeField] Image m_MouseCursor = null;
    [SerializeField] Canvas m_RootCanvas;
    public bool m_IsFPS = false;


    void Start()
    {
        //ShowTopUI(false);
    }

    // √ ±‚»≠
    public void Initialize()
    {
        ShowTopUI(true);
        //PrintDurationTime();
    }

    public void ShowTopUI(bool bShow)
    {
        //if (m_txtTime != null)
        //    m_txtTime.gameObject.SetActive(bShow);

        if (m_txtScore != null)
            m_txtScore.gameObject.SetActive(bShow);
    }

    //public void PrintDurationTime()
    //{
    //    if (m_txtTime != null)
    //    {
    //        GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
    //        int nMinute = (int)(kGameInfo.durationTime / 60);
    //        int nSecond = (int)(kGameInfo.durationTime % 60);

    //        string sTime = string.Format("{0:00}:{1:00}", nMinute, nSecond);

    //        m_txtTime.text = sTime;
    //    }
    //}


    void Update()
    {
        // PrintDurationTime();
        PrintScore();

        if (!m_IsFPS)
            Update_MouseCursor();
    }

     

    public void PrintScore()
    {
        int nScore = GameMgr.Inst.GetScore();
        m_txtScore.text = "Score : " + nScore;
    }

    public void PrintScore2( int nScore )
    {
        m_txtScore2.text = "Hit : " + nScore;
    }



    public void SetGameResultEnter()
    {
        ShowTopUI(false);
        //OpenResultUI();
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
