using UnityEngine;

/// <summary>
/// 方块颜色
/// </summary>
public enum EBlockColor
{
    Blue = 0,
    Red,
    Max
}

public class Block
{
    private Transform mTransform;
    public EBlockColor mColor { get; private set; }
    
    public int mX { get; set; }
    public int mY { get; set; }

    /// <summary>
    /// 本地坐标
    /// </summary>
    public Vector3 mLocalPosition
    {
        get { return mTransform.localPosition; }
        set { mTransform.localPosition = value; }
    }

    public float mPositionX
    {
        get { return mTransform.position.x; }
        set
        {
            Vector3 pos = mTransform.position;
            pos.x = value;
            mTransform.position = pos;
        }
    }

    public float mPositionY
    {
        get { return mTransform.position.y; }
        set
        {
            Vector3 pos = mTransform.position;
            pos.y = value;
            mTransform.position = pos;
        }
    }
    
    private Block(Transform transform)
    {
        mTransform = transform;
    }

    /// <summary>
    /// 生成一个指定颜色的方块
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Block Generate(EBlockColor color)
    {
        GameObject blockPrefab = Resources.Load<GameObject>(string.Format("Cube/{0}", color.ToString()));
        
        var blockGO = GameObject.Instantiate(blockPrefab) as GameObject;
        Block block = new Block(blockGO.transform);
        block.mColor = color;
        return block;
    }

    public void SetParent(Transform parent, bool worldPositionStays)
    {
        mTransform.SetParent(parent, worldPositionStays);
    }


    public void SetParent(Transform parent, Vector3 localPos)
    {
        SetParent(parent, false);
        mTransform.localPosition = localPos;
    }

    public void SetCordinate(int x, int y)
    {
        mX = x;
        mY = y;
    }

    public void Destroy()
    {
        GameObject.Destroy(mTransform.gameObject);
    }
}
