using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 30f;
    [SerializeField] float movementMaximumOffsetX = 30f;
    [SerializeField] float movementMaximumOffsetY = 12f;
    [SerializeField] float movementMinimumOffsetY = 4f;


    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Get input
        float xThrow = Input.GetAxis("Horizontal");
        float yThrow = Input.GetAxis("Vertical");
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
        transform.localPosition = new Vector3
               (newXPosition,
               newYPosition,
               transform.localPosition.z);
    }

}
