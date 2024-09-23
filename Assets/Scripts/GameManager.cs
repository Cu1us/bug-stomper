using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Lane[] Lanes;

    public Enemy PrefabShellEnemy;
    public Enemy PrefabCaterpillar;

    [ContextMenu("Start wave 0 on all lanes")]
    void StartWave1Test()
    {
        if (!Application.isPlaying) return;
        StartWave(0);
    }
    void Start()
    {
        if (instance == null) instance = this;
    }
    void StartWave(int wave)
    {
        foreach (Lane lane in Lanes)
        {
            lane.StartWave(wave);
        }
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