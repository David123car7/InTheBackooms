using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmilerJumpscare : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject smiler;
    [SerializeField] private GameObject smilerJumpscareVideo;
    private RawImage smilerJumpscareRawImage;
    
    [SerializeField] private GameObject smilerJumpscareGb;

    private bool isSmilersSpawned = false;
    private bool isVideoPlayed = false;

    private DeathSystem death;
    [SerializeField] private GameObject overlay;


    private void Start()
    {       
        death = player.GetComponent<DeathSystem>();
        smilerJumpscareRawImage = smilerJumpscareVideo.GetComponent<RawImage>();
    }

    private void Update()
    {
     
        if(isSmilersSpawned)
        {
            Effects();
        }

        if(isVideoPlayed)
        {
            StartCoroutine(Death());
        }
    }

    public void StartJumpscare()
    {
            SpawnSmiler();
            Effects();
    }

    public void SpawnSmiler()
    {
        Vector3 spawnSmiler1 = new Vector3(player.gameObject.transform.localPosition.x + 60f , player.gameObject.transform.localPosition.y, player.gameObject.transform.localPosition.z); //Right
        Vector3 spawnSmiler2 = new Vector3(player.gameObject.transform.localPosition.x - 60f, player.gameObject.transform.localPosition.y, player.gameObject.transform.localPosition.z); // Left
        Vector3 spawnSmiler3 = new Vector3(player.gameObject.transform.localPosition.x, player.gameObject.transform.localPosition.y, player.gameObject.transform.localPosition.z + 60f); // Forward
        Vector3 spawnSmiler4 = new Vector3(player.gameObject.transform.localPosition.x, player.gameObject.transform.localPosition.y, player.gameObject.transform.localPosition.z - 60f); // Back

        Instantiate(smiler, spawnSmiler1, Quaternion.identity, smilerJumpscareGb.transform);
        Instantiate(smiler, spawnSmiler2, Quaternion.identity, smilerJumpscareGb.transform);
        Instantiate(smiler, spawnSmiler3, Quaternion.identity, smilerJumpscareGb.transform);
        Instantiate(smiler, spawnSmiler4, Quaternion.identity, smilerJumpscareGb.transform);

        isSmilersSpawned = true;
    }

    public void Effects()
    {
        RenderSettings.fog = true;
        RenderSettings.fogDensity = 0.3f;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
    }

    public void JumpsCare()
    {
        var videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();

        if(isVideoPlayed == false)
        {
            overlay.SetActive(false);
            videoPlayer.Play();
            smilerJumpscareRawImage.enabled = true;
            isVideoPlayed = true;
        }

        Destroy(smilerJumpscareGb);
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(9.17f);
        death.Death();
    }
}
  