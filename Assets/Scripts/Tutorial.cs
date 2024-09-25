using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Tutorial setup:")]
    [SerializeField]string[] tutorialTexts;
    [SerializeField]float[] timings;

    Text text;
    float timer = 0;
    int tutTipNr = 0;

    void Start()
    {
        text = GetComponent<Text>();
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (tutTipNr > tutorialTexts.Count()-1) return;

        if (timer > timings[tutTipNr])
        {
            timer = 0;
            text.text = tutorialTexts[tutTipNr];
            tutTipNr ++;
        }
    }
}
