using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class IABScenceManager
{
    IABManager abManager;

    public IABScenceManager(string scenceName)
    {
        ReadConfiger(scenceName);
    }
    public IABScenceManager()
    {

    }
    //存储资源 与资源路径 对应关系
    Dictionary<string, string> allAsset = new Dictionary<string, string>();

    /// <summary>
    /// 读取场景配置文件
    /// </summary>
    /// <param name="scenceName"></param>
    public void ReadConfiger(string scenceName)
    {
        string textFileName = "Record.txt";
        string path = IPathTool.GetAssetBundlePath() + "/" + scenceName + textFileName;

        abManager = new IABManager(scenceName);
        ReadConfig(path);
    }

    private void ReadConfig(string path)
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        StreamReader br = new StreamReader(fs);
        string line = br.ReadLine();
        int allCount = int.Parse(line);
        for (int i = 0; i < allCount; i++)
        {
            string tmpStr = br.ReadLine();
            string[] tmpArr = tmpStr.Split(" ".ToCharArray());
            allAsset.Add(tmpArr[0], tmpArr[1]);
        }
        br.Close();
        fs.Close();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="bundleName">ab标签</param>
    /// <param name="progress">加载时回调</param>
    /// <param name="callback">回调</param>
    public void Init(string bundleName, LoadProgrecess progress, LoadAssetBundleCallBack callback)
    {
        if (allAsset.ContainsValue(bundleName))
        {
            abManager.Init(bundleName, progress, callback);
        }
        else
        {
            Debug.Log("Dont contain the bundle == " + bundleName);
        }
    }

    #region 由下层提供的功能

    /// <summary>
    /// 加载资源 需先加载完manifest
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public IEnumerator LoadAssetSys(string bundleName)
    {
        yield return abManager.LoadAssetBundles(bundleName);
    }

    public Object GetSingleResources(string bundleName, string resName)
    {
        if (allAsset.ContainsValue(bundleName))
        {
            return abManager.GetSingleResources(bundleName, resName);
        }
        else
        {
            Debug.Log("Dont contain the bundle == " + bundleName);
            return null;
        }
    }

    public Object GetSingleResources(string resName)
    {
        if(!allAsset.ContainsKey(resName))
        {
            Debug.Log("Dont contain the asset == " + resName);
            return null;
        }
        return GetSingleResources(allAsset[resName], resName);
    }



    public Object[] GetMutiResources(string bundleName, string resName)
    {
        if (allAsset.ContainsValue(bundleName))
        {
            return abManager.GetMutiResources(bundleName, resName);
        }
        else
        {
            Debug.Log("Dont contain the bundle == " + bundleName);
            return null;
        }
    }
    public Object[] GetMutiResources(string resName)
    {
        if (!allAsset.ContainsKey(resName))
        {
            Debug.Log("Dont contain the asset == " + resName);
            return null;
        }
        return GetMutiResources(allAsset[resName], resName);
    }

    /// <summary>
    /// 释放某个包的单个资源
    /// </summary>
    public void UnloadAsset(string bundleName, string resName)
    {
        if (allAsset.ContainsValue(bundleName))
        {
            abManager.UnloadAsset(bundleName, resName);
        }
        else
        {
            Debug.Log("Dont contain the bundle == " + bundleName);
        }
    }
    /// <summary>
    /// 释放单个资源
    /// </summary>
    public void UnloadAsset(string resName)
    {
        if (!allAsset.ContainsKey(resName))
        {
            Debug.Log("Dont contain the asset == " + resName);
            return;
        }
        UnloadAsset(allAsset[resName],resName);
    }


    /// <summary>
    /// 释放某个包的所有资源,但不卸载这个包
    /// </summary>
    /// <param name="bundleName"></param>
    public void UnloadAssets(string bundleName)
    {
        if (allAsset.ContainsValue(bundleName))
        {
            abManager.UnloadAsset(bundleName);
        }
        else
        {
            Debug.Log("Dont contain the bundle == " + bundleName);
        }
    }

    /// <summary>
    /// 释放已加载包的所有资源，但不卸载资源
    /// </summary>
    public void UnloadAllAsset()
    {
        abManager.UnloadAllAsset();
    }

    /// <summary>
    /// 卸载某个包
    /// </summary>
    /// <param name="bundleName"></param>
    public void UnloadBundle(string bundleName)
    {
        if (allAsset.ContainsValue(bundleName))
        {
            abManager.UnloadBundle(bundleName);
        }
    }

    /// <summary>
    /// 卸载所有包
    /// </summary>
    public void UnloadAllBundles()
    {
        abManager.UnloadAllBundles();

        allAsset.Clear();
    }

    /// <summary>
    /// 释放所有资源，卸载所有包
    /// </summary>
    public void DisposeAllBundleAndResources()
    {
        abManager.DisposeAllBundleAndResources();

        allAsset.Clear();
    }

    /// <summary>
    /// 测试
    /// </summary>
    public void DebugAllAsset()
    {

        List<string> values = new List<string>();

        values.AddRange(allAsset.Values);
        for (int i = 0; i < values.Count; i++)
        {
            abManager.DebugAssetBundle(values[i]);
        }
    }
    #endregion
}
