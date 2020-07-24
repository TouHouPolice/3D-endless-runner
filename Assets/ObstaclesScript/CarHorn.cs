using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHorn : MonoBehaviour {
    public GameObject playerCheck;
    private AudioSource mAudio;
    // Use this for initialization
    void Start () {
        mAudio = GetComponent<AudioSource>();
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Physics.Linecast(transform.position, playerCheck.transform.position, 1 << 10))
        {
            while (!mAudio.isPlaying)
            {
                mAudio.Play();
            }
        }
	}
}
