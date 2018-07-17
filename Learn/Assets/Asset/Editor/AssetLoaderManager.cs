using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class AssetLoaderManager
{
    /// <summary>
    /// 根据AB标签及资源名称加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public static T LoadAsset<T>(string bundleName, string assetName) where T : UnityEngine.Object
    {
        if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(assetName))
        {
            Debug.Log("BundleName or AssetName not sign yet!");
            return default(T);
        }
        string path = GetAssetPath(bundleName, assetName);
        if (path != null)
        {
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }
        return default(T);
    }

    /// <summary>
    /// 根据AB标签及资源名称加载资源
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public static UnityEngine.Object LoadAsset(string bundleName, string assetName, Type type)
    {
        if (string.IsNullOrEmpty(bundleName) || string.IsNullOrEmpty(assetName) || type == null)
        {
            Debug.Log("Bundle or Asset Name or type not sign yet!");
            return null;
        }
        string path = GetAssetPath(bundleName, assetName);
        if (path != null)
        {
            return AssetDatabase.LoadAssetAtPath(path, type);
        }
        return null;
    }

    /// <summary>
    /// 通过资源AB标签及资源名称 获取资源路径
    /// </summary>
    /// <returns></returns>
    private static string GetAssetPath(string bundleName, string assetName)
    {
        string path = null;
        //获取资源路径  所有同名资源
        string[] assetPathFromAssetBundleAndAssetName = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(bundleName, Path.GetFileNameWithoutExtension(assetName));
        //如果资源名包含后缀  根据后缀找到对应资源的路径
        if (Path.HasExtension(assetName))
        {
            string extension = Path.GetExtension(assetName);
            for (int i = 0; i < assetPathFromAssetBundleAndAssetName.Length; i++)
            {
                string text = assetPathFromAssetBundleAndAssetName[i];
                if (Path.GetExtension(text) == extension)
                {
                    path = text;
                    break;
                }
            }
        }
        else if (assetPathFromAssetBundleAndAssetName.Length > 0)
        {
            path = assetPathFromAssetBundleAndAssetName[0];
        }
        return path;
    }
}
