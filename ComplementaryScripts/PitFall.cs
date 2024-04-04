using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PitFall : MonoBehaviour
{
    private DeathSystem death;
    private GameObject fpc;

    private bool isVideoPlayed = false;
    [SerializeField] private GameObject jumpscareVideo;
    [SerializeField] private RawImage jumpscareVideoImage;
    [SerializeField] private GameObject overlay;

    private void Start()
    {
        fpc = GameObject.FindGameObjectWithTag("Player");
        death = fpc.GetComponent<DeathSystem>();
        overlay = GameObject.Find("Overlay");
        jumpscareVideo = GameObject.FindGameObjectWithTag("JumpscareVideo");
        jumpscareVideoImage = jumpscareVideo.GetComponent<RawImage>();
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            var videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
            if (isVideoPlayed == false)
            {
                overlay.SetActive(false);
                videoPlayer.Play();
                jumpscareVideoImage.enabled = true;
                isVideoPlayed = true;
            }
            Invoke("Death", 5f);           
        }
    }

    private void Death()
    {
        death.Death();
    }
}
