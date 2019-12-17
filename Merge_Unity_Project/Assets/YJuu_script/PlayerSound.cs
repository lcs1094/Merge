using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip walking;
    public AudioClip change;
    public AudioClip Skill01;
    public AudioClip Skill02;
    public AudioClip ladder;
    public AudioClip bread;

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

    public void audioStop() { audio.Stop(); }

    public void breadSound()
    {
        audio.Stop();
        audio.clip = bread;
        audio.loop = false;
        audio.Play();
    }

    public void ladderSound()
    {
        if (audio.clip != ladder)
        {
            if (audio.clip != bread&&audio.clip != change)
            {
                audio.Stop();
                audio.clip = ladder;
                audio.loop = true;
                audio.Play();
            }
            else
            {
                if (!audio.isPlaying)
                {
                    audio.Stop();
                    audio.clip = ladder;
                    audio.loop = true;
                    audio.Play();
                }
            }
        }
    }

    public void walkSound()
    {
        if (audio.clip != walking)
        {
            if (audio.clip != bread && audio.clip != change)
            {
                audio.Stop();
                audio.clip = walking;
                audio.loop = true;
                audio.Play();
            }
            else
            {
                if (!audio.isPlaying)
                {
                    audio.Stop();
                    audio.clip = walking;
                    audio.loop = true;
                    audio.Play();
                }
            }
        }
        else if (!audio.isPlaying) { audio.Play(); }
    }

    public void changeSound()
    {
        audio.Stop();
        audio.clip = change;
        audio.loop = false;
        audio.Play();
    }

    public void skillSound(int num)
    {
        audio.Stop();
        if (num == 1) { audio.clip = Skill01; }
        else if (num == 2) { audio.clip = Skill02; }
        audio.loop = false;
        audio.Play();
    }
}
