using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private int levelCharacter;
    private TextLevelController textLevelController;
    private SpriteRenderer spriteRenderer;
    private Animator ani;
    private Transform player;
    private float moveSpeed = 2f;
    private PlayerAttack playerAttack;
    
    public int LevelCharacter
    {
        get => levelCharacter;
        set => levelCharacter = value;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textLevelController = GetComponentInChildren<TextLevelController>();
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerAttack = FindObjectOfType<PlayerAttack>();
        levelCharacter = Random.Range(1, 10);
    }

    private void Start()
    {
        textLevelController.TextUpdate(levelCharacter.ToString());
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if(playerAttack.isAttacking)
        {
            if (distanceToPlayer < 3f)
            {
                if (levelCharacter > playerAttack.PlayerStat.levelCharacter)
                {
                    EnemyClover();
                    if (player.position.x < transform.position.x)
                        transform.localScale = new Vector3(-1, 1, 1);
                    else
                        transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    EnemyAway();
                    if (player.position.x > transform.position.x)
                        transform.localScale = new Vector3(-1, 1, 1);
                    else
                        transform.localScale = new Vector3(1, 1, 1);
                }
            }
            StartCoroutine(DisableAttackingAfterDelay(2f));
        }
    }
    private IEnumerator DisableAttackingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerAttack.isAttacking = false;
    }
    private void EnemyAway()
    {
        Vector2 direction = (transform.position - player.position).normalized;
        
        transform.position = Vector2.MoveTowards(transform.position, 
            transform.position + (Vector3)direction, moveSpeed * Time.deltaTime);
    }

    private void EnemyClover()
    {
        Vector2 direction = (player.position - transform.position).normalized;

        transform.position = Vector2.MoveTowards(transform.position, 
            player.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerAttack>().isAttacking)
            {
                other.gameObject.GetComponent<PlayerAttack>().TakeDamage();
            }
        }
    }
}