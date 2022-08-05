using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float xRange = 4f;
    [SerializeField] float yRange = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Trying new input system.
        float horizontalThrow = movement.ReadValue<Vector2>().x;
        float verticalThrow = movement.ReadValue<Vector2>().y;

        float horizontalSpeed = horizontalThrow * moveSpeed * Time.deltaTime;
        float verticalSpeed = verticalThrow * moveSpeed * Time.deltaTime;

        transform.localPosition += new Vector3(horizontalSpeed, verticalSpeed, 0f);
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x, -xRange, xRange), Mathf.Clamp(transform.localPosition.y, -yRange, yRange), 0f);

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
