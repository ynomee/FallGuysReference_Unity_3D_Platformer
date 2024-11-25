using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : AudioSounds
{
    [Header("Movement")]
    public float moveSpeed;
    public float sprintSpeed;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldawn;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;
    public Animator playerAnimator;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private bool _isWalking = false;
    private Coroutine _footstepCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 dir = Vector3.down;
        Debug.DrawRay(transform.position, dir, Color.red);
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);

        MyInput();
        SpeedControl();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // Проверка на падение
        if (!grounded && rb.velocity.y < 0)
        {
            playerAnimator.SetBool("isFalling", true);
        }
        else
        {
            playerAnimator.SetBool("isFalling", false);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Обновление параметра скорости для анимаций
        float speed = Mathf.Sqrt(horizontalInput * horizontalInput + verticalInput * verticalInput);
        playerAnimator.SetFloat("Speed", speed);

        //when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            PlaySound(0, volume: 0.6f);

            Invoke(nameof(ResetJump), jumpCooldawn);

            playerAnimator.SetBool("isJumping", true);    
        }
        else
            playerAnimator.SetBool("isJumping", false);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded && (horizontalInput != 0 || verticalInput != 0))
        {
            if (!_isWalking)
            {
                _isWalking = true;
                _footstepCoroutine = StartCoroutine(PlayFootsteps());  // Запуск корутины для шагов
            }

            if (Input.GetKey(sprintKey))
            {
                rb.AddForce(moveDirection.normalized * sprintSpeed * 10f, ForceMode.Force);
            }
            else
            {
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
            }
        }
        else
        {
            if (_isWalking)
            {
                _isWalking = false;
                if (_footstepCoroutine != null)
                {
                    StopCoroutine(_footstepCoroutine);  // Остановка шагов, если игрок не двигается
                }
            }
        }


        // В воздухе
        if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if (grounded && horizontalInput == 0 && verticalInput == 0)
        {
            playerAnimator.SetFloat("Speed", 0);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Ограничение скорости
        if (flatVelocity.magnitude > sprintSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * sprintSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private IEnumerator PlayFootsteps()
    {
        while (_isWalking && grounded)  // Проигрывать шаги только если игрок идет и стоит на земле
        {
            PlaySound(0, random: true, volume: 0.5f);;       // Звук шагов
            yield return new WaitForSeconds(0.3f);  // Интервал между шагами (настраивается)
        }
    }

}
