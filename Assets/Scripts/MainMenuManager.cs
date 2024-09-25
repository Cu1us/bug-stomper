using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public int GameScene;



    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
