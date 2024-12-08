using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Reference to the all-knowing GameManager
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioSource pointSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Toy") {
            gameManager.score += 1;
            Destroy(other.gameObject);
            pointSound.Play();
        }
        else if (other.gameObject.tag == "GoldToy")
        {
            gameManager.score += 5;
            Destroy(other.gameObject);
            pointSound.Play();
        }
    }
}
