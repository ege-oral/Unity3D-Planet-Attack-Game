using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xRange = 2.5f;
    [SerializeField] float yRange = 3f;

    float horizontalThrow, verticalThrow;
    // For Pitch
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;

    // For Yaw
    [SerializeField] float controlYawFactor = -5f;
    // For Roll
    [SerializeField] float controlRollFctor = -20f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerRotation();
    }

    private void PlayerMovement()
    {
        // Trying new input system.
        horizontalThrow = movement.ReadValue<Vector2>().x;
        verticalThrow = movement.ReadValue<Vector2>().y;

        float horizontalSpeed = horizontalThrow * moveSpeed * Time.deltaTime;
        float verticalSpeed = verticalThrow * moveSpeed * Time.deltaTime;

        transform.localPosition += new Vector3(horizontalSpeed, verticalSpeed, 0f);
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -xRange, xRange), 
                                              Mathf.Clamp(transform.localPosition.y, -yRange, yRange + 1f), 0f);
    }

    private void PlayerRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + verticalThrow * controlPitchFactor;
        float yaw = horizontalThrow * controlYawFactor;
        float roll = horizontalThrow * controlRollFctor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void OnEnable() 
    {
        movement.Enable();   
    }

    private void OnDisable() 
    {
        movement.Disable();   
    }
}
