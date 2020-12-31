using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //only allow one of them in a gameobject
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 movementVector = new Vector3(10f,10f,10f);
    [SerializeField] float period = 2f;

    // todo remove from inspector later
    [Range(0,1)] [SerializeField] float movementFactor; //0 not moved, 1 for fully moved
    Vector3 startingPos; //must be stored for absolute movement
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //set movement factor

        if(period <= Mathf.Epsilon)
        {
            return;
        }

        float cycles = Time.time / period; // grows continually from 0, frame rate independent 

        const float tau = Mathf.PI * 2f; // about 6.28...
        float rawSinWive = Mathf.Sin(cycles * tau); //continually producing sin wave values

        movementFactor = rawSinWive / 2f + 0.5f; //make sure that our movement factor stays between 0 and 1
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
     
    }
}
