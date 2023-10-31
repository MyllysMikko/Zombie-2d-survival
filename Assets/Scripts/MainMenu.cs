using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Käynistettävän Scene
    public string startLevel;
    public void NewGame()
    {

        // Aloitetaan uusi peli
        SceneManager.LoadScene(startLevel);
    }
}