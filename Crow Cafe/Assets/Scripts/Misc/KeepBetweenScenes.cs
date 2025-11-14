using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenScenes : MonoBehaviour
{
    private static HashSet<string> existingObjects = new HashSet<string>();

    void Awake()
    {
        string id = gameObject.name + "_" + gameObject.GetType().ToString();

        if (existingObjects.Contains(id))
        {
            Destroy(gameObject);
            return;
        }

        existingObjects.Add(id);
        DontDestroyOnLoad(gameObject);
    }
}
