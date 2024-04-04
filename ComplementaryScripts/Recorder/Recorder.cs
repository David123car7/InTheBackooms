using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    public AudioSource recorderAudio;
    public AudioClip recorderClip;

    void Start()
    {
        recorderAudio = GetComponent<AudioSource>();
        recorderAudio.clip = recorderClip;
    }
}
