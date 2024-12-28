
using System;
using UnityEngine;

public class SelectColor : MonoBehaviour
{
    [SerializeField] private GameObject redButton;
    [SerializeField] private GameObject blueButton;
    [SerializeField] private GameObject yellowButton;
    
    private ChangeColorPlayer changeColorPlayer;
    private PickUpCard pickUp;
    private PlayerStat playerStat;
    private PlayerAttack playerAttack;
    
    private void Awake()
    {
        changeColorPlayer = FindObjectOfType<ChangeColorPlayer>();
        pickUp = FindObjectOfType<PickUpCard>();
        playerStat = FindObjectOfType<PlayerStat>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    public void IsRedSelected()
    {
        changeColorPlayer.RedColorChange();
        playerAttack.currentColor = "Red";
        pickUp.isColorChangeCard = false;
    }
    public void IsBlueSelected()
    {
        changeColorPlayer.BlueColorChange();
        playerAttack.currentColor = "Blue";
        pickUp.isColorChangeCard = false;
    }
    public void IsYellowSelected()
    {
        changeColorPlayer.YellowColorChange();
        playerAttack.currentColor = "Yellow";
        pickUp.isColorChangeCard = false;
    }
}
