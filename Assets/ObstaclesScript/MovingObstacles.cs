using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacles : Obstacles
{
    
    public float maxSpeed;
    public float minSpeed;
    private float speed;
    
    /*private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;*/

    // Use this for initialization
    void Start () {
        /* meshRenderer = gameObject.GetComponent<MeshRenderer>();
         boxCollider = gameObject.GetComponent<BoxCollider>();*/
        speed = Random.Range(minSpeed, maxSpeed);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Moving();
        
		
	}

    private void Update()
    {
        
    }

    private void Moving()
    {
        Vector3 temp;
        temp = transform.position;
        temp.z = temp.z - speed * Time.fixedDeltaTime;
        transform.position= temp;
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
    }*/
}
