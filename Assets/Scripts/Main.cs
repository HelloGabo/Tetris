//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\Main.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：入口脚本
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour 
{
    private static Main m_Instance;
    public static Main mInstance
    {
        get
        {
            return m_Instance;
        }
    }

    private List<IKeyDownEvent> m_KDEventList;

    void Awake()
    {
        m_Instance = this;
    }

    void Start()
    {
        m_KDEventList = new List<IKeyDownEvent>();
        
        GameObject.DontDestroyOnLoad(GameObject.Find("Main"));

        UIManager.mInstance.Init();
        GameStateManager.mInstance.Init();
    }


    void Update()
    {
        GameStateManager.mInstance.Update();

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (m_KDEventList.Count > 0 )
                m_KDEventList[m_KDEventList.Count-1].OnKeyDownW();
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (m_KDEventList.Count > 0)
                m_KDEventList[m_KDEventList.Count - 1].OnKeyDownA();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (m_KDEventList.Count > 0)
                m_KDEventList[m_KDEventList.Count - 1].OnKeyDownS();
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (m_KDEventList.Count > 0)
                m_KDEventList[m_KDEventList.Count - 1].OnKeyDownD();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (m_KDEventList.Count > 0)
                m_KDEventList[m_KDEventList.Count - 1].OnKeyDownSpace();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (m_KDEventList.Count > 0)
                m_KDEventList[m_KDEventList.Count - 1].OnKeyDownEscape();
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (m_KDEventList.Count > 0)
                m_KDEventList[m_KDEventList.Count - 1].OnKeyDownReturn();
        }
    }

    public void Push(IKeyDownEvent KDEvent)
    {
        m_KDEventList.Add(KDEvent);
    }

    public void Pop()
    {
        m_KDEventList.RemoveAt(m_KDEventList.Count - 1);
    }
}
