using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [Header("UI and Menus")]
    public Text waveText;
    public Text scoreText;

    void Start()
    {
        Score.onAddScore += SetScoreText;
        Score.SetScore(0);
    }

    public void SetWaveText(int inWave)
    {
        waveText.text = "Wave " + (inWave+1);
    }

    public void SetScoreText(int scoreIn)
    {
        scoreText.text = ""+scoreIn;
    }


    public void LoadScene(int sceneIn)
    {
        SceneManager.LoadScene(sceneIn);
    }

    private void OnDestroy() 
    {
        Score.onAddScore -= SetScoreText;
    }
}

public static class Score
{
    public static int score = 0;
    public static Action<int> onAddScore;
    
    public static void AddScore(int scoreIn)
    {
        score += scoreIn;
        onAddScore?.Invoke(score);
    }

    public static void SetScore(int scoreIn)
    {
        score = scoreIn;
        onAddScore?.Invoke(score);
    }
}
