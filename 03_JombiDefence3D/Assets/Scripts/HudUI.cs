using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] StartMenuDlg m_MenuDlg = null;
    [SerializeField] ResultGameDlg m_ResultDlg = null;

    [SerializeField] Text m_txtTime = null;
    [SerializeField] Text m_txtCount = null;
    [SerializeField] Text m_txtScore = null;

    [SerializeField] Image m_MouseCursor = null;
    [SerializeField] Canvas m_RootCanvas;

    private bool m_isFPS = false;

    void Start()
    {
        ShowTopUI(false);
        OpenMenuDlg();
        m_MouseCursor.gameObject.SetActive(false);
    }

    // ÃÊ±âÈ­
    public void Initialize()
    {
        
        ShowTopUI(true);
        PrintDurationTime();
        m_MouseCursor.gameObject.SetActive(true);
    }

    public void OpenResultUI()
    {
        m_ResultDlg.OpenUI();
    }
    public void OpenMenuDlg()
    {
        m_MenuDlg.OpenUI();
    }
    public void ShowTopUI(bool bShow)
    {
        if (m_txtTime != null)
            m_txtTime.gameObject.SetActive(bShow);

        if( m_txtScore != null)
            m_txtScore.gameObject.SetActive(bShow);
    }

    public void PrintDurationTime()
    {
        if (m_txtTime != null)
        {
            GameInfo kGameInfo = GameMgr.Inst.m_GameInfo;
            int nMinute = (int)(kGameInfo.durationTime / 60);
            int nSecond = (int)(kGameInfo.durationTime % 60);

            string sTime = string.Format("{0:00}:{1:00}", nMinute, nSecond);

            m_txtTime.text = sTime;
        }
    }


    void Update()
    {
        PrintDurationTime();

        if (!GameMgr.Inst.gameScene.m_BattleFSM.IsGameState())
            return;

        if (!GameMgr.Inst.IsFPS )
        {
            Update_MouseCursor();
        }
    }


    public void ShowTextCount(bool bShow)
    {
        m_txtCount.gameObject.SetActive(bShow);
    }

    public void PrintScore()
    {
        int nScore = GameMgr.Inst.GetScore();
        m_txtScore.text = "Score : " + nScore;
    }


    public void StartReadyCount()
    {
        ShowTextCount(true);
        //StartCoroutine("EnumFunc_ReadyCount", 1.0f);
        StartCoroutine(EnumFunc_ReadyCount(1.0f));

    }

    IEnumerator EnumFunc_ReadyCount(float fDelay)
    {
        int nCount = 3;
        while (nCount >= 0)
        {
            if (nCount == 0)
                m_txtCount.text = "Start";
            else
                m_txtCount.text = nCount.ToString();

            --nCount;
            yield return new WaitForSeconds(fDelay);
        }

        this.ShowTextCount(false);
        GameMgr.Inst.gameScene.m_BattleFSM.SetGameState();

        yield return null;
    }



    public void SetGameResultEnter()
    {
        m_MouseCursor.gameObject.SetActive(false);
        ShowTopUI(false);
        OpenResultUI();
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

    void Update_MouseCursor2()
    {
        Vector3 vPos = Input.mousePosition;
        Camera kCamera = Camera.main;
        vPos.z = -kCamera.transform.position.z;

        Vector3 vWorld = kCamera.ScreenToWorldPoint(vPos);
        m_MouseCursor.transform.position = vWorld;
    }

}
