using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    AnimationManager animationManager;
    PlayerManager playerManager;
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity = 3;
    public float fallingVelocity = 33;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isInteracting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Jump Speeds")]
    public float jumpHeight = 1;
    public float gravityIntensity = -5;

    
    [Header("Movement Speeds")]
    public float movementSpeed = 7f;
    public float rotationSpeed = 15f;

    private void Awake() 
    {
        animationManager = GetComponent<AnimationManager>();
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }
    public void HandleAllMovement()
    {
        HandleFallindAndLanding();

        if(playerManager.isInteracting)
            return;
        
        HandleMovement();
        HandleRotation();
        
    }
    private void HandleMovement()
    {
        if(isJumping)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        if(isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward; 

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }   

    private void HandleFallindAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if(!isGrounded && !isJumping)
        {
            if(!playerManager.isInteracting)
            {
                animationManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);   
        }

        if(Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, 0.5f, groundLayer))
        {
            if(!isGrounded && playerManager.isInteracting)
            {   
                animationManager.PlayTargetAnimation("Landing", true);
            }
            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false; 
        }
    }

    public void HandleJumping()
    {
        if(isGrounded)
        {
            animationManager.animator.SetBool("isJumping", true);
            animationManager.PlayTargetAnimation("Jump", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;  
        }
    }
}
