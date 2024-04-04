 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private GameObject finishLevelHUD;
    [SerializeField] private MenuInGame menuIngame; //Menu do jogo
    [SerializeField] private GameObject timer; //Timer do final do jogo
    [SerializeField] private GameObject playerOverlay;
    private FirstPersonController fpc;
    private bool isTrigger;

    private void Start()
    {
        finishLevelHUD.SetActive(false);
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        isTrigger = false;
        timer.SetActive(false);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player") && isTrigger == false) //Se colidir com um collider tagado com Exit
        {
            finishLevelHUD.SetActive(true);
            Time.timeScale = 0; //Pausa o tempo

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            fpc.enabled = false;

            menuIngame.canOpen = false; //Nao pode abrir o menu
            timer.SetActive(true);
            playerOverlay.SetActive(false);

            isTrigger = true;
            Debug.Log("Exit");
        }
    }

    private void OnTriggerExit(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            isTrigger = false;
            menuIngame.canOpen = true; //Pode abrir o menu
            timer.SetActive(false);
            playerOverlay.SetActive(true);
        }
    }
}
