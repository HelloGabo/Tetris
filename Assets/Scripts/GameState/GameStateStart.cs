//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\GameState\GameStateStart.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：开始状态
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateStart : GameStateBase
{
    public override GameStateID mID { get { return GameStateID.Start; } }

    public GameStateStart()
    {

    }

    public override bool Start()
    {
        UIManager.mInstance.Show<UIStart>("UIStart");
        Application.LoadLevel("Start");

        return base.Start();
    }

    public override void End()
    {
        UIManager.mInstance.Hide("UIStart");
    }
}