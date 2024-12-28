using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public string currentColor = "Blue";
    private TextLevelController textLevelController;
    private PlayerStat playerStat;
    public float scaleFactor = 1.1f;
    public bool isAttacking = false;
    public bool isGameOver = false;
    public bool isVictory = false;
    private AudioManager audioManager;
    public PlayerStat PlayerStat
    {
        get => playerStat;
        set => playerStat = value;
    }

    private void Awake()
    {
        // Lấy TextLevelController từ con của đối tượng hiện tại
        textLevelController = GetComponentInChildren<TextLevelController>();
        playerStat = GetComponent<PlayerStat>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        transform.localScale *= Mathf.Pow(scaleFactor, playerStat.levelCharacter);
    }

    private void Update()
    {
        textLevelController.TextUpdate(playerStat.levelCharacter.ToString());
        if (playerStat.redColor > 0 && playerStat.blueColor > 0 
                                    && playerStat.yellowColor > 0 && playerStat.orangeColor > 0 
                                    && playerStat.indigoColor > 0 && playerStat.violetColor > 0 
                                    && playerStat.greenColor > 0)
        {
            isVictory = true;
        }
        if (isColliding && Input.GetKeyDown(KeyCode.Space))
        {
            audioManager.PlaySfx(audioManager.eating);
            if (currentCollision != null && playerStat.levelCharacter >= currentCollision.gameObject.GetComponent<EnemyController>().LevelCharacter)
            {
                if (currentColor == "Blue") playerStat.blueColor++;
                else if (currentColor == "Yellow") playerStat.yellowColor++;
                else playerStat.redColor++;
            
                isAttacking = true;

                playerStat.currenExp += currentCollision.gameObject.GetComponent<EnemyController>().LevelCharacter;
            
                if (playerStat.currenExp >= playerStat.levelCharacter)
                {
                    playerStat.currenExp -= playerStat.levelCharacter;
                    playerStat.levelCharacter++;
                    audioManager.PlaySfx(audioManager.powerUp);
                }
            
                if (playerStat.levelCharacter >= 10) isGameOver = true;
                transform.localScale *= scaleFactor;
                Destroy(currentCollision.gameObject);
            }
        
            // Reset cờ sau khi xử lý va chạm
            isColliding = false;
            currentCollision = null;
        }
    }

    private Collider2D currentCollision;
    private bool isColliding;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(currentColor))
        {
            currentCollision = other;
            isColliding = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == currentCollision)
        {
            isColliding = false;
            currentCollision = null;
        }
    }


    public void TakeDamage()
    {
        playerStat.levelCharacter--;
        transform.localScale /= scaleFactor;
        if (playerStat.levelCharacter <= 0) isGameOver = true;
    }
}