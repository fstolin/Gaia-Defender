using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float movementMaximumOffsetX = 20f;
    [SerializeField] float movementMaximumOffsetY = 14f;
    [SerializeField] float movementMinimumOffsetY = -14f;
    // Rotation
    [SerializeField] float pitchFactor = -1.5f;
    [SerializeField] float pitchUserFactor = -15f;
    [SerializeField] float yawFactor = 2.5f;
    [SerializeField] float rollFactor = -40f;
    [SerializeField] float rotationFactor = 3f;

    float xThrow, yThrow;


    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleRotation()
    {
        // Assign controlled / position variables
        float controllAffectedPitch = (yThrow * pitchUserFactor);
        float positionAffectedPitch = (transform.localPosition.y * pitchFactor);
        float positionAffectedRoll = (xThrow * rollFactor);

        // Actuall axis assignement
        float pitch = positionAffectedPitch + controllAffectedPitch;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = positionAffectedRoll;

        // Target location -> to not be choppy
        Quaternion targetLocation = Quaternion.Euler(pitch, yaw, roll);
        // Rotate towards angle -> finishing
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetLocation, rotationFactor);
    }

    private void HandleMovement()
    {
        // Get input
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");
        // Calculate the offset from the original position this frame
        float xOffset = (movementSpeed * xThrow * Time.deltaTime);
        float yOffset = (movementSpeed * yThrow * Time.deltaTime);
        // Calculate the new position this frame
        float newXPosition = transform.localPosition.x + xOffset;
        float newYPosition = transform.localPosition.y + yOffset;
        // Limit the movement
        newXPosition = Mathf.Clamp(newXPosition, -movementMaximumOffsetX, movementMaximumOffsetX);
        newYPosition = Mathf.Clamp(newYPosition, movementMinimumOffsetY, movementMaximumOffsetY);
        // Local position update
        transform.localPosition = new Vector3(newXPosition, newYPosition, transform.localPosition.z);
    }

}
