using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject StartGame;

    [SerializeField] private GameObject level0;

    [SerializeField] private TextBehavior textColor;

    private void Start()
    {
        playMenu.SetActive(false);
        StartGame.SetActive(false);
        textColor = GetComponent<TextBehavior>();
    }

    public void OpenPlayMenu()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void GoBack()
    {
        playMenu.SetActive(false);
        mainMenu.SetActive(true);

        //Reset
        level0.GetComponent<Image>().color = textColor.originalColor;
        StartGame.SetActive(false);
    }

    public void OnClickLevel0()
    {
        level0.GetComponent<Image>().color = textColor.wantedColor;
        StartGame.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level0");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
