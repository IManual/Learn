using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AssetLoaderManager : MonoSingleton<AssetLoaderManager>
{
    IABScenceManager iABScenceManager;

    public void Init(string sceneName, params string[] bundleNames)
    {
        iABScenceManager = new IABScenceManager(sceneName);
        for (int i = 0; i < bundleNames.Length; i++)
        {
            iABScenceManager.Init(bundleNames[i],
                (_bundleName, _process) => { },
                (_sceneName, _bundleName) =>
                {
                    Debug.Log(_sceneName + "--" + _bundleName + "加载完成");
                });
        }
    }

    public IEnumerator LoadAssetSys(params string[] bundleNames)
    {
        if (IABManifestLoader.Instance.assetManifest == null)
        {
            yield return IABManifestLoader.Instance.LoadManifet();
        }
        for (int i = 0; i < bundleNames.Length; i++)
        {
            yield return iABScenceManager.LoadAssetSys(bundleNames[i]);
        }
    }

    public Object GetSingleResources(string resName)
    {
        return iABScenceManager.GetSingleResources(resName);
    }
    public Object[] GetMutiResources(string resName)
    {
        return iABScenceManager.GetMutiResources(resName);
    }

    /// <summary>
    /// 释放某个包的单个资源
    /// </summary>
    public void UnloadAsset(string bundleName, string resName)
    {
        iABScenceManager.UnloadAsset(bundleName, resName);
    }
    /// <summary>
    /// 释放单个资源
    /// </summary>
    public void UnloadAsset(string resName)
    {
        iABScenceManager.UnloadAsset(resName);
    }


    /// <summary>
    /// 释放某个包的所有资源,但不卸载这个包
    /// </summary>
    /// <param name="bundleName"></param>
    public void UnloadAssets(string bundleName)
    {
        iABScenceManager.UnloadAsset(bundleName);
    }

    /// <summary>
    /// 释放已加载包的所有资源，但不卸载资源
    /// </summary>
    public void UnloadAllAsset()
    {
        iABScenceManager.UnloadAllAsset();
    }

    /// <summary>
    /// 卸载某个包
    /// </summary>
    /// <param name="bundleName"></param>
    public void UnloadBundle(string bundleName)
    {
        iABScenceManager.UnloadBundle(bundleName);
    }

    /// <summary>
    /// 卸载所有包
    /// </summary>
    public void UnloadAllBundles()
    {
        iABScenceManager.UnloadAllBundles();
    }

    /// <summary>
    /// 释放所有资源，卸载所有包
    /// </summary>
    public void DisposeAllBundleAndResources()
    {
        iABScenceManager.DisposeAllBundleAndResources();
    }

    /// <summary>
    /// 测试
    /// </summary>
    public void DebugAllAsset()
    {
        iABScenceManager.DebugAllAsset();
    }

    protected override void Reset()
    {
        gameObject.name = this.GetType().Name;

    }
}