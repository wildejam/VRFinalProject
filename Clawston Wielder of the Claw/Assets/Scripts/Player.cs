using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public InputActionReference button;
    public float dropRaiseTime = 2.0f;
    public float dropRaiseDistance = 30.0f;
    public AudioSource dropRaiseSFX;

    // Boundaries of claw machine walls
    [SerializeField]
    private float xLowerBoundary = -45;
    [SerializeField]
    private float xUpperBoundary = 45;
    [SerializeField]
    private float zLowerBoundary = -45;
    [SerializeField]
    private float zUpperBoundary = 45;
    [SerializeField]
    private GameObject leftGrabberObj;
    [SerializeField]
    private GameObject rightGrabberObj;

    // possible states are "raised", "lowered", "raising", and "lowering"
    public string verticalMovementState = "raised";
    // value used to store the ratio between start and end time for vertical movement, for use in lerp
    private float verticalMovementTimer = 0;
    private Vector3 verticalMovementStartPos;
    private Vector3 verticalMovementEndPos;

    void Start()
    {
        button.action.performed += DropRaise;
    }

    void DropRaise(InputAction.CallbackContext __)
    {
        if (verticalMovementState == "raising" || verticalMovementState == "lowering") return;
        else if (verticalMovementState == "raised")
        {
            verticalMovementState = "lowering";
            verticalMovementTimer = 0;
            verticalMovementStartPos = transform.position;
            verticalMovementEndPos = new Vector3(transform.position.x, transform.position.y - dropRaiseDistance, transform.position.z);
            dropRaiseSFX.loop = true;
            dropRaiseSFX.Play();
            return;
        }
        else if (verticalMovementState == "lowered")
        {
            rightGrabberCollider leftGrabberScript = leftGrabberObj.GetComponent<rightGrabberCollider>();
            rightGrabberCollider rightGrabberScript = rightGrabberObj.GetComponent<rightGrabberCollider>();
            for (int i = 0; i < rightGrabberScript.grabbedObjects.Count; i++)
            {
                rightGrabberScript.grabbedObjects[i].gameObject.transform.parent = transform;
                rightGrabberScript.grabbedObjects[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            for (int i = 0; i < leftGrabberScript.grabbedObjects.Count; i++)
            {
                leftGrabberScript.grabbedObjects[i].gameObject.transform.parent = transform;
                leftGrabberScript.grabbedObjects[i].gameObject.GetComponent<Rigidbody>().isKinematic = true;
            }

            verticalMovementState = "raising";
            verticalMovementTimer = 0;
            verticalMovementStartPos = transform.position;
            verticalMovementEndPos = new Vector3(transform.position.x, transform.position.y + dropRaiseDistance, transform.position.z);
            dropRaiseSFX.loop = true;
            dropRaiseSFX.Play();
            return;
        }
        else
        {
            Debug.Log("DropRaise verticalMovementState somehow got set to something outside of the given cases.");
            return;
        }
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

        // Handle raising and lowering movement
        if (verticalMovementState == "raising" || verticalMovementState == "lowering")
        {
            // User lerp to determine the position of the player across the movement time
            if (verticalMovementTimer < dropRaiseTime)
            {
                transform.position = Vector3.Lerp(verticalMovementStartPos, verticalMovementEndPos, verticalMovementTimer / dropRaiseTime);
                verticalMovementTimer += Time.deltaTime;
            }
            else
            {
                // Once complete (Time.time > end time), set the state back to "raised" or "lowered" accordingly
                switch(verticalMovementState)
                {
                    case "raising":
                        rightGrabberCollider leftGrabberScript = leftGrabberObj.GetComponent<rightGrabberCollider>();
                        rightGrabberCollider rightGrabberScript = rightGrabberObj.GetComponent<rightGrabberCollider>();
                        for (int i = 0; i < rightGrabberScript.grabbedObjects.Count; i++)
                        {
                            rightGrabberScript.grabbedObjects[i].gameObject.transform.parent = transform.root;
                            rightGrabberScript.grabbedObjects[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        }
                        for (int i = 0; i < leftGrabberScript.grabbedObjects.Count; i++)
                        {
                            leftGrabberScript.grabbedObjects[i].gameObject.transform.parent = transform.root;
                            leftGrabberScript.grabbedObjects[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                        }
                        rightGrabberScript.grabbedObjects.Clear();
                        leftGrabberScript.grabbedObjects.Clear();


                        verticalMovementState = "raised";
                        verticalMovementTimer = 0;
                        dropRaiseSFX.loop = false;
                        break;
                    case "lowering":
                        verticalMovementState = "lowered";
                        verticalMovementTimer = 0;
                        dropRaiseSFX.loop = false;
                        break;
                }
            }
        }   
    }
}
