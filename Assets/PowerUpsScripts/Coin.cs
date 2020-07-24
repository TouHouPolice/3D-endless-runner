using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUps
{
    private AudioController audioController;
    private GameObject Player;
    public float attractDistance;
    public float attractSpeed;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("Player");
        audioController = GameObject.Find("Audio").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        MagnetAttraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioController.coin.PlayOneShot(audioController.coin.clip);
            GameStatus.coinCollected += 1;
            
            Destroy(gameObject);
        }
    }

    void MagnetAttraction()
    {
        float distance = Vector3.Distance(gameObject.transform.position, Player.transform.position);

        if (GameStatus.magnet == true && distance < attractDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, attractSpeed * Time.deltaTime);
        }
    }
}
