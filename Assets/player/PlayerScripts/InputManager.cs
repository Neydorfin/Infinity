using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerLocomotion playerLocomotion;
    PlayerController playerInput;
    AnimationManager animationManager;
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;
    private float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool jump_Input;

    private void Awake() 
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animationManager = GetComponent<AnimationManager>();
    }

    private void OnEnable() 
    {
        if (playerInput == null)
        {
            playerInput = new PlayerController();

            playerInput.playerControl.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerInput.playerControl.CameraMove.performed += i => cameraInput = i.ReadValue<Vector2>(); 

            playerInput.playerActions.Jump.performed +=i => jump_Input = true;
        }

        playerInput.Enable();
    }

    private void OnDisable() 
    {
        playerInput.Disable();
    }

    public void HandleAllInputs()
    {
        if(Time.timeScale == 0f)
        {
            cameraInputX = 0;
            cameraInputY = 0;
        }
        else
        {
            HandleMovementInput();
            HandleJumpingInput();
        }
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animationManager.UpdateAnimatorValues(0,moveAmount);
    }

    private void HandleJumpingInput()
    {
        if(jump_Input)
        {
            jump_Input = false;
            playerLocomotion.HandleJumping();
        } 
    }

}
