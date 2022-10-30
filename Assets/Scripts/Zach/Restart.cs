using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public int SceneNumber;

    public void Load()
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
