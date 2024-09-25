using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    int currentWave;
    string[] tutorialTexts;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = GameManager.instance.currentWave;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave == 0)
        {
            Debug.Log("Do tutorial text");
        }
    }
}
