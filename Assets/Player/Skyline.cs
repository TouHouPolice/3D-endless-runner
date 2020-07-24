using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyline : MonoBehaviour {

    [SerializeField]
    Transform skylinePrefab;

    [SerializeField]
    Transform sideWalkPrefab;

    [SerializeField]
    Transform[] skylinePrefabs;

    [SerializeField]
    float[] skylinePrefabsLength;

    [SerializeField]
    float[] skylinePrefabsHeight;

    int numberOfObjects = 30;

    Vector3 leftSideWalkPos = new Vector3(-10.5f, 0.55f, 0);
    Vector3 rightSideWalkPos = new Vector3(10.5f, 0.55f, 0);
    Vector3 leftNextSideWalkPos;
    Vector3 rightNextSideWalkPos;

    Vector3 leftStartPos = new Vector3(-11, 0, 0);
    Vector3 rightStartPos = new Vector3(11, 0, 0);
    Vector3 leftNextPos;
    Vector3 rightNextPos;

    Queue<Transform> skylineLeftQueue;
    Queue<Transform> skylineRightQueue;

    Queue<Transform> sideWalkLeftQueue;
    Queue<Transform> sideWalkRightQueue;

    float minSizeY = 5f;
    float maxSizeY = 20f;

    float minSizeZ = 5f;
    float maxSizeZ = 10f;

	// Use this for initialization
	void Start () {
        skylineLeftQueue = new Queue<Transform>(numberOfObjects);
        skylineRightQueue = new Queue<Transform>(numberOfObjects);

        sideWalkLeftQueue = new Queue<Transform>(12);
        sideWalkRightQueue = new Queue<Transform>(numberOfObjects);
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            skylineLeftQueue.Enqueue((Transform)Instantiate(skylinePrefab));
            skylineRightQueue.Enqueue((Transform)Instantiate(skylinePrefab));
        }

        for(int i = 0; i< 12; i++)
        {
            sideWalkLeftQueue.Enqueue((Transform)Instantiate(sideWalkPrefab));
            sideWalkRightQueue.Enqueue((Transform)Instantiate(sideWalkPrefab));
        }

        leftNextPos = leftStartPos;
        rightNextPos = rightStartPos;

        leftNextSideWalkPos = leftSideWalkPos;
        rightNextSideWalkPos = rightSideWalkPos;

        for (int i = 0; i < numberOfObjects; i++)
        {
            Recycle();
        }

        for(int i = 0; i< 12; i++)
        {
            RecycleSideWalk();
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(skylineLeftQueue.Peek().localPosition.z + 25f < GameStatus.distanceTravelled)
        {
            Recycle();
        }

        if(sideWalkLeftQueue.Peek().localPosition.z + 25f < GameStatus.distanceTravelled)
        {
            RecycleSideWalk();
        }
	}

    void Recycle()
    {
        //Vector3 leftScale = new Vector3(5f, Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
        //Vector3 rightScale = new Vector3(5f, Random.Range(minSizeY, maxSizeY), Random.Range(minSizeZ, maxSizeZ));
        Vector3 leftPosition = leftNextPos;
        Vector3 rightPosition = rightNextPos;
        //leftPosition.y += leftScale.y * 0.5f;
        //rightPosition.y += rightScale.y * 0.5f;

        Transform leftSkyline = skylineLeftQueue.Dequeue();
        Transform rightSkyline = skylineRightQueue.Dequeue();

        Destroy(leftSkyline.gameObject);
        Destroy(rightSkyline.gameObject);

        int leftrand = Random.Range(0, skylinePrefabs.Length);
        int rightrand = Random.Range(0, skylinePrefabs.Length);

        leftPosition.z += skylinePrefabsLength[leftrand];
        rightPosition.z += skylinePrefabsLength[rightrand];

        //leftSkyline = (Transform)Instantiate(skylinePrefabs[leftrand], new Vector3(leftPosition.x, skylinePrefabsHeight[leftrand] + 0.6042783f, leftPosition.z), Quaternion.identity);
        //rightSkyline = (Transform)Instantiate(skylinePrefabs[rightrand], new Vector3(rightPosition.x, skylinePrefabsHeight[rightrand] + 0.5590504f, rightPosition.z), Quaternion.identity);

        leftSkyline = (Transform)Instantiate(skylinePrefabs[leftrand], new Vector3(leftPosition.x, skylinePrefabsHeight[leftrand] + 0.55f, leftPosition.z), Quaternion.identity);
        rightSkyline = (Transform)Instantiate(skylinePrefabs[rightrand], new Vector3(rightPosition.x, skylinePrefabsHeight[rightrand] + 0.55f, rightPosition.z), Quaternion.identity);

        leftSkyline.Rotate(0, -90, 0);
        rightSkyline.Rotate(0, 90, 0);

        //leftSkyline.localScale = leftScale;
        //rightSkyline.localScale = rightScale;
        //leftSkyline.localPosition = leftPosition;
        //rightSkyline.localPosition = rightPosition;

        leftNextPos.z += skylinePrefabsLength[leftrand] * 2 + 1f;
        rightNextPos.z += skylinePrefabsLength[rightrand] * 2 + 1f;

        

        //Color leftColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        //Color rightColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        //leftSkyline.gameObject.GetComponent<Renderer>().material.color = leftColor;
        //rightSkyline.gameObject.GetComponent<Renderer>().material.color = rightColor;

        skylineLeftQueue.Enqueue(leftSkyline);
        skylineRightQueue.Enqueue(rightSkyline);
    }

    void RecycleSideWalk()
    {
        Vector3 leftSideWalkPosition = leftNextSideWalkPos;
        Vector3 rightSideWalkPosition = rightNextSideWalkPos;

        leftSideWalkPosition.z += (20.87f / 2);
        rightSideWalkPosition.z += (20.87f / 2);

        Transform leftSideWalk = sideWalkLeftQueue.Dequeue();
        Transform rightSideWalk = sideWalkRightQueue.Dequeue();

        leftSideWalk.position = leftSideWalkPosition;
        rightSideWalk.position = rightSideWalkPosition;

        leftNextSideWalkPos.z += 20.87f;
        rightNextSideWalkPos.z += 20.87f;

        sideWalkLeftQueue.Enqueue(leftSideWalk);
        sideWalkRightQueue.Enqueue(rightSideWalk);
    }
}
