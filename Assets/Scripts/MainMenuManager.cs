using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    public int GameScene;
    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }
}
