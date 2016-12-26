using System.Xml.Serialization;
using System.Collections.Generic;

public class ScoreConfig
{
    [XmlElement("ScoreItem")]
    public List<ScoreItem> mScoreItemList;

    public uint GetScore(int lineCount)
    {
        uint score = 0;
        if (mScoreItemList == null)
            return score;

        for(int i=0; i<mScoreItemList.Count; ++i)
        {
            if(mScoreItemList[i].mLineCount == lineCount)
            {
                score = mScoreItemList[i].mScore;
                break;
            }
        }
        return score;
    }
}


public class ScoreItem
{
    [XmlAttribute("line_count")]
    public uint mLineCount;

    [XmlText]
    public uint mScore;
}