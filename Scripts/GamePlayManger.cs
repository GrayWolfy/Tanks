using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GamePlayManger : MonoBehaviour
{
    Tilemap waterTilemap, concreteTilemap;

    [SerializeField] Image topCurtain, bottomCurtain, blackCurtain;

    [SerializeField] Text level, gameOver;
    
    [SerializeField] private Text stage, lifes, reserve;

    [SerializeField] GameObject[] bonusCrates;

    private GameObject[] spawnPoints, playerSpawnPoints;
    public static int tankCount;
    private const int TankLimit = 15;
    private bool tankQuantity;
    private int tanksLeft;
    public static bool freezing = false;
    bool stageStart;
    GameObject[] smallTanks, armoredTanks, fastTanks, heavyTanks;

    // Start is called before the first frame update
    void Start()
    {
        concreteTilemap = GameObject.Find("Concrete").GetComponent<Tilemap>();
        waterTilemap = GameObject.Find("Water").GetComponent<Tilemap>();
        
        tanksLeft = LevelManager.limit;
        level.text = "STAGE " + Tracker.stageNumber;
        stageStart = true;
        
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        
        stage.text = Tracker.stageNumber.ToString();
        lifes.text = Tracker.playerLifes.ToString();
        reserve.text = tanksLeft.ToString();
        
        StartCoroutine(StartStage());
        
        stageStart = false;
    }

    private void Update()
    {
        lifes.text = Tracker.playerLifes.ToString();
        reserve.text = tanksLeft.ToString();

        if (tankQuantity && 0 == GameObject.FindGameObjectsWithTag("Small").Length &&
            0 == GameObject.FindGameObjectsWithTag("Fast").Length &&
            0 == GameObject.FindGameObjectsWithTag("Armored").Length &&
            0 == GameObject.FindGameObjectsWithTag("Heavy").Length)
        {
            Tracker.cleared = true;
            LevelCompleted();
        }
    }

    private void LevelCompleted()
    {
        tankCount = 0;
        tankQuantity = false;
        SceneManager.LoadScene("Score");
    }

    IEnumerator StartStage()
    {
        StartCoroutine(RevealStageNumber());
        yield return new WaitForSeconds(5);
        StartCoroutine(RevealTopStage());
        StartCoroutine(RevealBottomStage());
        new WaitForSeconds(20);
        InvokeRepeating("spawnEnemy", LevelManager.spawnRate, LevelManager.spawnRate);
    }

    IEnumerator RevealStageNumber()
    {
        while (blackCurtain.rectTransform.localScale.y > 0)
        {
            blackCurtain.rectTransform.localScale = new Vector3(1,
                Mathf.Clamp(blackCurtain.rectTransform.localScale.y - Time.deltaTime, 0, 1), 1);
            yield return null;
        }
    }

    IEnumerator RevealTopStage()
    {
        level.enabled = false;
        while (topCurtain.rectTransform.position.y < 1250)
        {
            topCurtain.rectTransform.Translate(new Vector3(0, 500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    IEnumerator RevealBottomStage()
    {
        while (bottomCurtain.rectTransform.position.y > -400)
        {
            bottomCurtain.rectTransform.Translate(new Vector3(0, -500 * Time.deltaTime, 0));
            yield return null;
        }
    }

    public IEnumerator GameOver()
    {
        while (gameOver.rectTransform.localPosition.y < 0)
        {
            gameOver.rectTransform.localPosition = new Vector3(gameOver.rectTransform.localPosition.x,
                gameOver.rectTransform.localPosition.y + 120f * Time.deltaTime, gameOver.rectTransform.localPosition.z);
            yield return null;
        }
    }

    public void spawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Small").Length + GameObject.FindGameObjectsWithTag("Fast").Length +
            GameObject.FindGameObjectsWithTag("Armored").Length + GameObject.FindGameObjectsWithTag("Heavy").Length <
            LevelManager.tanksThisLevel && tankCount < LevelManager.limit)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Animator anime = spawnPoints[spawnPointIndex].GetComponent<Animator>();
            anime.SetTrigger("Spawn");
        }

        if (tankCount == LevelManager.limit)
        {
            print("Limit reached");
            tankQuantity = true;
            CancelInvoke();
        }
    }

    public void SpawnPlayer()
    {
        Tracker.playerLifes--;
        lifes.text = Tracker.playerLifes.ToString();

        if (Tracker.playerLifes > 0)
        {
            Animator anime = playerSpawnPoints[0].GetComponent<Animator>();
            anime.SetTrigger("Spawn");
        }
        else
        {
            tankCount = 0;
            StartCoroutine(GameOver());
            float timeToLoadScene = 10;
            Invoke("GoToScene", timeToLoadScene);
        }
    }

    public void SetReserve()
    {
        reserve.text = (tanksLeft--).ToString();
    }

    void GoToScene()
    {
        Enemy.freezing = false;
        Tracker.playerLifes = 3;
        SceneManager.LoadScene("Score");
    }

    bool InvalidBonusCratePosition(Vector3 cratePosition)
    {
        return waterTilemap.GetTile(waterTilemap.WorldToCell(cratePosition)) != null ||
               concreteTilemap.GetTile(concreteTilemap.WorldToCell(cratePosition)) != null;
    }

    public void GenerateBonusCrate()
    {
        GameObject bonusCrate = bonusCrates[Random.Range(0, bonusCrates.Length)];
        print(bonusCrate);
        Vector3 cratePosition = new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 0);

        if (InvalidBonusCratePosition(cratePosition))
        {
            do
            {
                cratePosition = new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), 0);
                if (!InvalidBonusCratePosition(cratePosition))
                    Instantiate(bonusCrate, cratePosition, Quaternion.identity);
            } while (InvalidBonusCratePosition(cratePosition));
        }
        else
        {
            Instantiate(bonusCrate, cratePosition, Quaternion.identity);
        }
    }

    public void UpdatePlayerLifes()
    {
        lifes.text = Tracker.playerLifes.ToString();
    }

    public void ActivateFreeze()
    {
        StartCoroutine(FreezeActivated());
    }

    IEnumerator FreezeActivated()
    {
        Enemy.freezing = true;
        
        List<GameObject> enemies = new List<GameObject>();
        
        smallTanks = GameObject.FindGameObjectsWithTag("Small");
        armoredTanks = GameObject.FindGameObjectsWithTag("Armored");
        fastTanks = GameObject.FindGameObjectsWithTag("Fast");
        heavyTanks = GameObject.FindGameObjectsWithTag("Heavy");
        
        enemies.AddRange(smallTanks);
        enemies.AddRange(armoredTanks);
        enemies.AddRange(fastTanks);
        enemies.AddRange(heavyTanks);
        
        GameObject[] enemiesArray = enemies.ToArray();

        for (int i = 0; i < enemiesArray.Length; i++)
        {
            enemiesArray[i].gameObject.SetActive(false);
            enemiesArray[i].gameObject.GetComponent<Enemy>().ToFreezeTank();
            enemiesArray[i].gameObject.GetComponent<Enemy>().enabled = false;
            enemiesArray[i].gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(10);

        for (int i = 0; i < enemiesArray.Length; i++)
        {
            enemiesArray[i].gameObject.SetActive(false);
            enemiesArray[i].gameObject.GetComponent<Enemy>().enabled = true;
            enemiesArray[i].gameObject.GetComponent<Enemy>().ToUnfreezeTank();
            enemiesArray[i].gameObject.SetActive(true);
        }

        Enemy.freezing = false;
    }
}