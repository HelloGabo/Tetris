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

public enum TSMStatus
{ 
    None = 0,
    Play,       // 游戏状态
    Pause,      // 暂停状态
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
    /// 方块配置
    /// </summary>
    private TetrisConfig mTetrisConfig;

    /// <summary>
    /// 得分配置
    /// </summary>
    private ScoreConfig mScoreConfig;

    /// <summary>
    /// 游戏分数
    /// </summary>
    public uint mScore { get; private set; }

    /// <summary>
    /// 游戏状态
    /// </summary>
    private TSMStatus m_nStatus;
    public TSMStatus mStatus { get { return m_nStatus; } set { m_nStatus = value; } }

    private Block[,] m_SceneData;

    /// <summary>
    /// 当前正在操作的方块
    /// </summary>
    private Tetris m_CurTetris;

    private TimeData m_TimeData;

    private GameObject m_SceneTetrisGO;

    public delegate void ScoreChangeAction(uint score);
    public event ScoreChangeAction OnScoreChangeEvent;

    public TetrisManager()
    {
        m_SceneData = new Block[G.SCENE_WIDTH + 5, G.SCENE_HEIGHT + 5];
        mTetrisConfig = Utility.GetXmlConfig<TetrisConfig>("Config/TetrisConfig");;
        mScoreConfig = Utility.GetXmlConfig<ScoreConfig>("Config/ScoreConfig");
    }



    public bool Init()
    {
        mStatus = TSMStatus.None;

        return true;
    }

    public bool Start()
    {
        mStatus = TSMStatus.Play;

        if (m_SceneTetrisGO == null)
            m_SceneTetrisGO = new GameObject("SceneTetrisGO");

        m_TimeData = new TimeData(0, 1);
        m_CurTetris = GenerateTetris();
        mScore = 0;

        return true;
    }

    public void End()
    {
        GameStateManager.mInstance.mNextStateID = GameStateID.Start;
        mStatus = TSMStatus.None;
        if (m_CurTetris != null)
            m_CurTetris.Destroy();
        
        for(int i=0; i<G.SCENE_WIDTH+5; ++i)
        {
            for(int j=0; j<G.SCENE_HEIGHT+5; ++j)
            {
                if (m_SceneData[i, j] != null)
                {
                    m_SceneData[i, j].Destroy();
                    m_SceneData[i, j] = null;
                }

            }
        }
    }

    public void Update()
    {
        switch (mStatus)
        {
            case TSMStatus.None:
                Start();
                break;

            case TSMStatus.Play:
                m_TimeData.mTime += Time.deltaTime;
                if ((m_TimeData.mIsSpeedUp && m_TimeData.mTime>=m_TimeData.mSpeedUpInterval) ||
                    m_TimeData.mTime >= m_TimeData.mInterval)
                {
                    m_TimeData.mTime = 0;
                    MoveDown();
                }
                break;

            case TSMStatus.Pause:
                break;

            case TSMStatus.Calc:
                Calculate();
                break;
        }
    }

    private Tetris GenerateTetris()
    {
        int index = Random.Range(0, mTetrisConfig.mTetrisItemList.Count);
        var tetris = new Tetris(mTetrisConfig.mTetrisItemList[index]);
        tetris.Init();
        tetris.SetCoordinate(G.SCENE_WIDTH / 2, G.SCENE_HEIGHT);
        return tetris;
    }

    private void Calculate()
    {
        Block block;
        for (int i = 0; i < m_CurTetris.mBlocks.Count; ++i)
        {
            block = m_CurTetris.mBlocks[i];
            m_SceneData[m_CurTetris.mX + block.mX, m_CurTetris.mY + block.mY] = block;
            block.SetParent(m_SceneTetrisGO.transform, true);
        }
        LogSceneData();
        m_CurTetris.Destroy();
        
        for(int x=0; x<G.SCENE_WIDTH; ++x)
        {
            if(m_SceneData[x, G.SCENE_HEIGHT] != null)
            {
                End();
                return;
            }
        }


        int completedLineCount = 0;
        for(int y=0; y<G.SCENE_HEIGHT; ++y)
        {
            int newY = y - completedLineCount;
            bool isLineComplete = true;
            for(int x=0; x<G.SCENE_WIDTH; ++x)
            {
                if(m_SceneData[x, y] == null)
                    isLineComplete = false;
                else if(completedLineCount > 0)
                {
                    m_SceneData[x, y].mPositionY -= completedLineCount * G.BLOCK_SIZE;
                    m_SceneData[x, newY] = m_SceneData[x, y];
                    m_SceneData[x, y] = null;
                }
            }
            if(isLineComplete)
            {
                completedLineCount++;
                for(int x=0; x<G.SCENE_WIDTH; ++x)
                {
                    BlockPool.mInstance.Push(m_SceneData[x, newY]);
                    m_SceneData[x, newY] = null;
                }
                LogSceneData();
            }
        }
        if(completedLineCount > 0)
        {
            mScore += mScoreConfig.GetScore(completedLineCount);
            if(OnScoreChangeEvent != null)
                OnScoreChangeEvent(mScore);
        }

        mStatus = TSMStatus.Play;
        Next();
    }

    private void Next()
    {
        m_CurTetris = GenerateTetris();

        m_TimeData.mIsSpeedUp = false;
    }

    
    /// <summary>
    /// 变形
    /// </summary>
    public void Translate()
    {
        m_CurTetris.Translate();
    }

    /// <summary>
    /// 向左
    /// </summary>
    public void MoveLeft()
    {
        if (mStatus != TSMStatus.Play)
            return;

        if (m_CurTetris.mX > 0 && CanMoveLeft())
            m_CurTetris.mX--;

    }

    /// <summary>
    /// 向右
    /// </summary>
    public void MoveRight()
    {
        if (mStatus != TSMStatus.Play)
            return;

        if (m_CurTetris.mX + m_CurTetris.mCol < G.SCENE_WIDTH && CanMoveRight())
            m_CurTetris.mX++;
    }

    /// <summary>
    /// 向下
    /// </summary>
    public void MoveDown()
    {
        if (mStatus != TSMStatus.Play)
            return;

        m_TimeData.mTime = 0;
        bool canMoveDown = CanMoveDown();
        if (m_CurTetris.mY > 0 && canMoveDown)
            m_CurTetris.mY--;
        else if(!canMoveDown)
        {
            mStatus = TSMStatus.Calc;
        }
    }

    /// <summary>
    /// 落下
    /// </summary>
    public void Drop()
    {
        if (mStatus != TSMStatus.Play)
            return;

        m_TimeData.mIsSpeedUp = true;
    }

    private bool CanMoveLeft()
    {
        for (int i = 0; i < m_CurTetris.mBlocks.Count; ++i)
        {
            Block block = m_CurTetris.mBlocks[i];
            int x = m_CurTetris.mX + block.mX, y = m_CurTetris.mY + block.mY;
            if (x == 0 || m_SceneData[x - 1, y] != null)
                return false;

        }
        return true;
    }

    private bool CanMoveRight()
    {
        for(int i=0; i<m_CurTetris.mBlocks.Count; ++i)
        {
            Block block = m_CurTetris.mBlocks[i];
            int x = m_CurTetris.mX + block.mX, y = m_CurTetris.mY + block.mY;
            if (x == G.SCENE_WIDTH-1 || m_SceneData[x+1, y] != null)
                return false;

        }
        return true;
    }



    private bool CanMoveDown()
    {
        for(int i=0; i<m_CurTetris.mBlocks.Count; ++i)
        {
            Block block = m_CurTetris.mBlocks[i];
            int x = m_CurTetris.mX + block.mX, y = m_CurTetris.mY + block.mY;
            if (y == 0 || m_SceneData[x, y - 1] != null)
                return false;
        }
        return true;
    }


    private void LogSceneData()
    {
        Log.LogFormat<TetrisManager>("LogSceneData", "===================SceneData================");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i=G.SCENE_HEIGHT-1; i>=0; --i)
        {
            for (int j=0; j<G.SCENE_WIDTH; ++j)
            {
                string flag = m_SceneData[j, i] == null ? "0" : "*";
                sb.Append(flag);
            }
            Log.LogFormat<TetrisManager>("LogSceneData", sb.ToString());
            sb.Remove(0, sb.Length);
        }
        Log.LogFormat<TetrisManager>("LogSceneData", "=============================================");
    }
}