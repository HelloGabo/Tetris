using UnityEngine;
using System.Collections;

public class TimeData
{
    /// <summary>
    /// 离下次下降多少时间
    /// </summary>
    public float mTime { get; set; }

    /// <summary>
    /// 下降时间间隔
    /// </summary>
    public float mInterval { get; set; }

    /// <summary>
    /// 是否drop（加速）
    /// </summary>
    public bool mIsSpeedUp { get; set; }

    public float mSpeedUpInterval { get; set; }

    public TimeData(float time, float interval)
    {
        mTime = time;
        mInterval = interval;
        mSpeedUpInterval = 0.01f;
    }
}
