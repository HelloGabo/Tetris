//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\UI\UIStart.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：UI基类
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public class UIStart : UIBase, CustomButtonListEvent
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public override string mName { get { return "UIStart"; } }

    private CustomButtonList m_ButtonList;

    private enum ButtonID
    { 
        Start = 0,
        Info,
        Welcome,
    }

    public UIStart()
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
    public override bool OnLoad( GameObject root )
    {
        base.OnLoad(root);

        string[] buttonNames = { "Bg/Start", "Bg/Info", "Bg/Info2" };

        CustomButton customButton = null;
        for(int i=0; i<buttonNames.Length; ++i)
        {
            customButton = FindChild(buttonNames[i]).GetComponent<CustomButton>();
            if(customButton != null)
            {
                customButton.AddTapEvent(OnButtonTap);
                m_ButtonList.Push(customButton);
            }
        }

        return true;
    }

    public override void OnKeyDownW() { m_ButtonList.Pre(); }
    public override void OnKeyDownS() { m_ButtonList.Next(); }
    public override void OnKeyDownReturn() { m_ButtonList.Tap(); }

    public void OnTap(int nID)
    {
        if (GameStateManager.mInstance.mNextStateID != GameStateID.Start)
            return;

        switch((ButtonID)nID)
        {
            case ButtonID.Start:
                GameStateManager.mInstance.mNextStateID = GameStateID.Play;
                break;

            case ButtonID.Info:
                UIManager.mInstance.Show<UIHelp>("UIHelp");
                break;

            case ButtonID.Welcome:
                UIManager.mInstance.Show<UIWelcome>("UIWelcome");
                break;
        }
    }

    private void OnButtonTap(object sender)
    {
        var customButton = sender as CustomButton;
        if (customButton != null)
            OnTap(customButton.mID);
    }
}
