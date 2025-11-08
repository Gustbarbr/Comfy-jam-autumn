using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepBetweenScenes : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
