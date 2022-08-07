using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float m_Speed;
    float left;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        m_Speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            left += transform.localRotation.y + 2f;
            transform.localRotation = Quaternion.Euler(transform.localRotation.x, left, transform.localRotation.z);
        }
        else
        {
            m_Rigidbody.velocity = new Vector3(0f,0f,0f);
        }
    }
}
