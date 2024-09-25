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
    public ScorePopup scorePopup;
    public Canvas worldCanvas;

    int score = 0;

    void Start()
    {
        Score.onAddScore += SetScoreText;
        Score.SetScore(score);
        Score.onAddScore += SpawnPopupText;
        SetWaveText(0);
    }

    public void SetWaveText(int inWave)
    {
        waveText.text = "Wave " + (inWave);
    }

    public void SetScoreText(int scoreIn, Vector3 posIn)
    {
        score += scoreIn;
        scoreText.text = ""+score;
    }

    void SpawnPopupText(int scoreIn, Vector3 posIn)
    {
        var popup =  Instantiate(scorePopup, worldCanvas.transform);
        popup.spawnPos = posIn;
        popup.scoreValue = scoreIn;
    }


    public void LoadScene(int sceneIn)
    {
        SceneManager.LoadScene(sceneIn);
    }

    private void OnDestroy() 
    {
        Score.onAddScore -= SetScoreText;
        Score.onAddScore -= SpawnPopupText;
    }
}

public static class Score
{
    public static Action<int,Vector3> onAddScore;
    
    public static void AddScore(int scoreIn, Vector3 posIn)
    {
        onAddScore?.Invoke(scoreIn,posIn);
    }

    public static void SetScore(int scoreIn)
    {
        onAddScore?.Invoke(scoreIn,Vector3.zero);
    }
}
