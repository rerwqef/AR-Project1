using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BallThrow : MonoBehaviour
{
    public GameObject ballPrefab; // Assign your ball prefab in the inspector
    public float throwForce = 600f;
    private Camera arCamera;
    private GameObject ballInstance;
    private GameManger manager; // Corrected the typo in the variable name
    public Button btn;
    public bool m = false;

    void Start()
    {
        manager = FindObjectOfType<GameManger>(); // Corrected the typo in the class name
        arCamera = Camera.main; // Ensure the main camera is tagged as "MainCamera"
         // Create the initial ball at the start
    }
    public void FirCreateBalll()
    { 
        if(m)return;
        m = true;
        CreateBall();
        AudioManger.Instance.VisilingSoundPlay();
    }
    public void ThorwBall()
    {
        if (ballInstance != null )
        {
            ThrowBall();
        }
    }


    IEnumerator CreateBallAfterDelay()
    {
        btn.interactable = false;
        yield return new WaitForSeconds(1);
        if (ballInstance != null)
        {
        }


        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        AudioManger.Instance.VisilingSoundPlay();
        CreateBall();
    }

    void CreateBall()
    {
        if (ballInstance != null)
        {
            Destroy(ballInstance);
        }

        // Define an offset position relative to the camera's forward direction
        Vector3 offset = new Vector3(0f, -0.2f, 0f); // Adjust the x, y, z values as needed for the desired offset

        // Calculate the spawn position with the offset
        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 0.5f + arCamera.transform.right * offset.x + arCamera.transform.up * offset.y + arCamera.transform.forward * offset.z;

        ballInstance = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);

        btn.interactable = true;
        ballInstance.transform.SetParent(arCamera.transform);
        ballInstance.SetActive(false);// Set the ball as a child of the camera
        Rigidbody ballRigidbody = ballInstance.GetComponent<Rigidbody>();
        if (ballRigidbody != null)
        {
            ballRigidbody.isKinematic = true; // Disables the effects of gravity
        }
    }

    void ThrowBall()
    {
     /*   if (!m) return;*/
        Rigidbody rb = ballInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            ballInstance.transform.SetParent(null); 
                ballInstance.SetActive(true);// Remove the ball from the camera's hierarchy
            rb.isKinematic = false; // Enables the effects of gravity
            rb.AddForce(arCamera.transform.forward* throwForce);
            manager.ShootBallChanceUpdater();
            ballInstance = null; // Set to null so a new ball can be created after the delay

            if (manager.canCreateBall)
            {
                StartCoroutine(CreateBallAfterDelay());
            }
            else if (!manager.canCreateBall)
            {
                Debug.Log("Kali thottu..."); // Changed from print to Debug.Log for mobile debugging
            }
            if (manager.chances <= 0)
            {
                Invoke("ONloseCalller",3);
            }
        }
    }
   public void ONloseCalller()
    {
        print("Game poyi");
        manager.ONlose();
    }
}