using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vest : PowerUps
{
    public float vestDuration = 20f;
    private AudioController audioController;
    // Use this for initialization
    void Start () {
        audioController = GameObject.Find("Audio").GetComponent<AudioController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetVest();
            audioController.powerup.PlayOneShot(audioController.powerup.clip);
            Destroy(gameObject);
        }
    }

    public void GetVest() //These funciton is called when the powers being picked up
    {
        GameStatus.vest = true;
        GameStatus.vestRemaining = vestDuration;
    }
}
