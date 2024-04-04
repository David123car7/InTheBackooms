using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySounds : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource actualSource;

    private bool hadPlayed = false;

    private void Start()
    {
        StartCoroutine(StartScaryAudio(30, 120));
    }

    private void Update()
    {
        if(hadPlayed == true)
        {
            hadPlayed = false;
            StartCoroutine(StartScaryAudio(30, 120));
        }

    }

    private IEnumerator StartScaryAudio(int minTime, int maxTime)
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        actualSource = audioSources[Random.Range(0, audioSources.Length)];
        actualSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        actualSource.Play();

        hadPlayed = true;
    }
}
