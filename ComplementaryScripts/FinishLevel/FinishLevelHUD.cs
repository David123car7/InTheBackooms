using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevelHUD : MonoBehaviour
{
    [SerializeField] private GameObject finishLevelHUD;
    [SerializeField] private GameObject playerOverlay;
    [SerializeField] private GameObject timer;
    private FirstPersonController fpc;

    private void Start()
    {
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    public void Continue()
    {
        finishLevelHUD.SetActive(false);
        Time.timeScale = 1; //Reseta o tempo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fpc.enabled = true;
        playerOverlay.SetActive(true);
        timer.SetActive(false);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
