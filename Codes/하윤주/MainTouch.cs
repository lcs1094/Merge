using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTouch : MonoBehaviour
{
    public AudioClip mainTouch;
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
    
    public void mainTouchSound()
    {
        audio.Stop();
        audio.clip = mainTouch;
        audio.loop = false;
        audio.Play();
    }
}
