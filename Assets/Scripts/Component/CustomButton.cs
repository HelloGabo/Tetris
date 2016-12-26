//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\Component\CustomButton.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：自定义按钮
// 说明：
//
//////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public class CustomButton : MonoBehaviour 
{
    public int mID;
    public TweenScale mTweenScale;

    public delegate void TapAction(object sender);
    private event TapAction OnTapEvent;

    public void AddTapEvent(TapAction tapAction)
    {
        OnTapEvent += tapAction;
    }

    public void Select(bool bSelect)
    {
        if (bSelect)
        {
            mTweenScale.ResetToBeginning();
            mTweenScale.PlayForward();
        }
        else
        {
            mTweenScale.PlayReverse();
        }
    }
    
    #region NGUI callback
    private void OnPress(bool pressed)
    {
        Select(pressed);
    }

    private void OnClick()
    {
        if (OnTapEvent != null)
            OnTapEvent.Invoke(this);
    }
    #endregion
}
