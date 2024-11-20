using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    // Boundaries of claw machine walls
    [SerializeField]
    private float xLowerBoundary = -45;
    [SerializeField]
    private float xUpperBoundary = 45;
    [SerializeField]
    private float zLowerBoundary = -45;
    [SerializeField]
    private float zUpperBoundary = 45;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Restrict movement to within the claw machine
        if(transform.position.x < xLowerBoundary)
        {
            transform.position = new Vector3(xLowerBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xUpperBoundary)
        {
            transform.position = new Vector3(xUpperBoundary, transform.position.y, transform.position.z);
        }
        if (transform.position.z < zLowerBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLowerBoundary);
        }
        if (transform.position.z > zUpperBoundary)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zUpperBoundary);
        }


    }
}
