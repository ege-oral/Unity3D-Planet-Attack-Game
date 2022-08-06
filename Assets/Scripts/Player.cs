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

    float horizontalThrow;
    float verticalThrow = 0;
    // For Pitch
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -20f;

    // For Yaw
    [SerializeField] float controlYawFactor = -5f;
    // For Roll
    [SerializeField] float controlRollFctor = -20f;
    float pitch = 0f;
    float yaw = 0f;
    float roll = 0f;

    float acceleration = 1f;
    bool isUp =  true;

    float tmp;

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

    private void OnEnable() 
    {
        movement.Enable();   
    }

    private void OnDisable() 
    {
        movement.Disable();   
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
        PlayerPitchRotation();
        yaw = horizontalThrow * controlYawFactor;
        roll = horizontalThrow * controlRollFctor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void PlayerPitchRotation()
    {
        float min = -30f; // Minimum rotation value.
        float max = 30f; // Maximum rotation value.
        // Going Up
        if(verticalThrow == 1)
        {
            // Checking previous position of the ship.
            if(!isUp)
                acceleration = 0;
            acceleration +=  100f  * Time.deltaTime * -verticalThrow;
            pitch = Mathf.Clamp(acceleration, min, max);
            isUp = true;
        }
        // Going Down
        else if(verticalThrow == -1)
        {
            // Checking previous position of the ship.
            if(isUp)
                acceleration = 0;
            acceleration +=  100f  * Time.deltaTime * -verticalThrow;
            pitch = Mathf.Clamp(acceleration, min, max);
            isUp = false;
        }
        // Steady
        else
        {
            if(pitch > 0)
            {
                pitch -= 50f * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, 0, max);
            }
            else if (pitch < 0)
            {
                pitch += 50f * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, min, 0);
            }
        }
    }

    
}
