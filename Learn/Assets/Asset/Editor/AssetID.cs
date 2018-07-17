using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public struct AssetID : IEquatable<AssetID>
{
    public static readonly AssetID Empty = new AssetID(string.Empty, string.Empty);

    [SerializeField]
    private string bundleName;

    [SerializeField]
    private string assetName;

    [SerializeField]
    private string assetGUID;   //资源唯一编号

    public AssetID(string bundleName, string assetName)
    {
        this.bundleName = bundleName;
        this.assetName = assetName;
        this.assetGUID = string.Empty;
    }

    public string BundleName
    {
        get
        {
            return bundleName;
        }
    }

    public string AssetName
    {
        get
        {
            return assetName;
        }
    }

    public string AssetGUID
    {
        get
        {
            return assetGUID;
        }
        set
        {
            this.assetGUID = value;
        }
    }

    public bool IsEmpty
    {
        get
        {
            return string.IsNullOrEmpty(this.BundleName) || string.IsNullOrEmpty(this.AssetName);
        }
    }

    public static AssetID Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return default(AssetID);
        }
        int num = text.IndexOf(':');
        if (num > 0)
        {
            string text2 = text.Substring(0, num);
            string text3 = text.Substring(num + 1);
            return new AssetID(text2, text3);
        }
        throw new FormatException("Can not parse AssetID.");
    }

    public T LoadObject<T>() where T : UnityEngine.Object
    {
        string assetPath = this.GetAssetPath();
        if (!string.IsNullOrEmpty(assetPath))
        {
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
        return (T)((object)null);
    }

    //通过资源AB 及 assetname 获取路径
    public string GetAssetPath()
    {
        if (this.IsEmpty)
        {
            return null;
        }
        string text = null;
        if (!string.IsNullOrEmpty(this.assetGUID))
        {
            text = AssetDatabase.GUIDToAssetPath(this.assetGUID);
        }
        if (string.IsNullOrEmpty(text))
        {
            string[] assetPathFromAssetBundleAndAssetName = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(this.bundleName, Path.GetFileNameWithoutExtension(this.assetName));
            if (Path.HasExtension(this.assetName))
            {
                string extension = Path.GetExtension(this.assetName);
                string[] array = assetPathFromAssetBundleAndAssetName;
                for (int i = 0; i < array.Length; i++)
                {
                    string text2 = array[i];
                    if(Path.GetExtension(text2) == extension)
                    {
                        text = text2;
                        break;
                    }
                }
            }
            else if (assetPathFromAssetBundleAndAssetName.Length > 0)
            {
                text = assetPathFromAssetBundleAndAssetName[0];
            }
        }
        return text;
    }

    public override string ToString()
    {
        return this.BundleName + ":" + this.AssetName;
    }

    public bool Equals(AssetID other)
    {
        return this.BundleName == other.BundleName && this.AssetName == other.AssetName;
    }

    public override int GetHashCode()
    {
        int hashCode = this.bundleName.GetHashCode();
        return 397 * hashCode ^ this.AssetName.GetHashCode();
    }
}
