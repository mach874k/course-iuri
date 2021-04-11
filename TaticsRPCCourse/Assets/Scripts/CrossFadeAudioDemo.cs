using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossFadeAudioDemo : MonoBehaviour
{
    [SerializeField] AudioSource fadeInSource;
    [SerializeField] AudioSource fadeOutSource;
    void Start () 
    {
        fadeInSource.volume = 0;
        fadeOutSource.volume = 1;
        fadeInSource.Play();
        fadeOutSource.Play();
        fadeInSource.VolumeTo(1, 5.5f);
        fadeOutSource.VolumeTo(0, 5.5f);
    }
}
