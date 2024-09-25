using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScorePopup : MonoBehaviour
{
    public int scoreValue = 0;
    public float lifeTime = 3.0f;
    public float moveSpeed = 1f;
    public Vector3 spawnPos;
    float lifeTimer = 0;

    void Start()
    {
        // spawnPos = Camera.main.WorldToViewportPoint(spawnPos);
        // spawnPos = new Vector3(spawnPos.x * 250f, spawnPos.y * 160);
        // GetComponent<RectTransform>().anchoredPosition = spawnPos;
        spawnPos.y += 0.5f;
        transform.position = spawnPos;
        GetComponent<Text>().text = "+"+scoreValue;
    }

    void Update()
    {
        lifeTimer += Time.deltaTime;
        GetComponent<RectTransform>().localPosition += new Vector3(0,moveSpeed*Time.deltaTime,0);

        if (lifeTimer > lifeTime) Destroy(gameObject);
    }
}
