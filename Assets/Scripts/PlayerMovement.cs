using TMPro;
using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [SerializeField] private float moveSpeed = 5f;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        // Lưu lại scale hiện tại của trục y và z
        float currentScaleY = transform.localScale.y;
        float currentScaleZ = transform.localScale.z;

        // Chỉ thay đổi trục x để điều chỉnh hướng
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1 * Mathf.Abs(transform.localScale.x), currentScaleY, currentScaleZ);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), currentScaleY, currentScaleZ);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * (moveSpeed * Time.fixedDeltaTime));
    }
}