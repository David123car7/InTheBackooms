using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    [SerializeField] private GameObject menuGame;
    [SerializeField] private GameObject playerOverlay;
    [SerializeField] private GameObject controllsMenu;

    private FirstPersonController fpc;
    private bool isClosed;
    public bool canOpen;

    private void Start()
    {
        menuGame.SetActive(false);
        controllsMenu.SetActive(false);
        isClosed = false;
        canOpen = true;

        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isClosed && canOpen)
        {
            OpenMenu();
            playerOverlay.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isClosed)
        {
            CloseMenu();
            playerOverlay.SetActive(true);
        }
    }

    private void OpenMenu()
    {
        menuGame.SetActive(true);
        playerOverlay.SetActive(false);
        isClosed = true;
        Time.timeScale = 0; //Pausa o tempo

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        fpc.enabled = false;
    }

    public void CloseMenu()
    {
        menuGame.SetActive(false);
        playerOverlay.SetActive(true);
        controllsMenu.SetActive(false);
        isClosed = false;
        Time.timeScale = 1; //Reseta o tempo

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        fpc.enabled = true;
    }

    public void OpenControlls()
    {
        controllsMenu.SetActive(true);
    }

    public void CloseControlls()
    {
        controllsMenu.SetActive(false);
    }


    public void ExitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

