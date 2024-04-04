using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSystem : MonoBehaviour
{
    [SerializeField] private GameObject deathUI;
    private FirstPersonController fpc;

    void Start()
    {
        deathUI.SetActive(false);
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }

    public void Death()
    {
        deathUI.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

}
