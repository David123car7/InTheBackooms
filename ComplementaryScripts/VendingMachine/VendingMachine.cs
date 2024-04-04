using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] private Transform machine;
    [SerializeField] private GameObject drink;
    [SerializeField] private Interactions isVended;

    //Audio
    private AudioSource machineAudio;
    [SerializeField] private AudioClip machineClip;

    private void Start()
    {
        machine = gameObject.GetComponent<Transform>();
        machineAudio = machine.GetComponent<AudioSource>();
        drink = machine.Find("drink").gameObject;
        drink.SetActive(false);        
    }

    public void SpawnDrink()
    {
        machineAudio.PlayOneShot(machineClip);
        Invoke("SpawnDrink2", 10f);
    }

    public void SpawnDrink2()
    {
        drink.SetActive(true);
        isVended.isVended = false;
    }
}


/*
  public void SpawnDrink()
    {
        Vector3 drinkSpawn = new Vector3(machine.transform.localPosition.x - 0.914f, machine.transform.localPosition.y + 0.85f, machine.transform.localPosition.z + 0.253f); //Setting the position of the spawn
        
        Quaternion drinkRotation = Quaternion.identity;
        drinkRotation.eulerAngles = new Vector3(-10, 270, 90); //Rotation of the drink 

        drink = Instantiate(drink, drinkSpawn, drinkRotation) as GameObject; //Spawning the drink
        drink.transform.SetParent(machine.transform); //Setting the parent 

        //machineAudio.PlayOneShot(machineClip); //Playing Sound

        isDrinkSpawned = true;
    }
 */