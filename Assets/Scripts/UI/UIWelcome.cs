//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\UI\UIWelcome.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：欢迎UI
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public class UIWelcome : UIBase
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public override string mName { get { return "UIWelcome"; } }

    public UIWelcome()
    { 
        
    }

    public override void OnKeyDownEscape() 
    {
        UIManager.mInstance.Hide(mName);
    }
}