using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;

public class XmlSerializeUtil
{
    /// <summary>
    /// 反序列化XML文件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="text"></param>
    /// <returns></returns>
    public static T DeserializeXml<T>(string text)
    {
        T value = default(T);

        try
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(text))
            {
                value = (T)deserializer.Deserialize(reader);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return value;
    }
}
