 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform;
    public Transform cameraPivot;
    public Transform cameraTransform;
    public LayerMask collisionLayers ;
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float mouseSensitive = 0.5f;
    public float minCollisionOffSet = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraCollisionOffSet = 0.2f;
    public float cameraFollowSpeed = 0.05f;
    public float cameraLookSpeed = 0.5f;
    public float cameraPivotSpeed = 0.5f;

    public float lookAngle;
    public float pivotAngle;
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;

    private void Awake() 
    {
        targetTransform = FindObjectOfType<PlayerManager>().transform;    
        inputManager = FindObjectOfType<InputManager>();
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }
    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);
        
        rotation = Vector3.zero;
        rotation.y = lookAngle * mouseSensitive;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle * mouseSensitive;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation; 
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition =- (distance - cameraCollisionOffSet);    
        }

        if (Mathf.Abs(targetPosition) < minCollisionOffSet)
        {
            targetPosition = targetPosition - minCollisionOffSet;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.1f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
