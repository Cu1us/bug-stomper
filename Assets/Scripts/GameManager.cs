using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Settings")]
    public Lane[] Lanes;
    public float TimeBetweenWaves = 1;

    [Header("Enemy prefabs")]
    public ShellEnemy PrefabShellEnemy;
    public CaterpillarEnemy PrefabCaterpillar;
    public SnailEnemy PrefabSnail;
    public WormEnemy PrefabWorm;

    [Header("Misc. prefabs")]
    public DeathSprite PrefabDeathSprite;



    [Header("Events")]

    public UnityEvent<int> onWaveStart;
    public UnityEvent<int> onWaveEnd;
    public UnityEvent onGameFinish;
    public UnityEvent onGameLose;
    [Header("Object references")]
    public Transform LaneEndTransform;

    [Header("UI and Menus")]
    public Text waveText;

    // Local vars
    public int currentWave = 0;
    bool allWavesFinished = false;
    bool gameActive = false;

    [ContextMenu("Start game")]
    public void StartGame()
    {
        if (gameActive) return;
        gameActive = true;
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
        StartGame();
    }
    void OnLaneFinish()
    {
        if (!gameActive) return;
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
            onWaveEnd?.Invoke(currentWave);
            CancelInvoke(nameof(StartNextWave));
            Invoke(nameof(StartNextWave), TimeBetweenWaves);
        }
    }
    void StartWave(int wave)
    {
        if (!gameActive) return;
        onWaveStart?.Invoke(wave);
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
        }
    }
    void StartNextWave()
    {
        if (!gameActive) return;
        StartWave(currentWave);
        currentWave++;
    }

    public void SkipTutorial()
    {
        foreach (Lane lane in Lanes)
        {
            Enemy[] enemies = lane.GetComponentsInChildren<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }
        StartNextWave();
    }
    void Update()
    {
        if (allWavesFinished && gameActive)
        {
            bool allEnemiesDead = true;
            foreach (Lane lane in Lanes)
            {
                if (lane.transform.childCount > 0)
                {
                    allEnemiesDead = false;
                    break;
                }
            }
            if (allEnemiesDead)
            {
                gameActive = allWavesFinished = false;
                onGameFinish?.Invoke();
            }
        }
    }
    public static Enemy GetEnemyPrefab(WaveAction type)
    {
        return type switch
        {
            WaveAction.SHELL => instance.PrefabShellEnemy,
            //WaveAction.CATERPILLAR => instance.PrefabCaterpillar,
            WaveAction.SNAIL => instance.PrefabSnail,
            WaveAction.WORM => instance.PrefabWorm,
            _ => null
        };
    }
    public void OnEnemyReachEnd(Enemy enemy)
    {
        if (!gameActive) return;
        onGameLose?.Invoke();
        gameActive = false;
    }
}