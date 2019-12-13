using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackPlayer : MonoBehaviour
{
    AudioSource source;
    public AudioClip[] clips;
    int clipIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        clipIndex = Random.Range(0, clips.Length);
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(source.isPlaying) return;
        // Set a new track.
        clipIndex++;
        if(clipIndex >= clips.Length)
            clipIndex = 0;
        source.clip = clips[clipIndex];
        source.Play();
    }
}
