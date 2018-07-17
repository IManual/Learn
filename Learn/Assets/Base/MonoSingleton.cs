using UnityEngine;
using System.Collections;

/// <summary>
/// 脚本的单例类
/// </summary>
public class MonoSingleton<T> : BaseBehaviour where T:MonoSingleton<T>
{ 
    private static T instance;
    public static T Instance
    {
        //懒汉
        //按需分配，如果在Awake方法中需要调用，就在Awake方法中创建
        get
        {
            if (instance == null)
            {
                //在场景中查找已经存在的对象引用
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                { 
                    //创建脚本对象
                    string goName =typeof(T).Name;
                    instance = new GameObject(goName).AddComponent<T>() as T;
                }
                //子类可能需要在此执行一段代码……
                instance.InitManager();
            }
            return instance;
        }
    }

    //饿汉
    protected override void Awake()
    {
        //当场景切换时 不销毁当前游戏对象
        DontDestroyOnLoad(gameObject);

        //Instance = this as T;
    }
    
    protected virtual void InitManager() { }
}
