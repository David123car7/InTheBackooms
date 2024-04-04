using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSmiler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource smiler;
    [SerializeField] private AudioClip smilerSound;
    [SerializeField] private GameObject smilerJumpscareScript;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        smilerJumpscareScript = GameObject.Find("SmilerJumpscare");
        smiler = GetComponent<AudioSource>();
        SmilerSound();
    }

    private void Update()
    {
        SmilerMovement();
    }

    private void SmilerMovement()
    {
        transform.LookAt(player.transform);
        transform.position += transform.forward * 5f * Time.deltaTime;
    }

    private void SmilerSound()
    {
        smiler.clip = smilerSound;
        smiler.Play();
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            smilerJumpscareScript.GetComponent<SmilerJumpscare>().JumpsCare();            
        }
    }
}
