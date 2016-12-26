using UnityEngine;
using System.Collections;

public class Log
{
    /// <summary>
    /// 打印日志
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="funcName"></param>
    /// <param name="message"></param>
    public static void LogFormat<T>(string funcName, string message)
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log(string.Format("[{0}::{1}]  {2}", typeof(T), funcName, message));
        }
    }

    /// <summary>
    /// 打印错误日志
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="funcName"></param>
    /// <param name="message"></param>
    public static void LogErrorFormat<T>(string funcName, string message)
    {
        if (Debug.isDebugBuild)
        {
            Debug.LogError(string.Format("[{0}::{1}]  {2}", typeof(T), funcName, message));
        }
    }
}