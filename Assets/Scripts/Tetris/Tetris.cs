//////////////////////////////////////////////////////////////////////////
// 乐探游戏版权所有
// 
// 文件：Tetris\Assets\Scripts\Tetris\Tetris.cs
// 作者：Xiexx
// 时间：2016/11/17
// 描述：俄罗斯方块基类
// 说明：
//
//////////////////////////////////////////////////////////////////////////
using UnityEngine;
using System.Collections.Generic;

public class Tetris
{
    private GameObject m_TetrisGO;

    private Vector3 m_Coordinate = Vector3.zero;

    /// <summary>
    /// 方块x坐标
    /// </summary>
    public int mX
    {
        get { return (int)m_Coordinate.x; }
        set
        {
            m_Coordinate.x = value;
            m_TetrisGO.transform.position = m_Coordinate;
        }
    }

    /// <summary>
    /// 方块y坐标
    /// </summary>
    public int mY
    {
        get { return (int)m_Coordinate.y; }
        set
        {
            m_Coordinate.y = value;
            m_TetrisGO.transform.position = m_Coordinate;
        }
    }

    /// <summary>
    /// 方块行数
    /// </summary>
    public int mRow { get; private set; }

    /// <summary>
    /// 方块列数
    /// </summary>
    public int mCol{get; private set;}

    /// <summary>
    /// 方块数据，mTmpFlag：变形时用的，避免每次变形都new一个数组产生gc
    /// </summary>
    private bool[,] m_Flag, m_TmpFlag;
    public bool[,] mFlag { get { return m_Flag; } }

    /// <summary>
    /// 构成方块的立方体
    /// </summary>
    private List<Block> m_Blocks;
    public List<Block> mBlocks { get { return m_Blocks; } private set { m_Blocks = value; } }

    /// <summary>
    /// 从配置文件的数据初始化
    /// </summary>
    /// <param name="tetrisItem"></param>
    public Tetris(TetrisItem tetrisItem)
    {
        mRow = tetrisItem.mRow;
        mCol = tetrisItem.mCol;

        string[] flagData = tetrisItem.mData.Split(',');
        
        int max = Mathf.Max(mRow, mCol);
        m_Flag = new bool[max, max];
        m_TmpFlag = new bool[max, max];
        for (int x = 0; x < mCol; ++x)
        {
            for (int y = 0; y < mRow; ++y)
            {
                m_Flag[x, y] = flagData[y * mCol + x] == "1";
            }
        }
    }

    /// <summary>
    /// 构造方块
    /// </summary>
    /// <returns></returns>
    public virtual bool Init()
    {
        mBlocks = new List<Block>();
        m_TetrisGO = new GameObject();

        EBlockColor color = (EBlockColor)Random.Range(0, (int)EBlockColor.Max);
        
        for(int x=0; x< mCol; ++x)
        {
            for(int y=0; y<mRow; ++y)
            {
                if (m_Flag[x, y] == false)
                    continue;

                Block block = BlockPool.mInstance.Pop(color);
                block.SetCordinate(x, y);
                Vector3 localPos = new Vector3(x * G.BLOCK_SIZE, y * G.BLOCK_SIZE, -0.1f);
                block.SetParent(m_TetrisGO.transform, localPos);
                mBlocks.Add(block);
            }
        }

        return true;
    }

    public void Destroy()
    {
        GameObject.Destroy(m_TetrisGO);
    }

    /// <summary>
    /// 设置方块坐标
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetCoordinate(int x, int y)
    {
        m_Coordinate.x = x;
        m_Coordinate.y = y;
        m_TetrisGO.transform.position = m_Coordinate;
    }

    /// <summary>
    /// 改变形状（顺时针旋转90度）
    /// </summary>
    public void Translate()
    {
        for(int x=0; x<mCol; ++x)
        {
            for(int y=0; y<mRow; ++y)
            {
                m_TmpFlag[y, mCol-x-1] = m_Flag[x, y];
            }
        }
        int tmp = mRow, blockIdx = 0;
        mRow = mCol;
        mCol = tmp;
        for(int x=0; x< mCol; ++x)
        {
            for(int y=0; y<mRow; ++y)
            {
                m_Flag[x, y] = m_TmpFlag[x, y];
                if(m_Flag[x, y])
                {
                    Block block = mBlocks[blockIdx++];
                    block.mLocalPosition = new Vector3(x * G.BLOCK_SIZE, y * G.BLOCK_SIZE, 0);
                    block.SetCordinate(x, y);
                }
            }
        }
    }
}