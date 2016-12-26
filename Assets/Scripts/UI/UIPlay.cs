using UnityEngine;
using System.Collections;

public class UIPlay : UIBase, CustomButtonListEvent
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public override string mName { get { return "UIPlay"; } }

    private CustomButtonList m_ButtonList;

    private UILabel m_ScoreLabel;
    private UISprite m_Background;

    private enum ButtonID
    {
        Continue = 0,
        Quit,
    }

    public UIPlay()
    {
    }

    /// <summary>
    /// 逻辑初始化
    /// 创建时调用一次
    /// </summary>
    /// <returns></returns>
    public override bool Init()
    {
        m_ButtonList = new CustomButtonList();
        m_ButtonList.Init(this);

        return true;
    }

    /// <summary>
    /// 释放操作
    /// </summary>
    public override void Release()
    {

    }

    /// <summary>
    /// UI组件初始化
    /// 创建时调用一次
    /// </summary>
    public override bool OnLoad(GameObject root)
    {
        base.OnLoad(root);

        m_ScoreLabel = FindChild("Score/ScoreLabel").GetComponent<UILabel>();
        m_Background = FindChild("Bg").GetComponent<UISprite>();

        m_ScoreLabel.text = TetrisManager.mInstance.mScore.ToString();

        string[] buttonNames = { "Continue", "Quit" };

        CustomButton customButton = null;
        for (int i = 0; i < buttonNames.Length; ++i)
        {
            customButton = FindChild(buttonNames[i]).GetComponent<CustomButton>();
            if (customButton != null)
            {
                m_ButtonList.Push(customButton);
            }
        }

        m_ButtonList.SetVisible(false);
        m_Background.gameObject.SetActive(false);
        TetrisManager.mInstance.OnScoreChangeEvent += OnScoreChange;
        m_ScoreLabel.text = "0";

        return true;
    }

    public override void OnHide()
    {
        TetrisManager.mInstance.OnScoreChangeEvent -= OnScoreChange;
        base.OnHide();
    }

    private void OnScoreChange(uint score)
    {
        m_ScoreLabel.text = score.ToString();
    }

    public override void OnKeyDownW()
    {
        if (TetrisManager.mInstance.mStatus == TSMStatus.Pause)
            m_ButtonList.Pre();
        else if (TetrisManager.mInstance.mStatus == TSMStatus.Play)
            TetrisManager.mInstance.Translate();
    }

    public override void OnKeyDownS()
    {
        if (TetrisManager.mInstance.mStatus == TSMStatus.Pause)
            m_ButtonList.Next();
        else if (TetrisManager.mInstance.mStatus == TSMStatus.Play)
            TetrisManager.mInstance.MoveDown();
    }

    public override void OnKeyDownA()
    {
        TetrisManager.mInstance.MoveLeft();
    }

    public override void OnKeyDownD()
    {
        TetrisManager.mInstance.MoveRight();
    }

    public override void OnKeyDownReturn() { m_ButtonList.Tap(); }

    public override void OnKeyDownSpace()
    {
        TetrisManager.mInstance.Drop();
    }

    public override void OnKeyDownEscape()
    {
        TetrisManager.mInstance.mStatus = TSMStatus.Pause;
        m_ButtonList.SetVisible(true);
        m_Background.gameObject.SetActive(true);
    }

    public void OnTap(int nID)
    {
        if (GameStateManager.mInstance.mNextStateID != GameStateID.Play)
            return;

        switch ((ButtonID)nID)
        {
            case ButtonID.Continue:
                m_ButtonList.SetVisible(false);
                m_Background.gameObject.SetActive(false);
                TetrisManager.mInstance.mStatus = TSMStatus.Play;
                break;

            case ButtonID.Quit:
                GameStateManager.mInstance.mNextStateID = GameStateID.Start;
                break;
        }
    }
}
