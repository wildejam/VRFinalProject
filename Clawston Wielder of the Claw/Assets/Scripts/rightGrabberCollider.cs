using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightGrabberCollider : MonoBehaviour
{

    [SerializeField]
    private GameObject xrOrigin;

    public List<GameObject> grabbedObjects;
    //called when something enters the trigger

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Toy" || other.gameObject.tag == "GoldToy")
        {
            //other.gameObject.transform.parent = xrOrigin.transform;
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObjects.Add(other.gameObject);
        }
      
    }
}
