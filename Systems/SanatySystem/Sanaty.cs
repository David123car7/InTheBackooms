using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Sanaty : MonoBehaviour
{
   [SerializeField] private GameObject jumpscare;

    //Sanaty
    private int maxSanaty = 100;
    [SerializeField] private float currentSanaty;
    private int sanatyGain = 1;
    private int sanatyLoss = 5;
    [SerializeField] private bool isLosingSanaty;

    [SerializeField] public Volume volume; //Volume

    //Grain
    private FilmGrain grain;

    //Vignette
    private Vignette vignette;
    private float vignetteMultiplier = 0.015f;

    //DephOfField
    private DepthOfField depht;
    private float depthMultiplier = 1f;

    //Sanaty Audio
    [SerializeField] AudioSource sanaty;
    [SerializeField] AudioClip sanatySound;
    [SerializeField] AudioSource heart;
    [SerializeField] AudioClip heartSound;
    private IEnumerator fadeIn;
    private IEnumerator fadeOut;

    private bool jumpscared = false;


    private void Start()
    {
        PostProcStart();
        currentSanaty = maxSanaty;
        isLosingSanaty = false;
        sanaty.volume = 0f;

        depht.aperture.value = 0.1f;
        depht.focusDistance.value = 0.1f;
    }

    private void Update()
    {
        if (currentSanaty <= 80 && currentSanaty > 0)
        {            
            VignetteEffect();
            SanatyEffects();
            DepthOfFieldEffect();
        }

        if(currentSanaty > 80)
        {
            ResetEffects();
        }
            

        GainSanaty();

        if (currentSanaty >= maxSanaty)
            currentSanaty = maxSanaty;
        else if (currentSanaty <= 0)
            currentSanaty = 0;

        if(currentSanaty == 0 && jumpscared == false)
        {
            jumpscare.GetComponent<SmilerJumpscare>().StartJumpscare();
            jumpscared = true;
        }
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Sanaty"))
        {
            isLosingSanaty = true;
            StartSoundSanaty();
            //StartSoundHeart();
            
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Sanaty"))
        {
            isLosingSanaty = false;            
            StopSoundSanaty();
            //StopSoundHeart();
        }
    }

    private void PostProcStart()
    {       
        volume.profile.TryGet<FilmGrain>(out grain);

        //Vignette
        volume.profile.TryGet<Vignette>(out vignette);
        vignette.intensity.value = 0;
        vignette.color.value = Color.black;

        volume.profile.TryGet<DepthOfField>(out depht);

    }

    private void GainSanaty()
    {
        if (isLosingSanaty == true && currentSanaty != 0)
            currentSanaty -= sanatyLoss * Time.deltaTime;
        else if (isLosingSanaty == false && currentSanaty != 0)
            currentSanaty += sanatyGain * Time.deltaTime;
    }

    public void GainSanatyItem(int val) //Temporario
    {
        //Debug.Log("+20");
        currentSanaty = currentSanaty + val;
    }

    private void SanatyEffects()
    {
        grain.active = true;

        if (currentSanaty >= 60 && currentSanaty <= 80)
        {
            grain.type.value = FilmGrainLookup.Medium1;
        }
        else if (currentSanaty >= 40 && currentSanaty < 60)
        {
            grain.type.value = FilmGrainLookup.Medium2;
        }
        else if (currentSanaty >= 20 && currentSanaty < 40)
        {
            grain.type.value = FilmGrainLookup.Medium4;
        }
        else if (currentSanaty >= 0 && currentSanaty < 20)
        {
            grain.type.value = FilmGrainLookup.Medium4;
        }
    }

    private void VignetteEffect()
    {      
        if (currentSanaty < 100 && isLosingSanaty)
            vignette.intensity.value += vignetteMultiplier * Time.deltaTime; //Velocidade para a vinheta aparecer
        else
            vignette.intensity.value -= (vignetteMultiplier) * Time.deltaTime; //Velocidade para a vinheta desaparecer (Multiplica por 3 pois tem que ser mais rapido)

    }

    private void DepthOfFieldEffect()
    {
        depht.active = true;

        if (currentSanaty < 80 && isLosingSanaty)
            depht.focalLength.value += depthMultiplier * Time.deltaTime; //Velocidade para a vinheta aparecer
        else
            depht.focalLength.value -= depthMultiplier * Time.deltaTime; //Velocidade para a vinheta desaparecer (Multiplica por 3 pois tem que ser mais rapido)

        /*if (currentSanaty > 30 && !isLosingSanaty)
        {
            depht.active = false;
            depht.focalLength.value = 0;
        }*/
    }

    private void ResetEffects()
    {
        grain.type.value = FilmGrainLookup.Thin1;
        depht.focalLength.value = 0;
        vignette.intensity.value = 0.1f;
        vignette.smoothness.value = 1f;
    }

    private void StartSoundSanaty()
    {
        if (fadeOut != null)
            StopCoroutine(fadeOut);

        sanaty.clip = sanatySound;
        sanaty.Play();
        fadeIn = FadeInOutSound.FadeIn(sanaty, 40f, 1f);
        StartCoroutine(fadeIn);
    }

    private void StopSoundSanaty()
    {
        fadeOut = FadeInOutSound.FadeOut(sanaty, 10f, 0f);
        if(sanaty.isPlaying)
        {
            StopCoroutine(fadeIn);
            StartCoroutine(fadeOut);
        }
    }

    /*private void StartSoundHeart()
    {
        if (fadeOut != null)
            StopCoroutine(fadeOut);

        heart.clip = heartSound;
        heart.Play();
        fadeIn = FadeInOutSound.FadeIn(heart, 40f, 1f);
        StartCoroutine(fadeIn);
    }

    private void StopSoundHeart()
    {
        fadeOut = FadeInOutSound.FadeOut(heart, 20f, 0f);
        if (heart.isPlaying)
        {
            StopCoroutine(fadeIn);
            StartCoroutine(fadeOut);
        }
    }*/
}


