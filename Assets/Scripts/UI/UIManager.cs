//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\UI\UIManager.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：UI管理
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager 
{
    private static UIManager m_Instance;
    public static UIManager mInstance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new UIManager();

            return m_Instance;
        }
    }

    /// <summary>
    /// UI默认路径
    /// </summary>
    private string m_szUIPath = "UI/";
    private int m_nDepthStart = 1;

    private Dictionary<string, UIBase> m_UIList;
    private List<UIBase> m_UIShowList;

    public GameObject mUIRoot { get; private set; }

    public UIManager()
    {
        m_UIList = new Dictionary<string, UIBase>();

        m_UIShowList = new List<UIBase>();
    }

    public bool Init()
    {
        mUIRoot = GameObject.Find("UI Root");
        if (mUIRoot == null)
            return false;

        GameObject.DontDestroyOnLoad(mUIRoot);

        return true;
    }

    /// <summary>
    /// 显示一个UI
    /// </summary>
    /// <param name="szUIName"></param>
    public T Show<T>(string szUIName) where T : UIBase, new()
    {
        UIBase ui = null;
        if (m_UIList.TryGetValue(szUIName, out ui))
        {
            ui.OnShow();
            return null;
        }

        T newUI = new T();
        if (!newUI.Init())
        {
            return null;
        }

        Object uiObj = Resources.Load(string.Format("{0}{1}", m_szUIPath, szUIName));
        GameObject rootUI = GameObject.Instantiate(uiObj) as GameObject;

        rootUI.transform.parent = mUIRoot.transform;
        rootUI.transform.localPosition = Vector3.zero;
        rootUI.transform.localRotation = Quaternion.identity;
        rootUI.transform.localScale = Vector3.one;

        if (!newUI.OnLoad(rootUI))
        {
            return null;
        }

        newUI.OnShow();

        m_UIList.Add(szUIName, newUI);

        // 计算UI深度
        int nDepth = m_nDepthStart;
        if (m_UIShowList.Count > 0)
        {
            nDepth = m_UIShowList[m_UIShowList.Count - 1].mDepth + 1;
        }

        newUI.mDepth = nDepth;

        m_UIShowList.Add(newUI);

        return newUI;
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    /// <param name="szUIName"></param>
    public void Hide(string szUIName)
    {
        UIBase ui = null;
        if (!m_UIList.TryGetValue(szUIName, out ui))
        {
            return;
        }

        ui.OnHide();

        m_UIShowList.Remove(ui);

        m_UIList.Remove(szUIName);
        ui.Release();
        GameObject.Destroy(ui.mRoot);
    }


}
