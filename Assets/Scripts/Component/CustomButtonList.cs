//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\Component\CustomButtonList.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：自定义按钮列表
// 说明：
//
//////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public interface CustomButtonListEvent
{
    void OnTap(int nID);
}

public class CustomButtonList
{
    private List<CustomButton> m_ButtonList;

    private int m_nCurIndex;
    private int m_nDefaultIndex = -100;

    private CustomButtonListEvent m_EventCallback;

    public CustomButtonList()
    {
        m_ButtonList = new List<CustomButton>();

        m_nCurIndex = m_nDefaultIndex;
    }

    public void Init(CustomButtonListEvent callback)
    {
        m_EventCallback = callback;
    }

    public void Push(CustomButton button)
    {
        m_ButtonList.Add(button);
    }

    public void Next()
    {
        if (m_nCurIndex == m_nDefaultIndex)
        {
            m_nCurIndex = 0;

            m_ButtonList[m_nCurIndex].Select(true);
        }
        else
        {
            m_ButtonList[m_nCurIndex].Select(false);

            m_nCurIndex++;

            if (m_nCurIndex >= m_ButtonList.Count)
            {
                m_nCurIndex = 0;
            }

            m_ButtonList[m_nCurIndex].Select(true);
        }
    }

    public void Pre()
    {
        if (m_nCurIndex == m_nDefaultIndex)
        {
            m_nCurIndex = m_ButtonList.Count - 1;

            m_ButtonList[m_nCurIndex].Select(true);
        }
        else
        {
            m_ButtonList[m_nCurIndex].Select(false);

            m_nCurIndex --;
            if (m_nCurIndex < 0)
            {
                m_nCurIndex = m_ButtonList.Count - 1;
            }

            m_ButtonList[m_nCurIndex].Select(true);
        }
    }

    public void Tap()
    {
        if (m_nCurIndex == m_nDefaultIndex)
            return;

        m_EventCallback.OnTap(m_ButtonList[m_nCurIndex].mID);
    }
}
