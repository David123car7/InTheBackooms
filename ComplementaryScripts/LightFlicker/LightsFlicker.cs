using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsFlicker : MonoBehaviour
{
    [SerializeField] private Light lightOB;
    [SerializeField] private GameObject lightON;
    [SerializeField] private GameObject lightOFF;

    private bool isFlickering = false;
    private float timeDelay;

    [SerializeField] private AudioSource lightSound;
    [SerializeField] private AudioClip lightd;

  

    private void Start()
    {
        lightOB = gameObject.GetComponent<Light>();
        lightSound = gameObject.GetComponent<AudioSource>();

        
        lightSound.clip = lightd;
        lightSound.Play();
    }

    private void Update()
    {
        if(isFlickering == false)
        {
            StartCoroutine(FlickeringLight());
        }
    }

    private IEnumerator FlickeringLight()
    {
        isFlickering = true;
        lightOB.enabled = false; //Desliga luz
        lightON.SetActive(false); //Desliga a luz ligada
        lightOFF.SetActive(true); //Ativa luz desligada
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        lightOB.enabled = true; //Liga a luz
        lightON.SetActive(true); //Ativa a luz ligada
        lightOFF.SetActive(false); //Desativa luz desligada
        timeDelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
