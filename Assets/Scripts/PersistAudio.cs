using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistAudio : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
