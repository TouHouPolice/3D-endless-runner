using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    private GameObject boss;
    public float thrownSpeed;
    public bool pickedByPlayer = false;
    public GameManager GM;
    private Rigidbody rb;
    public float upForce = 100f;
    public GameObject explostion;

	// Use this for initialization
	void Start () {


        boss = GameObject.Find("BossCentre");
        GM = GameObject.Find("Game Manager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody>();
            
        
	}
	
	// Update is called once per frame
	void Update () {
        if (pickedByPlayer == true)
        {
            
            
            transform.position = Vector3.MoveTowards(transform.position, boss.transform.position, thrownSpeed * Time.deltaTime);
        }

        if (!GameManager.bossIsAlive)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Boss") && pickedByPlayer == true)
        {
           // AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, transform.position);
            Instantiate(explostion, transform.position, Quaternion.identity);
            print("hitBOss");
            GameManager.bossCurrentHealth -= 1;
            //some visual effect



            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Player"))
        {
            pickedByPlayer = true;
            gameObject.tag = "Grenade";
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(Vector3.up * upForce);
        }

        
    }
}
