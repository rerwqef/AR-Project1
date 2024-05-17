using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceManger : MonoBehaviour
{
    private Placeindicator placeIndicator;
    public GameObject objectToPlace;
    private GameObject newPlacedObject;

   public bool isSpwaned=false;
    void Start()
    {
        placeIndicator=FindAnyObjectByType<Placeindicator>();
    }

    public void ClickToPlace()
    {
        newPlacedObject = Instantiate(objectToPlace, placeIndicator.transform.position, placeIndicator.transform.rotation);
        Vector3 direction = Camera.main.transform.position - newPlacedObject.transform.position;
        direction.y = 0; // Zero out the y component to keep the hoop's vertical orientation

        // Calculate the rotation needed to face the direction
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Apply the rotation to the hoop, only affecting the y-axis
        newPlacedObject.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        isSpwaned =true;
    }
  
}
