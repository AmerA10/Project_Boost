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
    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip loadLevelSound;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] float levelLoadDelay = 2f;


    Boolean enabledCollision = true;
    bool isTransitioning = false;

    void Start()
    {
        Debug.Log(SceneManager.sceneCountInBuildSettings);
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if(!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
        //todo only when debug is on
        if(Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        } 
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            //toggle collision

            enabledCollision = !enabledCollision;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning || !enabledCollision)
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
                StartDeathSequence();//kill playerf
                break;
        }   
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        audio.Stop();
        audio.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("LoadThisScene", levelLoadDelay); //make parameter
    }

    private void StartSuccessSequence()
    {
        //Move to next level
        isTransitioning = true;
        audio.Stop();
        audio.PlayOneShot(loadLevelSound);
        successParticle.Play();
        Invoke("LoadNextScene", levelLoadDelay); //make parameter
    }

    private void LoadThisScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex; //gets the current scene index
        int nextScene = currentScene == SceneManager.sceneCountInBuildSettings - 1 ?  0 : currentScene + 1 ;
        SceneManager.LoadScene(nextScene); //allow for more than 2 levels
    }

    private void RespondToThrustInput()
    {

        
        if (Input.GetKey(KeyCode.Space))
        {
            
            ApplyThrust();

        }

        else if(Input.GetKeyUp(KeyCode.Space))
        {
            StopApplyingThrust();
            Debug.Log("stopping particles");
        }

    }
    private void StopApplyingThrust()
    {
        audio.Stop();
        mainEngineParticles.Stop();
    }

    private void ApplyThrust()
    {
     
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        mainEngineParticles.Play();
        if (Input.GetKeyDown(KeyCode.Space))
        {
   
            audio.PlayOneShot(mainEngine);
         
        }
    }

    private void RespondToRotateInput()
    {

        rb.angularVelocity = Vector3.zero; //the angular rotaional, physics based rotation = to zero

        
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * rotationThisFrame);

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-1 * Vector3.forward * rotationThisFrame);
        }

        
    }

}
