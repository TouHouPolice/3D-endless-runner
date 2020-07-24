using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public float distanceToPlayer;
    private GameObject player;

    

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        KeepDistance();
        
        
	}

    void KeepDistance()
    {
        Vector3 temp = transform.position;
        temp.z = player.transform.position.z + distanceToPlayer;
        transform.position = temp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grenade"))
        {
            GetComponent<AudioSource>().Play();
        }
    }


}
