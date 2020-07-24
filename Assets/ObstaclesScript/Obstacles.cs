using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour {
    

    /* public MeshRenderer meshRenderer;
     public BoxCollider boxCollider;*/
    // Use this for initialization
    void Start () {
        /*meshRenderer =gameObject.GetComponent<MeshRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider>();*/
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager GM = GameObject.Find("Game Manager").GetComponent<GameManager>();

            if (gameObject.CompareTag("HeavyObstacle"))
            {
                
                GM.TakingFatalHit();
                
            }
            else if (gameObject.CompareTag("LightObstacle"))
            {
                
                GM.TakingLightHit();
            }

            gameObject.SetActive(false);
           /* meshRenderer.enabled = false;
            boxCollider.enabled = false;*/
            //maybe some visual effect
        }           
        
    }
    public void OnDestroy()
    {
        
    }

    public void SelfDestoy()
    {
        Destroy(this.gameObject);
    }

    
}
