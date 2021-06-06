using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuDlg : MonoBehaviour
{
    [SerializeField] Button m_btnStart = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnStart.onClick.AddListener(OnClicked_Start);
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public void OnClicked_Start()
    {
        GameScene kGameScene = GameMgr.Inst.gameScene;
        kGameScene.m_BattleFSM.SetReadyState();

        CloseUI();
    }




}
