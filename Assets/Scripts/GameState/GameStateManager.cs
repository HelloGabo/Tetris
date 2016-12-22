//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\GameStateManager.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：游戏状态管理
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateManager 
{
    private static GameStateManager m_Instance;
    public static GameStateManager mInstance
    {
        get
        {
            if (m_Instance == null)
                m_Instance = new GameStateManager();

            return m_Instance;
        }
    }

    public GameStateBase[] m_StateList;

    public GameStateBase mCurGameState { get; private set; }
    public GameStateID mNextStateID;

    public GameStateManager()
    {
        
    }

    public bool Init()
    {
        m_StateList = new GameStateBase[(int)GameStateID.Max];

        m_StateList[(int)GameStateID.Base] = new GameStateBase();
        m_StateList[(int)GameStateID.Start] = new GameStateStart();
        m_StateList[(int)GameStateID.Play] = new GameStatePlay();

        mCurGameState = m_StateList[(int)GameStateID.Base];
        mNextStateID = GameStateID.Start;

        return true;
    }

    public void Update()
    {
        if (mNextStateID != mCurGameState.mID)
        {
            mCurGameState.End();

            mCurGameState = m_StateList[(int)mNextStateID];

            mCurGameState.Start();
            
            return;
        }

        mCurGameState.Update();
    }
}
