using UnityEngine;
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    //����ʱ ��Ҫ����ű���Ψһʵ�� ����instance

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
    //��Ŀ����ʱ ��Awakeʱ���ӳ������ҵ� Ψһʵ�� ��¼��instance��
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
    }
    //�ṩ ��ʼ��һ��ѡ��
    public virtual void Init() { }
    //��Ӧ�ó��� �˳��� ������ ����ģʽ����=null
    private void OnApplicationQuit()
    {
        instance = null;
    }
}
