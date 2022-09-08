using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI Instance;
    public TMP_Text moneyText;
    public TMP_Text betText;
    public TMP_Text[] historyTexts;
    public TMP_Text[] historyMultiplierTexts;
    public GameObject[] historyBars;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
