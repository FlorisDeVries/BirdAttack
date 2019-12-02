using UnityEngine;

public class AwakeSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning($"Auto created an instance of {typeof(T).ToString()}, which might not work...");
                _instance = new AwakeSingleton<T>() as T;
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        _instance = this as T;
    }
}