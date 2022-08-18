using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input.")]
    [SerializeField] float moveSpeed = 20f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] lasers;
    public bool isAlive = true;

    //Horizontal and Vertical Throw.
    float horizontalThrow;
    float verticalThrow;

    // For Pitch
    float pitch = 0f;
    [Header("Pitch Variables")] 
    [SerializeField] float pitchAcceleration = 0f;
    [SerializeField] float pitchAccelerationFactor = 200f;
    [SerializeField] float minPitchRotation = -30f;
    [SerializeField] float maxPitchRotation = 30f;
    bool isUp_Pitch =  true;

    // For Yaw
    float yaw = 0f;
    [Header("Yaw Variables")]
    [SerializeField] float yawAcceleration = 0f;
    [SerializeField] float yawAccelerationFactor = 20f;
    [SerializeField] float minYawRotation = -5f;
    [SerializeField] float maxYawRotation = 5f;
    bool isRight_Yaw =  true;

    // For Roll
    float roll = 0f;
    [Header("Roll Variables")]
    [SerializeField] float rollAcceleration = 0f;
    [SerializeField] float rollAccelerationFactor = 200f;
    [SerializeField] float minRollRotation = -20f;
    [SerializeField] float maxRollRotation = 20f;
    bool isRight_Roll =  true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // If player dies.
        if(!isAlive) 
        { 
            StopFiringLaser();    
            return; 
        }

        PlayerMovement();
        PlayerRotation();
        FiringLaser();
    }

    private void OnEnable() 
    {
        movement.Enable();   
        fire.Enable();
    }

    private void OnDisable() 
    {
        movement.Disable();   
        fire.Disable();
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
                                              Mathf.Clamp(transform.localPosition.y, -yRange, yRange + 2f), 0f);
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
            if(!isUp_Pitch)
                pitchAcceleration = 0;
            pitchAcceleration +=  pitchAccelerationFactor  * Time.deltaTime * -verticalThrow;
            pitch = Mathf.Clamp(pitchAcceleration, minPitchRotation, maxPitchRotation);
            isUp_Pitch = true;
        }
        // Going Down
        else if(verticalThrow == -1)
        {
            // Checking previous position of the ship.
            if(isUp_Pitch)
                pitchAcceleration = 0;
            pitchAcceleration +=  pitchAccelerationFactor  * Time.deltaTime * -verticalThrow;
            pitch = Mathf.Clamp(pitchAcceleration, minPitchRotation, maxPitchRotation);
            isUp_Pitch = false;
        }
        // Steady
        else
        {
            pitchAcceleration = 0;
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
            yawAcceleration = 0;
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
            rollAcceleration = 0;
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

    private void FiringLaser()
    {
        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = (fire.ReadValue<float>() > 0.5);; // If fire key pressed.
        }   
    }

    // Check Later.
    private void StopFiringLaser()
    {
        foreach(GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = false; // If fire key pressed.
        }
    }
    
}
