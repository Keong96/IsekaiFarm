using System.Linq;
using UnityEngine;
// Highly accessed class, keep this performant!
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //Debug.Log("Instance set");
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject newgo = new GameObject();
                    instance = newgo.AddComponent<T>();
                    //newgo.name = nameof(T)
                }
            }
            else
            {
                //Debug.LogError($"Detected two singletons of type: {typeof(T)}. You should only have one of each in this scene!");
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
}