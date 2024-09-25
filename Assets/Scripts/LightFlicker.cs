using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LightFlicker : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField]
    float flickerSpeed;
    [SerializeField]
    [Range(0,1)]
    float flickerIntensity;
    [SerializeField]
    [Range(0,1)]
    float baseIntensity;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float alpha = Mathf.PerlinNoise1D(Time.time * flickerSpeed) * flickerIntensity;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha + baseIntensity);
    }
}
