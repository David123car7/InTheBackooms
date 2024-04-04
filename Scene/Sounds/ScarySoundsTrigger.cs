using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarySoundsTrigger : MonoBehaviour
{
    [SerializeField] private AudioSource scarySound;
    [SerializeField] private AudioClip scarySoundClip;

    private bool isPlayed = false;

    private void OnTriggerEnter(Collider hit)
    {        
        if (hit.CompareTag("Player") && isPlayed == false)
        {
            scarySound.clip = scarySoundClip;
            scarySound.Play();
            isPlayed = true;
        }
    }
}
