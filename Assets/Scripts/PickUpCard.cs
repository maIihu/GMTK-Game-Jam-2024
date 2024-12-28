using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUpCard : MonoBehaviour
{
    public bool isColorChangeCard = false;
    public bool isFusionCard = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Color Change Card"))
        {
            isColorChangeCard = true;
        }

        if (other.gameObject.CompareTag("Fusion Card"))
        {
            isFusionCard = true;
        }
    }
}
