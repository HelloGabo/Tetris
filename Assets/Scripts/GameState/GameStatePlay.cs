//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\GameState\GameStatePlay.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：游戏中状态
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStatePlay : GameStateBase
{
    public override GameStateID mID { get { return GameStateID.Play; } }

    public GameStatePlay()
    {

    }

    public override bool Start()
    {
        Application.LoadLevel("Play");

        return base.Start();
    }

    public override void End()
    {

    }

    public void Pause(bool bPause)
    { 
        
    }
}