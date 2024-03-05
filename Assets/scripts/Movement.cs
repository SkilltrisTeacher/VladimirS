using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 1000f;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D feetCollider;
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnimator;

    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }

    private void SwitchAnimator(float playerInput)
    {
        playerAnimator.SetBool("Run", playerInput != 0);
    }

    private void Update()
    {
        float playerInput = Input.GetAxis("Horizontal");
        Move(playerInput);
        Flip(playerInput);
        SwitchAnimator(playerInput);
        bool isGrounded = feetCollider.IsTouchingLayers(groundLayer);
        if (Input.GetKeyDown(jumpButton) && isGrounded) Jump();
    }

    private void Move(float direction)
    {
        //объявляем двумерный вектор скорости по оси Х
        Vector2 velocity = myRigidbody2D.velocity;
        //Изменение вектора движения
        myRigidbody2D.velocity = new Vector2(speed * direction, velocity.y);
    }

    private void Jump()
    {
        Vector2 jumpVector = new Vector2(0f, jumpForce);
        myRigidbody2D.AddForce(jumpVector);
    }

    private void Flip(float direction)
    {
        if (direction > 0) spriteRenderer.flipX = false;
        if (direction < 0) spriteRenderer.flipX = true;
    }

}
