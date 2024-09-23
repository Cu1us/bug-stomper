using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

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

    // Local vars
    int currentWave = 0;

    [ContextMenu("Start game")]
    void StartGame()
    {
        currentWave = 0;
        StartNextWave();
    }
    void Start()
    {
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
        foreach (Lane lane in Lanes)
        {
            lane.StartWave(wave);
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