using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOutSound : MonoBehaviour
{
    public static float minVolume = 0f;
    public static float maxVolume = 1f;

    public static FadeInOutSound instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public static void StartSound()
    {
        
    }

    public static void StopMusic()
    {

    }

    public static IEnumerator FadeIn(AudioSource aSource, float duration, float targetVolume)
    {
        float timer = 0f;
        float currentVolume = aSource.volume;
        float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume);

        while(timer < duration)
        {
            timer += Time.deltaTime;
            var newVolume = Mathf.Lerp(currentVolume, targetValue, timer / duration);
            aSource.volume = newVolume;
            yield return null;
        }
            
    }

    public static IEnumerator FadeOut(AudioSource aSource, float duration, float targetVolume)
    {
        float timer = 0f;
        float currentVolume = aSource.volume;
        float targetValue = Mathf.Clamp(targetVolume, minVolume, maxVolume) * Time.deltaTime;

        while (aSource.volume > 0)
        {
            timer  += Time.deltaTime;
            var newVolume = Mathf.Lerp(currentVolume, targetValue, timer / duration);
            aSource.volume = newVolume;
            yield return null;
        }
    }
}

/*public static IEnumerator FadeIn(AudioSource audioSource, float duration, float targetVolume, bool canSound)
{
    float currentTime = 0;
    float start = audioSource.volume;
    while (currentTime < duration && canSound) 
    {
        currentTime += Time.deltaTime;
        audioSource.volume = Mathf.Lerp(start, 1, currentTime / duration);
        yield return null;
    }
    yield break;
}

public static IEnumerator FadeOut(AudioSource audioSource, float duration, float targetVolume, bool canSound)
{
    float currentTime = 0;
    float start = audioSource.volume;
    while (currentTime < duration && !canSound)
    {
        currentTime += Time.deltaTime;
        audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
        yield return null;
    }
    yield break;
}*/