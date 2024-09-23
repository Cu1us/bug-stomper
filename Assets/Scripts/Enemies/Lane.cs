using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Lane : MonoBehaviour
{
    public Wave[] Waves;

    int waveNumber = 0;
    bool waveActive = false;
    int currentWaveStep = 0;

    Wave currentWave { get { return (waveNumber >= 0 && waveNumber < Waves.Length) ? Waves[waveNumber] : null; } } // Returns the current wave object, or null if one isn't defined for this lane
    public UnityAction onFinishSpawning;

    [ContextMenu("Start wave 0 on this lane")]
    void StartWave1Test()
    {
        if (!Application.isPlaying) return;
        StartWave(0);
    }
    public void StartWave(int wave)
    {
        waveNumber = wave;
        if (waveActive)
        {
            CancelInvoke(nameof(TickWave));
        }
        currentWaveStep = 0;
        InvokeRepeating(nameof(TickWave), 0, currentWave.timeBetweenActions);
    }
    public void StopWave()
    {
        CancelInvoke(nameof(TickWave));
        waveActive = false;
        onFinishSpawning.Invoke();
    }
    void TickWave() // Runs the current wave's next action
    {
        DoWaveAction(currentWave.Actions[currentWaveStep]);
        currentWaveStep++;
        if (currentWaveStep > currentWave.Actions.Length)
        {
            StopWave();
        }
    }
    void DoWaveAction(WaveAction action)
    {
        if (action == WaveAction.WAIT) return;
        Enemy enemyPrefab = GameManager.GetEnemyPrefab(action);
        SpawnEnemy(enemyPrefab);
    }
    void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, transform);
    }
}

public enum WaveAction
{
    WAIT,
    SHELL,
    CATERPILLAR
}

[System.Serializable]
public class Wave
{
    [SerializeField]
    public float timeBetweenActions = 1f;
    [SerializeField]
    public WaveAction[] Actions;
}