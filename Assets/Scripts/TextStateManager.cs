using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TextStateManager : MonoBehaviour
{
    private PlayerStat playerStat;
    public List<TextMeshProUGUI> textMPUs;
    
    private void Awake()
    {
        playerStat = FindObjectOfType<PlayerStat>();
    }

    private void Update()
    {
        textMPUs[0].text = playerStat.redColor.ToString();
        textMPUs[1].text = playerStat.orangeColor.ToString();
        textMPUs[2].text = playerStat.yellowColor.ToString();
        textMPUs[3].text = playerStat.greenColor.ToString();
        textMPUs[4].text = playerStat.blueColor.ToString();
        textMPUs[5].text = playerStat.indigoColor.ToString();
        textMPUs[6].text = playerStat.violetColor.ToString();
        
    }
}
