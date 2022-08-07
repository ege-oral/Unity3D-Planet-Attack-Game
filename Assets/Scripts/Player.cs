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
    float verticalThrow;

    // For Pitch
    float pitch = 0f; 
    float pitchAcceleration = 0f;
    //[SerializeField] 
    float pitchAccelerationFactor = 100f;
    //[SerializeField] 
    float minPitchRotation = -30f;
    //[SerializeField] 
    float maxPitchRotation = 30f;
    bool isUp =  true;

    // For Yaw
    float yaw = 0f;
    float yawAcceleration = 0f;
    //[SerializeField] 
    float yawAccelerationFactor = 20f;
    //[SerializeField] 
    float minYawRotation = -5f;
    //[SerializeField] 
    float maxYawRotation = 5f;
    bool isRight_Yaw =  true;

    // For Roll
    float roll = 0f;
    float rollAcceleration = 0f;
    //[SerializeField] 
    float rollAccelerationFactor = 100f;
    //[SerializeField] 
    float minRollRotation = -20f;
    //[SerializeField] 
    float maxRollRotation = 20f;
    bool isRight_Roll =  true;



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
        // Preventing if pitch, yaw or roll wants to rotate same time.
        if(horizontalThrow > 0 && verticalThrow > 0 || 
           horizontalThrow < 0 && verticalThrow < 0 ||
           horizontalThrow > 0 && verticalThrow < 0 ||
           horizontalThrow < 0 && verticalThrow > 0) { return; }

        
        PlayerPitchRotation();
        PlayerYawRotation();
        PlayerRollRotation();

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void PlayerPitchRotation()
    {
        // Going Up
        if(verticalThrow == 1)
        {
            // Checking previous position of the ship.
            if(!isUp)
                pitchAcceleration = 0;
            pitchAcceleration +=  pitchAccelerationFactor  * Time.deltaTime * -verticalThrow;
            pitch = Mathf.Clamp(pitchAcceleration, minPitchRotation, maxPitchRotation);
            isUp = true;
        }
        // Going Down
        else if(verticalThrow == -1)
        {
            // Checking previous position of the ship.
            if(isUp)
                pitchAcceleration = 0;
            pitchAcceleration +=  pitchAccelerationFactor  * Time.deltaTime * -verticalThrow;
            pitch = Mathf.Clamp(pitchAcceleration, minPitchRotation, maxPitchRotation);
            isUp = false;
        }
        // Steady
        else
        {
            if(pitch > 0)
            {
                pitch -= pitchAccelerationFactor * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, 0, maxPitchRotation);
            }
            else if (pitch < 0)
            {
                pitch += pitchAccelerationFactor * Time.deltaTime;
                pitch = Mathf.Clamp(pitch, minPitchRotation, 0);
            }
        }
    }

    private void PlayerYawRotation()
    {
        // Going Right
        if(horizontalThrow == 1)
        {
            // Checking previous position of the ship.
            if(!isRight_Yaw)
                yawAcceleration = 0;
            yawAcceleration +=  yawAccelerationFactor * Time.deltaTime * -horizontalThrow;
            yaw = Mathf.Clamp(yawAcceleration, minYawRotation, maxYawRotation);
            isRight_Yaw = true;
        }
        // Going Left
        else if(horizontalThrow == -1)
        {
            // Checking previous position of the ship.
            if(isRight_Yaw)
                yawAcceleration = 0;
            yawAcceleration +=  yawAccelerationFactor * Time.deltaTime * -horizontalThrow;
            yaw = Mathf.Clamp(yawAcceleration, minYawRotation, maxYawRotation);
            isRight_Yaw = false;
        }
        // Steady
        else
        {
            if(yaw > 0)
            {
                yaw -= yawAccelerationFactor * Time.deltaTime;
                yaw = Mathf.Clamp(yaw, 0, maxYawRotation);
            }
            else if (yaw < 0)
            {
                yaw += yawAccelerationFactor * Time.deltaTime;
                yaw = Mathf.Clamp(yaw, minYawRotation, 0);
            }
        }
    }

    private void PlayerRollRotation()
    {
        // Going Right
        if(horizontalThrow == 1)
        {
            // Checking previous position of the ship.
            if(!isRight_Roll)
                rollAcceleration = 0;
            rollAcceleration +=  rollAccelerationFactor * Time.deltaTime * -horizontalThrow;
            roll = Mathf.Clamp(rollAcceleration, minRollRotation, maxRollRotation);
            isRight_Roll = true;
        }
        // Going Left
        else if(horizontalThrow == -1)
        {
            // Checking previous position of the ship.
            if(isRight_Roll)
                rollAcceleration = 0;
            rollAcceleration +=  rollAccelerationFactor * Time.deltaTime * -horizontalThrow;
            roll = Mathf.Clamp(rollAcceleration, minRollRotation, maxRollRotation);
            isRight_Roll = false;
        }
        // Steady
        else
        {
            if(roll > 0)
            {
                roll -= rollAccelerationFactor * Time.deltaTime;
                roll = Mathf.Clamp(roll, 0, maxRollRotation);
            }
            else if (roll < 0)
            {
                roll += rollAccelerationFactor * Time.deltaTime;
                roll = Mathf.Clamp(roll, minRollRotation, 0);
            }
        }
    }
    
}
