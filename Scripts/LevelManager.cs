using UnityEngine;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    int tanksPerLevel, stageNumber, tankLimit;
    public static int tanksThisLevel;
    public static int limit;
    [SerializeField]
    float spawnRateThisLevel=5;
    public static float spawnRate;

    private void Awake()
    {
        Tracker.stageNumber = stageNumber;
        tanksThisLevel = tanksPerLevel;
        spawnRate = spawnRateThisLevel;
        limit = tankLimit;
    }
}