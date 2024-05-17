using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public AudioSource HitSound;
    public AudioSource visilingSound;
    public AudioSource Ballplaced;

    public AudioClip[] malegoalSound;
    public static AudioManger Instance;

    private void Awake()
    {
        Instance=this;
    }

    public void BallPlacedSoundPlay()
    {
        int randomv=Random.Range(0,malegoalSound.Length);

        Ballplaced.PlayOneShot(malegoalSound[randomv]);
    }
    public void VisilingSoundPlay()
    {
        visilingSound.Play();
    }

    public void HitSoundPlay()
    {
       HitSound.Play();
    }

}
