using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("UI and Menus")]
    public Text waveText;

    public void SetWaveText(int inWave)
    {
        waveText.text = "Wave " + (inWave+1);
    }

    public void LoadScene(int sceneIn)
    {
        SceneManager.LoadScene(sceneIn);
    }

}
