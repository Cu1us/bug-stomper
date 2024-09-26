using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathSprite : MonoBehaviour
{
    public static DeathSprite SpawnDeathSprite(Vector3 position, EnemyType type)
    {
        DeathSprite sprite = Instantiate(GameManager.instance.PrefabDeathSprite, position, Quaternion.identity);
        sprite.Play(type);
        return sprite;
    }

    void Play(EnemyType type)
    {
        string animation = type switch
        {
            EnemyType.WORM => "Worm",
            EnemyType.SHELLMET => "Shellmet",
            EnemyType.SNAIL => "Snail",
            EnemyType.CATERPILLAR => "Caterpillar",
            EnemyType.MUSHROOM => "Mushroom",
            _ => "Undefined"
        };
        GetComponent<Animator>().Play(animation);
    }
    public void OnFinishAnimation() // needs to be called on by an animation event
    {
        Destroy(gameObject);
    }

    public enum EnemyType
    {
        WORM,
        SHELLMET,
        SNAIL,
        CATERPILLAR,
        MUSHROOM
    }
}
