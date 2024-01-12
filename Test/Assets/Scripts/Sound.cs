using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string Name;
    public AudioClip AudioClip;
    [Range(0f, 1f)]
    public float Volume =0.2f;
    [HideInInspector]
    public AudioSource Source;
    //some other clip data 
}
