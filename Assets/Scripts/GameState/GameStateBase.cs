//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\GameState\GameStateBase.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：游戏状态基类
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameStateID
{ 
    Base = 0,
    Start,
    Play,

    Max,
}


public class GameStateBase 
{
    public virtual GameStateID mID { get { return GameStateID.Base; } }

    public GameStateBase()
    {
        
    }

    public virtual bool Init()
    {
        return true;
    }

    public virtual void Update()
    {
        
    }

    public virtual bool Start()
    { 
        return true;
    }

    public virtual void End()
    { 
        
    }
}
