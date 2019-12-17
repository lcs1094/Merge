using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTouch : MonoBehaviour
{
    public AudioClip touch;
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

    public void touchSound()
    {
        audio.Stop();
        audio.clip = touch;
        audio.loop = false;
        audio.Play();
    }
}
