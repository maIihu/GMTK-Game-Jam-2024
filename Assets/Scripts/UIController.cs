using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
[System.Serializable]
public class ColorPrefabEntry
{
    public GameObject prefab;
    public int quantity;
}

public class UIController : MonoBehaviour
{
    private PickUpCard pickUp;
    [SerializeField] private GameObject selectColorPanel;
    [SerializeField] private GameObject fusionPanel;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private List<ColorPrefabEntry> colorPrefabs;

    private PlayerAttack playerAttack;

    [SerializeField] private GameObject pausePanel;
    
    private GameObject currentColorPrefab;

    private PlayerStat playerStat;

    private bool isPauseGame = false;
    private void Awake()
    {
        pickUp = FindObjectOfType<PickUpCard>();
        playerStat = FindObjectOfType<PlayerStat>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    private void Start() 
    {
        selectColorPanel.SetActive(false);
        fusionPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
    }

    private void Update() 
    {
        if (pickUp.isColorChangeCard)
        {
            selectColorPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (pickUp.isFusionCard)
        {
            fusionPanel.SetActive(true);
            if (currentColorPrefab == null) 
                InstantiateRandomColorPrefab(); 
            Time.timeScale = 0f;
        }
        else if (isPauseGame)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
        }
        else if (playerAttack.isGameOver)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
        }
        else if (playerAttack.isVictory)
        {
            Time.timeScale = 0f;
            victoryPanel.SetActive(true);
        }
        else
        {
            selectColorPanel.SetActive(false);
            fusionPanel.SetActive(false);
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void InstantiateRandomColorPrefab()
    {
        List<ColorPrefabEntry> availableColorPrefabs = new List<ColorPrefabEntry>();

        if (playerStat.redColor > 1 && playerStat.yellowColor > 1)
        {
            colorPrefabs[2].quantity = 1;
            playerStat.redColor--;
            playerStat.yellowColor--;
        }
        if (playerStat.redColor > 1 && playerStat.blueColor > 1)
        {
            colorPrefabs[3].quantity = 1;
            playerStat.redColor--;
            playerStat.blueColor--;
        }
        if (playerStat.blueColor > 1 && playerStat.yellowColor > 1)
        {
            colorPrefabs[0].quantity = 1;
            playerStat.blueColor--;
            playerStat.yellowColor--;
        }
        if (playerStat.blueColor > 1 && playerStat.violetColor > 1)
        {
            colorPrefabs[1].quantity = 1;
            playerStat.blueColor--;
            playerStat.violetColor--;
        }

        // Tìm các màu khả dụng
        foreach (var colorPrefab in colorPrefabs)
        {
            if (colorPrefab.quantity == 1)
            {
                availableColorPrefabs.Add(colorPrefab);
                //colorPrefab.quantity = 0;
            }
        }
        
        
        if (availableColorPrefabs.Count > 0)
        {
            System.Random random = new System.Random();
            int index = random.Next(availableColorPrefabs.Count);
            GameObject randomColorPrefab = availableColorPrefabs[index].prefab;

            // if (randomColorPrefab.CompareTag("Green")) playerStat.greenColor++;
            // else if (randomColorPrefab.CompareTag("Indigo")) playerStat.indigoColor++;
            // else if (randomColorPrefab.CompareTag("Orange")) playerStat.orangeColor++;
            // else if (randomColorPrefab.CompareTag("Violet")) playerStat.violetColor++;
            
            currentColorPrefab = Instantiate(randomColorPrefab, fusionPanel.transform.position, Quaternion.identity, fusionPanel.transform);
        }
        else
        {
           // Debug.LogWarning("No color prefabs available with quantity greater than 1.");
        }
    }




    public void ReceiveFusionColor()
    {
        pickUp.isFusionCard = false;
        ColorPrefabEntry colorPrefabEntry = null;

        if (currentColorPrefab.CompareTag("Orange"))
        {
            playerStat.orangeColor++;
            colorPrefabEntry = colorPrefabs.Find(entry => entry.prefab.CompareTag("Orange"));
        }
        else if (currentColorPrefab.CompareTag("Green"))
        {
            playerStat.greenColor++;
            colorPrefabEntry = colorPrefabs.Find(entry => entry.prefab.CompareTag("Green"));
        }
        else if (currentColorPrefab.CompareTag("Indigo"))
        {
            playerStat.indigoColor++;
            colorPrefabEntry = colorPrefabs.Find(entry => entry.prefab.CompareTag("Indigo"));
        }
        else if (currentColorPrefab.CompareTag("Violet"))
        {
            playerStat.violetColor++;
            colorPrefabEntry = colorPrefabs.Find(entry => entry.prefab.CompareTag("Violet"));
        }
        
        if (colorPrefabEntry != null)
        {
            colorPrefabEntry.quantity++;
        }
    
        if (currentColorPrefab != null)
        {
            Destroy(currentColorPrefab);
            currentColorPrefab = null;
        }
    
    }
    
    public void PauseGame()
    {
        isPauseGame = true;
    }

    public void OnClickResumeButton()
    {
        isPauseGame = false;
    }

    public void QuitButton()
    {
        SceneManager.LoadScene("Menu Scene");
    }
}
