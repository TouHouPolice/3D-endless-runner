using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    //GameObject[] leftPlatforms;
    //GameObject[] platforms;
    //GameObject[] rightPlatforms;
    public GameObject destroyParticle;
    [SerializeField]
    Transform leftPlatformPrefab;
    [SerializeField]
    Transform platformPrefab;
    [SerializeField]
    Transform rightPlatformPrefab;

    Queue<Transform> platformQueue;
    Queue<Transform> platformLeftQueue;
    Queue<Transform> platformRightQueue;

    Queue<Transform> obstacleQueueLeft;
    Queue<Transform> obstacleQueue;
    Queue<Transform> obstacleQueueRight;

    Vector3 startLeftPos = new Vector3(-5, 0, 0);
    Vector3 startPos = new Vector3(0, 0, 0);
    Vector3 startRightPos = new Vector3(5, 0, 0);

    Vector3 nextLeftPos;
    Vector3 nextPos;
    Vector3 nextRightPos;

    [SerializeField]
    Transform[] obstacles;
    [SerializeField]
    float[] obstaclesHeight;
    [SerializeField]
    Transform[] powerups;
    [SerializeField]
    Transform[] bossFightItems;
    [SerializeField]
    float[] bossFightItemsHeight;

    int noOfPlatforms = 12;
    int noOfObstacles = 12;


    // Use this for initialization
    void Start () {

        platformLeftQueue = new Queue<Transform>(noOfPlatforms);
        platformQueue = new Queue<Transform>(noOfPlatforms);
        platformRightQueue = new Queue<Transform>(noOfPlatforms);

        obstacleQueueLeft = new Queue<Transform>(noOfObstacles);
        obstacleQueue = new Queue<Transform>(noOfObstacles);
        obstacleQueueRight = new Queue<Transform>(noOfObstacles);


        for (int i = 0; i<noOfPlatforms; i++)
        {
            platformLeftQueue.Enqueue((Transform)Instantiate(leftPlatformPrefab));
            platformQueue.Enqueue((Transform)Instantiate(platformPrefab));
            platformRightQueue.Enqueue((Transform)Instantiate(rightPlatformPrefab));
        }

        for (int i = 0; i<noOfObstacles; i++)
        {
            obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueue.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
        }


        nextLeftPos = startLeftPos;
        nextPos = startPos;
        nextRightPos = startRightPos;


        for (int i = 0; i<noOfPlatforms; i++)
        {
            Recycle();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (platformQueue.Peek().localPosition.z + 25f < GameStatus.distanceTravelled)
        {
            Recycle();
        }
	}

    void Recycle()
    {
        Vector3 leftPos = nextLeftPos;
        Vector3 position = nextPos;
        Vector3 rightPos = nextRightPos;

        leftPos.z += (20.87f / 2);
        position.z += (20.87f / 2);
        rightPos.z += (20.87f / 2);

        Transform leftPlatform = platformLeftQueue.Dequeue();
        Transform platform = platformQueue.Dequeue();
        Transform rightPlatform = platformRightQueue.Dequeue();

        leftPlatform.position = leftPos;
        platform.position = position;
        rightPlatform.position = rightPos;

        nextLeftPos.z += 20.87f;
        nextPos.z += 20.87f;
        nextRightPos.z += 20.87f;

        platformLeftQueue.Enqueue(leftPlatform);
        platformQueue.Enqueue(platform);
        platformRightQueue.Enqueue(rightPlatform);

        Transform leftPlatformStatus;
        Transform platformStatus;
        Transform rightPlatformStatus;

        if(GameStatus.distanceTravelled > 10)
        {
            RecycleObstacle(leftPlatform, platform, rightPlatform, out leftPlatformStatus, out platformStatus, out rightPlatformStatus);
            RecyclePowerups(leftPlatformStatus, platformStatus, rightPlatformStatus);
        }
    }

    void RecycleObstacle(Transform leftPlatform, Transform platform, Transform rightPlatform, out Transform leftPlatformStatus, out Transform platformStatus, out Transform rightPlatformStatus)
    {
        Transform obstacleLeft = obstacleQueueLeft.Dequeue();
        Transform obstacleMiddle = obstacleQueue.Dequeue();
        Transform obstacleRight = obstacleQueueRight.Dequeue();

        Destroy(obstacleLeft.gameObject);
        Destroy(obstacleMiddle.gameObject);
        Destroy(obstacleRight.gameObject);

        int noOfItems = Random.Range(1, 100);

        //50% 1 platform
        //40% 2 platform
        //10% 3 platform


        if (noOfItems <= GameManager.oneObstaclePercent) //if 1 item is spawn
        {
            int platformChosen = Random.Range(1, 3);
            if (platformChosen == 1)
            {
                //if platform chosen to be left platform
                int leftObstacle = Random.Range(1, obstacles.Length);
                if (GameManager.bossIsAlive)
                {
                    leftObstacle = 1;
                }
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);

                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, 0, platform);

                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, 0, rightPlatform);

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
                
            }

            else if (platformChosen == 2)
            {
                //if platform chosen to be middle platform
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, 0, leftPlatform);

                int obstacle = Random.Range(1, obstacles.Length);
                if (GameManager.bossIsAlive)
                {
                    obstacle = 1;
                }
                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);

                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, 0, rightPlatform);

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

            else
            {
                //if platform chosen to be right platform

                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, 0, leftPlatform);

                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, 0, platform);

                int rightObstacle = Random.Range(1, obstacles.Length);
                if (GameManager.bossIsAlive)
                {
                    rightObstacle = 1;
                }
                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

        }

        else if (noOfItems > GameManager.oneObstaclePercent && noOfItems <= GameManager.oneObstaclePercent + GameManager.twoObstaclesPercent) //if 2 item is going to spawn
        {
            int platformChosen = Random.Range(1, 3);

            if (platformChosen == 1)
            {
                int obstacle = Random.Range(1, obstacles.Length);
                int rightObstacle = Random.Range(1, obstacles.Length);
                if (GameManager.bossIsAlive)
                {
                    obstacle = 1;
                    rightObstacle = 1;
                }

                //if platform chosen to be middle and right
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, 0, leftPlatform);

                
                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);

                
                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;

            }

            else if (platformChosen == 2) //platform is left and right
            {
                int leftObstacle = Random.Range(1, obstacles.Length);
                int rightObstacle = Random.Range(1, obstacles.Length);
                if (GameManager.bossIsAlive)
                {
                    leftObstacle = 1;
                    rightObstacle = 1;
                }
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);

                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, 0, platform);

                
                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }

            else //left middle
            {
                int leftObstacle = Random.Range(1, obstacles.Length);
                int obstacle = Random.Range(1, obstacles.Length);
                if (GameManager.bossIsAlive)
                {
                    leftObstacle = 1;
                    obstacle = 1;
                }
                Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);

                
                Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);

                Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, 0, rightPlatform);

                leftPlatformStatus = newLeftObstacle;
                platformStatus = newObstacle;
                rightPlatformStatus = newRightObstacle;
            }
        }

        else if (noOfItems > ((GameManager.oneObstaclePercent + GameManager.twoObstaclesPercent + GameManager.threeObstaclesPercent) - GameManager.threeObstaclesPercent))
        {
            int leftObstacle = Random.Range(1, obstacles.Length);
            int obstacle = Random.Range(1, obstacles.Length);
            int rightObstacle = Random.Range(1, obstacles.Length);
            if (GameManager.bossIsAlive)
            {
                leftObstacle = 1;
                obstacle = 1;
                rightObstacle = 1;
            }
            Transform newLeftObstacle = HandleRecyclingObstacles(obstacleQueueLeft, leftObstacle, leftPlatform);

            
            Transform newObstacle = HandleRecyclingObstacles(obstacleQueue, obstacle, platform);

            
            Transform newRightObstacle = HandleRecyclingObstacles(obstacleQueueRight, rightObstacle, rightPlatform);

            leftPlatformStatus = newLeftObstacle;
            platformStatus = newObstacle;
            rightPlatformStatus = newRightObstacle;
        }

        else //not needed
        {
            leftPlatformStatus = null;
            platformStatus = null;
            rightPlatformStatus = null;
        }
    }

    Transform HandleRecyclingObstacles(Queue<Transform> queue, int index, Transform transform)
    {
        if (GameManager.bossIsAlive)
        {
            Transform gObj = Instantiate(bossFightItems[index], new Vector3(transform.position.x, transform.position.y + bossFightItemsHeight[index], transform.position.z), Quaternion.identity);
            queue.Enqueue(gObj);
            return gObj;
        }

        else
        {
            Transform gObj = Instantiate(obstacles[index], new Vector3(transform.position.x, transform.position.y + obstaclesHeight[index], transform.position.z), Quaternion.identity);
            queue.Enqueue(gObj);
            return gObj;
        }
    }

    void RecyclePowerups(Transform leftStatus, Transform middleStatus, Transform rightStatus)
    {
        int percentage = Random.Range(1, 101);

        //None = 40%
        //Magnet = 30%
        //Vest = 20%
        //Magic Wand = 10%

        Transform[] possibleSpawn = CheckIfEmpty(leftStatus, middleStatus, rightStatus);

        int rand = Random.Range(0, possibleSpawn.Length);

        if(GameManager.readyForPowerup == true)
        {
            if (possibleSpawn.Length != 0)
            {
                if (percentage <= GameManager.percentMagicWand)
                {
                    int magicWand = 4;
                    Instantiate(powerups[magicWand], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[rand].transform.position.y + (powerups[magicWand].transform.localScale.y / 2), possibleSpawn[rand].transform.position.z), Quaternion.identity);
                }

                else if (percentage > GameManager.percentMagicWand && percentage <= GameManager.percentVest + GameManager.percentMagicWand)
                {
                    int vest = 3;
                    Instantiate(powerups[vest], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[rand].transform.position.y + (powerups[vest].transform.localScale.y / 2), possibleSpawn[rand].transform.position.z), Quaternion.identity);
                }

                else if (percentage > GameManager.percentMagicWand + GameManager.percentVest && percentage <= GameManager.percentMagicWand + GameManager.percentVest + GameManager.percentMagnet)
                {
                    int magnet = 2;
                    Instantiate(powerups[magnet], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[rand].transform.position.y + (powerups[magnet].transform.localScale.y / 2), possibleSpawn[rand].transform.position.z), Quaternion.identity);
                }

                else
                {
                    int coin = 1;
                    Instantiate(powerups[coin], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[rand].transform.position.y + (powerups[coin].transform.localScale.y / 2), possibleSpawn[rand].transform.position.z), transform.rotation);
                }

                if (possibleSpawn.Length == 2)
                {
                    int coin = 1;
                    Instantiate(powerups[coin], new Vector3(possibleSpawn[1 - rand].transform.position.x, possibleSpawn[1 - rand].transform.position.y + (powerups[coin].transform.localScale.y / 2), possibleSpawn[1 - rand].transform.position.z), transform.rotation);
                }
            }

            GameManager.readyForPowerup = false;
        }

        else
        {
            if (possibleSpawn.Length == 2)
            {
                if (GameManager.bossIsAlive)
                {
                    int bossPowerUp = 2;
                    Instantiate(bossFightItems[bossPowerUp], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[rand].transform.position.y + (bossFightItems[bossPowerUp].transform.localScale.y / 2), possibleSpawn[rand].transform.position.z), Quaternion.identity);
                    Instantiate(bossFightItems[bossPowerUp], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[1-rand].transform.position.y + (bossFightItems[bossPowerUp].transform.localScale.y / 2), possibleSpawn[1-rand].transform.position.z), Quaternion.identity);
                }
                int coin = 1;
                Instantiate(powerups[coin], new Vector3(possibleSpawn[rand].transform.position.x, possibleSpawn[rand].transform.position.y + (powerups[coin].transform.localScale.y / 2), possibleSpawn[rand].transform.position.z), transform.rotation);
                Instantiate(powerups[coin], new Vector3(possibleSpawn[1 - rand].transform.position.x, possibleSpawn[1 - rand].transform.position.y + (powerups[coin].transform.localScale.y / 2), possibleSpawn[1 - rand].transform.position.z), transform.rotation);
            }
        }
        
    }

    void DestroyLaneObstacles(ref Queue<Transform> obstacleq)
    {
        for (int i = 0;i< noOfObstacles;i++)
        {
            Transform obstacle = obstacleq.Dequeue();
            Instantiate(destroyParticle, new Vector3(obstacle.transform.position.x, obstacle.transform.position.y + 1.5f, obstacle.transform.position.z), Quaternion.identity);
            Destroy(obstacle.gameObject);
        }
    }

    public void DestroyAllObstacles()
    {
        DestroyLaneObstacles(ref obstacleQueueLeft);
        DestroyLaneObstacles(ref obstacleQueue);
        DestroyLaneObstacles(ref obstacleQueueRight);


        for (int i = 0; i < noOfObstacles; i++)
        {
            obstacleQueueLeft.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueue.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
            obstacleQueueRight.Enqueue((Transform)Instantiate(obstacles[Random.Range(0, 2)]));
        }

    }

    Transform[] CheckIfEmpty(Transform left, Transform middle, Transform right)
    {
        Transform[] storeInside = new Transform[3];
        int count = 0;
        if (left.gameObject.tag == obstacles[0].tag)
        {
            storeInside[0] = left;
            count++;
            
            if(middle.gameObject.tag == obstacles[0].tag)
            {
                storeInside[1] = middle;
                storeInside[2] = null;
                count++;
            }

            else if(right.gameObject.tag == obstacles[0].tag)
            {
                storeInside[1] = null;
                storeInside[2] = right;
                count++;
            }

            else
            {
                storeInside[1] = null;
                storeInside[2] = null;
            }

            
        }

        else if(middle.gameObject.tag == obstacles[0].tag)
        {
            storeInside[1] = middle;
            count++;

            if (right.gameObject.tag == obstacles[0].tag)
            {
                storeInside[0] = null;
                storeInside[2] = right;
                count++;
            }

            else
            {
                storeInside[0] = null;
                storeInside[2] = null;
            }

            
        }

        else if(right.gameObject.tag == obstacles[0].tag)
        {
            storeInside[0] = null;
            storeInside[1] = null;
            storeInside[2] = right;
            count++;
            
        }

        else
        {
            storeInside[0] = null;
            storeInside[1] = null;
            storeInside[2] = null;
        }

        Transform[] result = new Transform[count];
        int count2 = 0;
        foreach (Transform obs in storeInside)
        {
            if (obs != null)
            {
                result[count2] = obs;
                count2++;
            }
        }

        return result;

    }
}
