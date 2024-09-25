using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    static CameraShake instance;
    static float Intensity = 0;
    public float intensityModifier = 1;
    public Vector3 cameraPosition;
    void Start()
    {
        instance = this;
        cameraPosition = transform.position;
    }
    public static void Play(float intensity)
    {
        if (intensity <= 0)
        {
            instance.cameraPosition = instance.transform.position;
            Intensity = intensity;
        }
        else
            Intensity += intensity;
    }
    void Update()
    {
        if (Intensity > 0)
        {
            transform.position = cameraPosition + (Vector3)Random.insideUnitCircle * Intensity * intensityModifier;
            Intensity -= Time.deltaTime;
            if (Intensity <= 0)
            {
                Intensity = 0;
                transform.position = cameraPosition;
            }
        }
    }
}
