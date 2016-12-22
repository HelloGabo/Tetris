//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\Tetris\TetrisManager.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：俄罗斯方块管理
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum TSMStatus
{ 
    None = 0,
    Play,       // 游戏状态
    Puase,      // 暂停状态
    Calc,       // 计分
    GameEnd,    // 游戏结束
}

public class TetrisManager
{
    private static TetrisManager m_Instance;
    public static TetrisManager mInstance
    {
        get
        {
            if ( m_Instance == null )
                m_Instance = new TetrisManager();

            return m_Instance;
        }
    }

    /// <summary>
    /// 游戏分数
    /// </summary>
    public uint mScore { get; private set; }

    /// <summary>
    /// 游戏状态
    /// </summary>
    private TSMStatus m_nStatus;

    /// <summary>
    /// 当前正在操作的方块
    /// </summary>
    private Tetris m_CurTetris;

    public TetrisManager()
    {

    }

    public bool Init()
    {
        m_nStatus = TSMStatus.None;

        return true;
    }

    public bool Start()
    {
        m_nStatus = TSMStatus.Play;

        return true;
    }

    public void Update()
    {
        switch (m_nStatus)
        { 
            case TSMStatus.Play:
                break;

            case TSMStatus.Puase:
                break;
        }
    }

    /// <summary>
    /// 向左
    /// </summary>
    public void MoveLeft()
    { 
    
    }

    /// <summary>
    /// 向右
    /// </summary>
    public void MoveRight()
    { 
    
    }

    /// <summary>
    /// 向下
    /// </summary>
    public void MoveDown()
    { 
    
    }

    /// <summary>
    /// 落下
    /// </summary>
    public void Drop()
    {

    }
}