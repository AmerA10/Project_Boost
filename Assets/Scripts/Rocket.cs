using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private AudioSource audio;
    [SerializeField]float rcsThrust = 200f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip loadLevelSound;


    enum State
    {
        Alive, Dying, Transcending
    }
    State state = State.Alive;

    private void Awake()
    {
        
      //  state = State.Alive;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
     
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive)
        {
            return; //dont execute if alive
        }
        switch (collision.transform.tag)
        {
            
            case ("Friendly"):
                {
                //why is this here even
                break;
                }
            case ("Finish"):
                StartSuccessSequence(); //move to next level 
                break;
            default:
                StartDeathSequence();//kill player

                break;
        }   
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audio.Stop();
        audio.PlayOneShot(deathSound);
        Invoke("LoadThisScene", 1f); //make parameter
    }

    private void StartSuccessSequence()
    {
        //Move to next level
        state = State.Transcending;
        audio.Stop();
        audio.PlayOneShot(loadLevelSound);
        Invoke("LoadNextScene", 1f); //make parameter
    }

    private void LoadThisScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); //allow for more than 2 levels
    }

    private void RespondToThrustInput()
    {
        ApplyThrust();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audio.Stop();
        }
    }

    private void ApplyThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust);

        }
        if (Input.GetKeyDown(KeyCode.Space) && state == State.Alive)
        {
            audio.PlayOneShot(mainEngine);
        }
    }

    private void RespondToRotateInput()
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
