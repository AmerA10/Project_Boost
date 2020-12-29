using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    AudioSource audio;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource >();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up);
            
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audio.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audio.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
          
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-1 * Vector3.forward);
          
        }
    }
}
