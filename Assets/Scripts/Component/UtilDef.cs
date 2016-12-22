//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\Component\UtilDef.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：常规定义
// 说明：
//
//////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

public interface IKeyDownEvent
{
    void OnKeyDownW();
    void OnKeyDownS();
    void OnKeyDownA();
    void OnKeyDownD();
    void OnKeyDownSpace();
    void OnKeyDownEscape();
    void OnKeyDownReturn();
}

/// <summary>
/// 一些全局定义
/// </summary>
public class G
{
    /// <summary>
    /// 每一个方块的宽高都是1米
    /// </summary>
    public static int BLOCK_SIZE = 1;

    /// <summary>
    /// 场景水平方向可容纳方块的数量
    /// </summary>
    public static int SCENE_WIDTH = 6;

    /// <summary>
    /// 场景垂直方向可容纳方块的数量
    /// </summary>
    public static int SCENE_HEIGHT = 12;
}
