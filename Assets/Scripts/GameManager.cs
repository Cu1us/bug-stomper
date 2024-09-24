using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Settings")]
    public Lane[] Lanes;
    public float TimeBetweenWaves = 1;

    [Header("Enemy prefabs")]
    public Enemy PrefabShellEnemy;
    public Enemy PrefabCaterpillar;

    [Header("Events")]

    public UnityEvent onWaveStart;
    public UnityEvent onWaveEnd;
    public UnityEvent onGameFinish;

    // Local vars
    int currentWave = 0;
    bool allWavesFinished = false;

    [ContextMenu("Start game")]
    public void StartGame()
    {
        currentWave = 0;
        StartNextWave();
    }
    void Start()
    {
        Application.targetFrameRate = 60;

        if (instance == null) instance = this;
        foreach (Lane lane in Lanes)
        {
            lane.onFinishSpawning += OnLaneFinish;
        }
    }
    void OnLaneFinish()
    {
        bool allLanesFinished = true;
        foreach (Lane lane in Lanes)
        {
            if (lane.waveActive)
            {
                allLanesFinished = false;
                break;
            }
        }
        if (allLanesFinished)
        {
            onWaveEnd?.Invoke();
            CancelInvoke(nameof(StartNextWave));
            Invoke(nameof(StartNextWave), TimeBetweenWaves);
        }
    }
    void StartWave(int wave)
    {
        onWaveStart?.Invoke();
        bool noMoreWaves = true;
        foreach (Lane lane in Lanes)
        {
            if (lane.StartWave(wave))
            {
                noMoreWaves = false;
            }
        }
        if (noMoreWaves)
        {
            allWavesFinished = true;
            //SceneManager.LoadScene(0);
        }
    }
    void StartNextWave()
    {
        StartWave(currentWave);
        currentWave++;
    }
    public static Enemy GetEnemyPrefab(WaveAction type)
    {
        return type switch
        {
            WaveAction.SHELL => instance.PrefabShellEnemy,
            WaveAction.CATERPILLAR => instance.PrefabCaterpillar,
            _ => null
        };
    }
}