using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    AudioSource source;

    [SerializeField]
    AudioClip clip;
	// Use this for initialization
	void Start () {
        source.PlayOneShot(clip,.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(!source.isPlaying)
        {
            source.PlayOneShot(clip, .5f);
        }
	}
}
