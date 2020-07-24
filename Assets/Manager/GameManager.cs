using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    private Platform platform;
    public Camera mainCamera;
    public Chaser chaser;
    public GameObject destroyParticle;
    public GameObject boss;

    public  int bossTotalHeath = 10;
    [HideInInspector]
    public static int bossCurrentHealth;
    [HideInInspector]
    public static bool bossIsAlive = false;
    public static bool bossFightActive = false;

    float minigameDistance = 100;
    float powerupDistance = 200;
    public float bossCoolDownLength = 120f;

    public static int oneObstaclePercent = 35;
    public static int twoObstaclesPercent = 45;
    public static int threeObstaclesPercent = 20;

    public static int percentMagicWand = 10;
    public static int percentVest = 20;
    public static int percentMagnet = 30;

    public static bool readyForPowerup = false; //put in game manager when cross 500m.

    public Text coinText;
    public Text distanceText;
    public GameObject wandButton;
    public GameObject magnetImage;
    public GameObject vestImage;
    public Image vestFillBar;
    public Image magnetFillBar;
    public Text coinTextGameOver;
    public Text distanceTextGameOver;
    public GameObject gameOverCanvas;
    public GameObject mainCanvas;
    public GameObject pauseUI;
    public Text wandText;
    public GameObject bossFightStart;
    public GameObject bossFightEnd;
    public GameObject startAnimation;
    public Image bossHealthBar;

    private AudioController audioController;

    // Use this for initialization
    void Start()
    {
        Player.canJump = true;
        bossFightActive = false;
        bossIsAlive = false;
        GameStatus.bossFightCoolDown = bossCoolDownLength;
        gameOverCanvas.SetActive(false);
        pauseUI.SetActive(false);
        platform = GameObject.Find("Game Manager").GetComponent<Platform>();
        GameStatus.vest = false;
        GameStatus.vestRemaining = 0f;
        GameStatus.magicWand = false;
        GameStatus.magnet = false;
        GameStatus.magnetRemaining = 0f;
        GameStatus.coinCollected = 0;
        GameStatus.distanceTravelled = 0f;
        audioController = GameObject.Find("Audio").GetComponent<AudioController>();
        startAnimation.SetActive(true);
        bossHealthBar.gameObject.SetActive(false);
        AudioController.MusicFadeIn(audioController.mainBGM, 0.008f, 0.15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatus.bossFightCoolDown > 0)
        {
            GameStatus.bossFightCoolDown -= Time.deltaTime;
        }
        if (Mathf.RoundToInt(GameStatus.distanceTravelled) % powerupDistance == 0 && Mathf.RoundToInt(GameStatus.distanceTravelled) != 0 && !bossIsAlive)
        {
            readyForPowerup = true;
        }

        //if(Mathf.RoundToInt(GameStatus.distanceTravelled) % minigameDistance == 0) //for every 100 metres
        //{
        //    float n = Random.Range(0, 2);
        //    if (n == 0)
        //    {
        //        QTEStart();
        //    } 

        //    else if (n == 1)
        //    {
        //        BossFightStart();
        //    }

        //    readyToSpawn = true;
        //}

        PowerUpCountDown();
        DisplayPowerUps();
        if (bossCurrentHealth <= 0 && bossIsAlive == true)
        {
            Instantiate(destroyParticle, boss.transform.position, Quaternion.identity);
            boss.SetActive(false);
            bossIsAlive = false;
            bossFightEnd.SetActive(true);
            Invoke("BossFigntEnd",3);
            bossHealthBar.gameObject.SetActive(false);

        }

        UpdateUIText();
        if (!bossFightActive && GameStatus.bossFightCoolDown <= 0)
        {
            BossFightStart();
        }

        if (bossIsAlive == true)
        {
            bossHealthBar.fillAmount =   bossCurrentHealth/ (float)bossTotalHeath;
            
        }

    }

    

    void PowerUpCountDown()
    {
        GameStatus.vestRemaining -= Time.deltaTime;
        GameStatus.magnetRemaining -= Time.deltaTime;
        if (GameStatus.vestRemaining <= 0f)
        {
            GameStatus.vest = false;
        }
        if (GameStatus.magnetRemaining <= 0f)
        {
            GameStatus.magnet = false;
        }
    }

    public void UseMagicWind()
    {
        audioController.magic.PlayOneShot(audioController.magic.clip);

        platform.DestroyAllObstacles();
        GameStatus.magicWand = false;
    }

    public void TakingFatalHit()   //when player is hit, either function will be called
    {
        Instantiate(destroyParticle, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), Quaternion.identity);

        if (GameStatus.vest == false)
        {
            //gameover event

            GameOver();

        }
        else
        {
            print("Protected");
            GameStatus.vest = false;
        }
    }
    public void TakingLightHit()
    {
        Instantiate(destroyParticle, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), Quaternion.identity);

        audioController.hit.PlayOneShot(audioController.hit.clip);

        if (GameStatus.vest == false)
        {
            if (chaser.isClose == true)
            {
                GameOver();
            }
            else if (chaser.isFar == true)
            {
                chaser.timeStarted = Time.time;
                chaser.startPos = chaser.gameObject.transform.position;
                chaser.dangerousTimeRemaining = chaser.dangerousDuration;
                chaser.isFar = false;
                chaser.isClose = true;
                chaser.isLerping = true;
            }
        }
        else
        {
            GameStatus.vest = false;
        }

    }

    public void BossFigntEnd()
    {
        {
            audioController.MusicTransition(audioController.mainBGM, audioController.bossFightBGM);
            bossFightActive = false;
            bossFightEnd.SetActive(false);
            //boss defeated
            //disable all the grenades
            GameStatus.bossFightCoolDown = bossCoolDownLength;
            boss.SetActive(false);
        }
    }

    public void BossFightStart()
    {
        bossCurrentHealth = bossTotalHeath;
        bossFightActive = true;
        bossFightStart.SetActive(true);
        Invoke("BossAppear", 3);
    }

    public void BossAppear()
    {
        audioController.MusicTransition(audioController.bossFightBGM, audioController.mainBGM);
        bossIsAlive = true;
        bossFightStart.SetActive(false);
        boss.SetActive(true);
        bossHealthBar.gameObject.SetActive(true);
    }

    void QTEStart()
    {

    }

    void DefaultValues()
    {
        oneObstaclePercent = 50;
        twoObstaclesPercent = 40;
        threeObstaclesPercent = 10;

        percentVest = 40;
        percentMagnet = 60;
    }

    void IncreaseDifficulty()
    {
        Player.acceleration += 5f;

        //obstacle
    }

    void UpdateUIText()
    {
        float currentDistance = Mathf.RoundToInt(GameStatus.distanceTravelled);
        coinText.text = "Coin:" + GameStatus.coinCollected.ToString();
        distanceText.text = currentDistance.ToString();

    }

    void DisplayPowerUps()
    {
        if (GameStatus.vest)
        {
            vestImage.SetActive(true);
            vestFillBar.fillAmount = GameStatus.vestRemaining / 30f;
        }
        else
        {
            vestImage.SetActive(false);
        }
        if (GameStatus.magnet)
        {
            magnetImage.SetActive(true);
            magnetFillBar.fillAmount = GameStatus.magnetRemaining / 15f;
        }
        else
        {
            magnetImage.SetActive(false);
        }
        if (GameStatus.magicWand)
        {
            wandButton.SetActive(true);
        }
        else
        {
            wandButton.SetActive(false);
        }
    }

    public void GameOver()
    {
        Animator playerAnim = player.GetComponent<Animator>();
        playerAnim.SetTrigger("Die");
        player.GetComponent<Player>().enabled = false;
        
        Instantiate(destroyParticle, new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z), Quaternion.identity);
        audioController.die.PlayOneShot(audioController.die.clip);
        chaser.timeStarted = Time.time;
        chaser.startPos = chaser.gameObject.transform.position;
        chaser.isCaught = true;
        chaser.isFar = false;
        chaser.isClose = false;
        
        chaser.isLerping=true;
        
        gameOverCanvas.SetActive(true);
        coinTextGameOver.text = "Coin:"+GameStatus.coinCollected.ToString();
        distanceTextGameOver.text = "Distance:"+Mathf.RoundToInt(GameStatus.distanceTravelled).ToString();
        //mainCamera.transform.parent = null;
        mainCanvas.SetActive(false);
        
        

        GameStatus.vest = false;
        GameStatus.vestRemaining = 0f;
        GameStatus.magicWand = false;
        GameStatus.magnet = false;
        GameStatus.magnetRemaining = 0f;
        GameStatus.coinCollected = 0;
        GameStatus.distanceTravelled = 0f;

    }

    public void GamePause()
    {
        pauseUI.SetActive(true);
        audioController.click.PlayOneShot(audioController.click.clip);
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        pauseUI.SetActive(false);
        audioController.click.PlayOneShot(audioController.click.clip);
        Time.timeScale = 1;
    }

    public void PickingUpWand()
    {
        if (GameStatus.usedWand == false)
        {
            wandText.gameObject.SetActive(true);
        }
        else
        {
            wandText.gameObject.SetActive(false);
        }
    }

    
    
}
