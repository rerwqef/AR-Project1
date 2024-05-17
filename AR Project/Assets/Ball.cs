using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public GameManger gameManger;

    private void Start()
    {
        // Start the countdown coroutine when the ball is created
        gameManger=FindAnyObjectByType<GameManger>();
        StartCoroutine(DestroyIfMissed());
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the ball has collided with the hoop's collider
        if (other.CompareTag("Hoop"))     
        {
            StopCoroutine(DestroyIfMissed());
            // Increase the score by one
            gameManger.scoreupdater();
            // Stop the countdown coroutine as the ball has hit the collider
          
        }
    }

    private IEnumerator DestroyIfMissed()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(1f);
        Debug.Log("calling from ballscript onmiss");
        // If the ball hasn't hit the collider, destroy the ball and log a message
        gameManger.OnMissedPannel();
        Destroy(gameObject,2);
    }
}