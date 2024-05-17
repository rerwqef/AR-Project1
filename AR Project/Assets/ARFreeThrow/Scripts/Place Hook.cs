    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;


    [RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
    public class PlaceHook : MonoBehaviour
{
    [SerializeField] private GameObject HoopPrefab;
    private ARRaycastManager arRaycastManager;
    private ARPlaneManager arPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool s = false;
    public bool placed = false;
    private Timer timer;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arPlaneManager = GetComponent<ARPlaneManager>();
        timer=FindAnyObjectByType<Timer>();
    }

    private void Update()
    {
        // Detect mouse click (left button)
        if (placed) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (s && !placed)
            {
                // Raycast from mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose pose = hits[0].pose;
                    GameObject hoop = Instantiate(HoopPrefab, pose.position, pose.rotation);

                    // Calculate the direction vector from the hoop to the camera
                    Vector3 direction = Camera.main.transform.position - hoop.transform.position;
                    direction.y = 0; // Zero out the y component to keep the hoop's vertical orientation

                    // Calculate the rotation needed to face the direction
                    Quaternion rotation = Quaternion.LookRotation(direction);

                    // Apply the rotation to the hoop, only affecting the y-axis
                    hoop.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);

                    placed = true;
                 

                }
            }
        }
    }

    public void Sseteer()
    {
        s = !s;
    }
}
