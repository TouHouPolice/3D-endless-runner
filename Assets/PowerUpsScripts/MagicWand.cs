using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWand : PowerUps
{
    private GameManager GM;
    private AudioController audioController;
	// Use this for initialization
	void Start () {
        audioController = GameObject.Find("Audio").GetComponent<AudioController>();
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetWand();
            audioController.powerup.PlayOneShot(audioController.powerup.clip);
            Destroy(gameObject);
        }
    }

    public void GetWand() //These funciton is called when the powers being picked up
    {
        GameStatus.magicWand = true;
        GM.PickingUpWand();
        
    }
}
