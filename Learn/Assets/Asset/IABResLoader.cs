using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 单个包的“资源”加载
/// </summary>
public class IABResLoader : IDisposable
{
    private AssetBundle ABRes;

    /// <summary>
    /// 初始化获取Assetbundle包
    /// </summary>
    /// <param name="tmpBundle">assetbundle对象</param>
    public IABResLoader(AssetBundle tmpBundle)
    {
        ABRes = tmpBundle;
    }

    /// <summary>
    /// 索引器（加载单个资源）
    /// </summary>
    /// <param name="resName">资源名称</param>
    /// <returns></returns>
    public UnityEngine.Object this[string resName]
    {
        get
        {
            if (this.ABRes == null || !this.ABRes.Contains(resName))
            {
                Debug.LogError("res not contain == " + resName);
            }
            return ABRes.LoadAsset(resName);
        }
    }

    /// <summary>
    /// 加载嵌套资源（自身及所有独立出去子预制体）
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="resName">资源名称</param>
    /// <returns></returns>
    public UnityEngine.Object[] LoadResources(string resName)
    {
        if(this.ABRes == null || !this.ABRes.Contains(resName))
        {
            Debug.LogError("res not contain == " + resName);
            return null;
        }
        //加载所有相关资源
        return ABRes.LoadAssetWithSubAssets(resName);
    }

    /// <summary>
    /// 释放已加载的Asset对象
    /// </summary>
    /// <param name="resObj"></param>
    public void UnloadAsset(UnityEngine.Object resObj)
    {
        Resources.UnloadAsset(resObj);
    }
    /// <summary>
    /// 释放AssetBundle文件内存镜像
    /// True 销毁所有已经Load的Assets内存镜像
    /// False 不销毁Load创建的Assets对象
    /// </summary>
    /// <param name="unloadAllLoadedObjects"></param>
    public void Unload(bool unloadAllLoadedObjects)
    {
        ABRes.Unload(unloadAllLoadedObjects);
    }

    /// <summary>
    /// 自动释放AssetBundle文件内存镜像，不销毁Load创建的Assets对象
    /// </summary>
    public void Dispose()
    {
        Unload(false);
    }

    /// <summary>
    /// 调试
    /// </summary>
    public void DebugAllRes()
    {
        string[] tmpAssetName = ABRes.GetAllAssetNames();
        for (int i = 0; i < tmpAssetName.Length; i++)
        {
            Debug.Log("ABRes Contain asset name == " + tmpAssetName[i]);
        }
    }



    //GameObject.Destroy(gameObject)，销毁该物体；
    //AssetBundle.Unload(false)，释放AssetBundle文件内存镜像，不销毁Load创建的Assets对象；
    //AssetBundle.Unload(true)，释放AssetBundle文件内存镜像同时销毁所有已经Load的Assets内存镜像；
    //Resources.UnloadAsset(Object)，释放已加载的Asset对象；
    //Resources.UnloadUnusedAssets，释放所有没有引用的Asset对象。
}
