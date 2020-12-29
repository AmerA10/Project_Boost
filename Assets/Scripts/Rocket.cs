using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private AudioSource audio;
    [SerializeField]float rcsThrust = 200f;
    [SerializeField] float mainThrust = 100f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
       
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audio.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audio.Stop();
        }
    }

    private void Rotate()
    {

        rb.freezeRotation = true; //takes manual control of rotation. Free physics rotation

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {

            
            transform.Rotate(Vector3.forward * rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-1 * Vector3.forward * rotationThisFrame);

        }

        rb.freezeRotation = false; //resumee rotation physics
    }

    
}
