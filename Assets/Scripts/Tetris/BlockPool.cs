using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockPool
{
    private GameObject m_PoolGO;
    private Dictionary<EBlockColor, List<Block>> m_Pool;

    private static BlockPool m_Instance;
    public static BlockPool mInstance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new BlockPool();
                m_Instance.m_PoolGO = new GameObject("BlockPool");
                m_Instance.m_PoolGO.transform.position = new Vector3(0, 0, 10000);
                GameObject.DontDestroyOnLoad(m_Instance.m_PoolGO);
            }

            return m_Instance;
        }
    }

    private BlockPool()
    {
        m_Pool = new Dictionary<EBlockColor, List<Block>>();
    }

    /// <summary>
    /// 将对象添加到对象池中
    /// </summary>
    /// <param name="key"></param>
    /// <param name="block"></param>
    public void Push(Block block)
    {
        List<Block> poolList;
        if(!m_Pool.TryGetValue(block.mColor, out poolList))
        {
            poolList = new List<Block>();
            m_Pool[block.mColor] = poolList;
        }

        block.SetParent(m_PoolGO.transform, Vector3.zero);
        poolList.Add(block);
    }

    /// <summary>
    /// 从对象池中拿出一个块
    /// </summary>
    /// <param name="blockColor"></param>
    /// <returns></returns>
    public Block Pop(EBlockColor blockColor)
    {
        Block block = null;

        List<Block> poolList;
        if(!m_Pool.TryGetValue(blockColor, out poolList) || poolList.Count == 0)
        {
            block = Block.Generate(blockColor);
        }
        else
        {
            block = poolList[0];
            poolList.RemoveAt(0);
        }

        return block;
    }
}
