using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public Wave[] Waves;
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