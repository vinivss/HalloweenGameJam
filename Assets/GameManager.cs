using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject optionsScreen;
    public bool gamepaused = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamepaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }

    void PauseGame()
    {
       Instantiate(optionsScreen, new Vector3(0, 0, 0), Quaternion.identity);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gamepaused = true;

    }

    void ResumeGame()
    {
        Destroy(optionsScreen);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gamepaused = false;
    }

}
