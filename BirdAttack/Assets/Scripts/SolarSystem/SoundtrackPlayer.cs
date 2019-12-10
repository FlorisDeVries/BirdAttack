using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackPlayer : MonoBehaviour
{
    AudioSource audio;
    public AudioClip[] clips;
    int clipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        clipIndex = Random.Range(0, clips.Length);
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(audio.isPlaying) return;
        // Set a new track.
        audio.clip = clips[clipIndex++];
        audio.Play();
    }
}
