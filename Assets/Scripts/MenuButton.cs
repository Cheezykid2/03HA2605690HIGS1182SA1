using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour
{
    //load scene
    public void Play()
    { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }

    //Quit game
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player said I quit");
    }
}

