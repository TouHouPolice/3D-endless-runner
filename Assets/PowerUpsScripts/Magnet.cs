using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : PowerUps
{
    private AudioController audioController;
    public float magnetDuration =10f;
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
            GetMagnet();
            audioController.powerup.PlayOneShot(audioController.powerup.clip);
            Destroy(gameObject);
        }
    }

    public void GetMagnet() //These funciton is called when the powers being picked up
    {
        GameStatus.magnet = true;
        GameStatus.magnetRemaining = magnetDuration;
    }
}
