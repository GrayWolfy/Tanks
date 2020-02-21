using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    static Tracker instance;

    [SerializeField]
    int smallTankPoints = 100, fastTankPoints = 200, bigTankPoints= 300, armoredTankPoints=400;
    public int smallTankPointsWorth { get { return smallTankPoints; } }
    public int fastTankPointsWorth { get { return fastTankPoints; } }
    public int bigTankPointsWorth { get { return bigTankPoints; } }
    public int armoredTankPointsWorth { get { return armoredTankPoints; } }

    public static int smallTankDestroyed, fastTankDestroyed, bigTankDestroyed, armoredTankDestroyed;
    public static int stageNumber;
    public static int playerScore = 0;
    public static int playerLifes = 3;
    public static bool cleared;
    public static int totalScore;
    public static int highScore;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
}
