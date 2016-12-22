//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\UI\UIBase.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：UI基类
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;

public class UIBase : IKeyDownEvent
{
    /// <summary>
    /// 窗口名称
    /// </summary>
    public virtual string mName { get { return "UIBase"; } }

    /// <summary>
    /// 根UI物件
    /// </summary>
    public GameObject mRoot { get; private set; }

    public UIBase()
    { 
        
    }

    /// <summary>
    /// 逻辑初始化
    /// 创建时调用一次
    /// </summary>
    /// <returns></returns>
    public virtual bool Init()
    {
        return true;
    }

    /// <summary>
    /// 释放操作
    /// </summary>
    public virtual void Release()
    { 
        
    }

    /// <summary>
    /// UI组件初始化
    /// 创建时调用一次
    /// </summary>
    public virtual bool OnLoad( GameObject root )
    {
        mRoot = root;

        return true;
    }

    /// <summary>
    /// 查找一个子对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Transform FindChild(string path)
    {
        return mRoot.transform.FindChild(path);
    }

    /// <summary>
    /// 设置窗口深度
    /// </summary>
    protected int m_nRealDepth;
    public virtual int mDepth
    {
        set 
        {
            mRoot.GetComponent<UIPanel>().depth = value;
            m_nRealDepth = value;
        }

        get
        {
            return m_nRealDepth;
        }
    }

    public virtual void OnShow()
    { 
        Main.mInstance.Push(this);
    }

    public virtual void OnHide()
    {
        Main.mInstance.Pop();
    }

    public virtual void OnKeyDownW() {  }
    public virtual void OnKeyDownS() {  }
    public virtual void OnKeyDownA() { }
    public virtual void OnKeyDownD() { }
    public virtual void OnKeyDownSpace() { }
    public virtual void OnKeyDownEscape() { }
    public virtual void OnKeyDownReturn() {  }
}
