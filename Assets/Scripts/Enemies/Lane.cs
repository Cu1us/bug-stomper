using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Wave[] Waves;

    int waveNumber = 0;
    public bool waveActive { get; protected set; } = false;
    int currentWaveStep = 0;

    public Wave currentWave { get { return (waveNumber >= 0 && waveNumber < Waves.Length) ? Waves[waveNumber] : null; } } // Returns the current wave object, or null if one isn't defined for this lane
    public System.Action onFinishSpawning;

    public bool StartWave(int wave) // returns false if there are no more waves
    {
        if (wave >= Waves.Length) return false;
        waveNumber = wave;
        if (waveActive)
        {
            CancelInvoke(nameof(TickWave));
        }
        currentWaveStep = 0;
        InvokeRepeating(nameof(TickWave), 0, currentWave.timeBetweenActions);
        return true;
    }
    public void StopWave()
    {
        CancelInvoke(nameof(TickWave));
        waveActive = false;
        onFinishSpawning?.Invoke();
    }
    void TickWave() // Runs the current wave's next action
    {
        if (Waves.Length < waveNumber || currentWave.Actions.Length == 0)
        {
            StopWave();
            return;
        }
        DoWaveAction(currentWave.Actions[currentWaveStep]);
        currentWaveStep++;
        if (currentWaveStep >= currentWave.Actions.Length)
        {
            StopWave();
        }
    }
    void DoWaveAction(WaveAction action)
    {
        if (action == WaveAction.WAIT) return;
        if (action == WaveAction.CATERPILLAR_LONG || action == WaveAction.CATERPILLAR_SHORT)
        {
            CaterpillarEnemy enemy = Instantiate(GameManager.instance.PrefabCaterpillar, transform);
            enemy.parentLane = this;
            enemy.transform.position = transform.position;
            int segmentCount = action switch
            {
                WaveAction.CATERPILLAR_SHORT => 3,
                WaveAction.CATERPILLAR_LONG => 5,
                _ => 4
            };
            segmentCount += Random.Range(0,2);
            enemy.Spawn(segmentCount);
        }
        else
        {
            Enemy enemyPrefab = GameManager.GetEnemyPrefab(action);
            SpawnEnemy(enemyPrefab);
        }
    }
    void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, transform);
        enemy.parentLane = this;
        enemy.transform.position = transform.position;
    }
}

public enum WaveAction
{
    WAIT,
    SHELL,
    CATERPILLAR_SHORT,
    CATERPILLAR_LONG,
    SNAIL
}

[System.Serializable]
public class Wave
{
    [SerializeField]
    public float timeBetweenActions = 1f;
    [SerializeField]
    public float enemyMoveSpeedMultiplier = 1f;
    [SerializeField]
    public WaveAction[] Actions;
}