using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{

    public GameManager Pause;
    public GameObject optionsScreen;

    void start()
    {
        optionsScreen = GameObject.FindGameObjectWithTag("Menu");
    }
    public void ResumeGame()
    {
        Destroy(optionsScreen);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Pause.gamepaused = false;
    }
}
