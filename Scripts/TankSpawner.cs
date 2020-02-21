using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    private GameObject[] tanks;

    private GameObject tank;

    [SerializeField] bool isPlayer;

    [SerializeField] private GameObject weakTank, fastShootingTank, armoredTank, heavyTank;
    
    enum tankType
    {
        small, fast, armored, heavy
    };
    
    // Start is called before the first frame update
    void Start()
    {

        
        if (isPlayer)
        {
            tanks = new GameObject[1] { weakTank };
        }
        else
        {
            tanks = new GameObject[4] { weakTank, fastShootingTank, armoredTank, heavyTank };
        }
    }

    public void SpawningStart()
    {
        if (!isPlayer)
        {
            List<int> tankToSpawn = new List<int>();

            tankToSpawn.Add((int)tankType.small);
            tankToSpawn.Add((int)tankType.fast);
            tankToSpawn.Add((int)tankType.armored);
            tankToSpawn.Add((int)tankType.heavy);
            
            int tankID = tankToSpawn[Random.Range(0, tankToSpawn.Count)];
            tank = Instantiate(tanks[tankID], transform.position, transform.rotation);
            
            if (Random.value <= 0.2)
            {
                tank.GetComponent<BonusTank>().MakeBonusTank();
            }
            
            GamePlayManger.tankCount++;

        }
        else
        {
            tank = Instantiate(tanks[0], transform.position, transform.rotation);
        }
    }

    IEnumerator Unfreeze(GameObject tank)
    {
        yield return new WaitForSeconds(10);
        tank.GetComponent<Enemy>().enabled = true;
        tank.GetComponent<Enemy>().ToUnfreezeTank();
        tank.SetActive(true);
        Enemy.freezing = false;
    }
    
    public void SpawnNewTank()
    {
        if (tank != null)
        {
            tank.SetActive(true);
            
            if (Enemy.freezing)
            {
                tank.SetActive(false);
                tank.GetComponent<Enemy>().ToFreezeTank();
                tank.GetComponent<Enemy>().enabled = false;
                tank.SetActive(true);
                
                StartCoroutine(Unfreeze(tank));
            }
        }
    }
}
