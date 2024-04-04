using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlashLightSystem : MonoBehaviour
{
    private float maxBattery = 100f; //Bateria maxima
    [SerializeField] public float currentBattery; //Bateria no momento
    private float batteryloss = 2f; //Quantidade que se perde de bateria
    private bool hasBattery; //Tem bateria?
    [SerializeField] private Light FlashLight; //GameObject da flashlight
    private bool isActive; //Esta ligada?

    [SerializeField] private AudioSource flashLightAudio;
    [SerializeField] private AudioClip switch_;


    //FlickerFlashLight
    private bool isFlickering = false;
    private float timeDelay;


    private void Start()
    {
        FlashLight.gameObject.SetActive(false);
        isActive = false;

        
        isFlickering = false;
        currentBattery = maxBattery;
    }

    private void Update()
    {
        if (currentBattery <= 0)
        {
            currentBattery = 0;
            FlashLight.gameObject.SetActive(false);
        }
        else if (currentBattery >= maxBattery)
            currentBattery = maxBattery;

        if (currentBattery < 20 && currentBattery > 0 && isActive && !isFlickering)
            StartCoroutine(FlickeringLight());

        CheckBattery();
        BatterySystem();
        InputFlashLight();
        
    }

    private void CheckBattery()
    {
        if (currentBattery <= 0)
        {
            hasBattery = false;
        }
        else
        {
            hasBattery = true;
        }
    }

    private void InputFlashLight()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (isActive == false && hasBattery)
            {
                FlashLight.gameObject.SetActive(true);
                flashLightAudio.PlayOneShot(switch_);
                isActive = true;
            }
            else
            {
                FlashLight.gameObject.SetActive(false);
                flashLightAudio.PlayOneShot(switch_);
                isActive = false;
            }
        }
    }

    private void BatterySystem()
    {
        if (isActive == true && hasBattery)
        {
            currentBattery -= batteryloss * Time.deltaTime;
        }
    }

    public void GainBattery(int val) //Temporario
    {
        currentBattery = currentBattery + val;
    }

    private IEnumerator FlickeringLight()
    {
        isFlickering = true;
        FlashLight.enabled = false; //Desliga luz       
        timeDelay = Random.Range(0.01f, 0.3f);
        yield return new WaitForSeconds(timeDelay);
        FlashLight.enabled = true; //Liga a luz
        timeDelay = Random.Range(0.01f, 0.3f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}
