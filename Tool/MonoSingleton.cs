using UnityEngine;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    //运行时 需要这个脚本的唯一实例 调用instance

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    instance = new GameObject("Singleton of" + typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    instance.Init();
                }
            }
            return instance;
        }
    }
    //项目运行时 在Awake时，从场景中找到 唯一实例 记录在instance中
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
    }
    //提供 初始化一种选择
    public virtual void Init() { }
    //当应用程序 退出做 清理工作 单例模式对象=null
    private void OnApplicationQuit()
    {
        instance = null;
    }
}
