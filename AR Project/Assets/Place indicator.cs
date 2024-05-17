using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Placeindicator : MonoBehaviour
{


    private ARRaycastManager aRaycastManager;
    private GameObject indicator;
    private List<ARRaycastHit> hits=new List<ARRaycastHit>();
    private PlaceManger placeManger;
    public GameObject hoopPlacingBtn;
    public GameObject ballThrowbtn;
    public BallThrow ballThrow;
    Timer timer;
    private void Start()
    {
        aRaycastManager=FindAnyObjectByType<ARRaycastManager>();
        placeManger = FindAnyObjectByType<PlaceManger>();
        indicator=transform.GetChild(0).gameObject;
       hoopPlacingBtn = GameObject.Find("HoopBtn");
        ballThrowbtn = GameObject.Find("ThrowBall");
        ballThrow = FindAnyObjectByType<BallThrow>();
        timer = FindAnyObjectByType<Timer>();
        ballThrowbtn.SetActive(false);
        indicator.SetActive(false);
    }
    private void Update()
    {
        if (placeManger.isSpwaned)
        {
            indicator.SetActive(false);
           hoopPlacingBtn.SetActive(false);
            ballThrowbtn.SetActive(true);
           ballThrow.FirCreateBalll();
            timer.stopPlayinAnim();
            print("ini spwan aavilla");
            
            return;
        }
        var ray=new Vector2(Screen.width/2, Screen.height/2);

        if (aRaycastManager.Raycast(ray, hits, TrackableType.Planes))
        {
            Pose pose = hits[0].pose;
            transform.position = pose.position;

            transform.rotation= pose.rotation;
            if (!indicator.activeInHierarchy)
            {
                indicator.SetActive(true);
            }
        }
    }
}
