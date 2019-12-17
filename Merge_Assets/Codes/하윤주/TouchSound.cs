using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchSound : MonoBehaviour
{
    public AudioClip galleryTouch;
    public AudioClip goMain;
    public AudioSource audio;
    // Start is called before the first frame update

    void Start()
    {
        audio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void galleryTouchSound()
    {
        audio.Stop();
        audio.clip = galleryTouch;
        audio.loop = false;
        audio.Play();
    }

    public void goMainSound()
    {
        audio.Stop();
        audio.clip = goMain;
        audio.loop = false;
        audio.Play();
    }
}
