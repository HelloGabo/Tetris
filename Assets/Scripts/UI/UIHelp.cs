//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\UI\UIHelp.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：帮助UI
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public class UIHelp : UIBase
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public override string mName { get { return "UIHelp"; } }

    public UIHelp()
    { 
        
    }

    public override void OnKeyDownEscape() 
    {
        UIManager.mInstance.Hide(mName);
    }
}