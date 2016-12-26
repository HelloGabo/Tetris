using System.Xml.Serialization;
using System.Collections.Generic;

public class TetrisConfig
{
    [XmlElement("TerisItem")]
    public List<TetrisItem> mTetrisItemList;
}

public class TetrisItem
{
    [XmlAttribute("id")] public int mID;
    [XmlAttribute("row")] public int mRow;
    [XmlAttribute("col")] public int mCol;
    [XmlText] public string mData;
}
