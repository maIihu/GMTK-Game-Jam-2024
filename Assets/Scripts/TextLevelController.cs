
using System;
using TMPro;
using UnityEngine;

public class TextLevelController : MonoBehaviour
{
    private GameObject parentObject;
    private TextMeshProUGUI textMeshProUGUI;
    
    private void Awake()
    {
        parentObject = transform.parent.parent.gameObject;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (parentObject.transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (parentObject.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void TextUpdate(string text)
    {
        textMeshProUGUI.text = text;
    }
    
}
