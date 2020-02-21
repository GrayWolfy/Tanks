using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Text
        hiScoreText,
        stageText,
        playerScoreText,
        smallTankScoreText,
        fastTankScoreText,
        bigTankScoreText,
        armoredTankScoreText,
        smallTanksDestroyed,
        fastTanksDestroyed,
        bigTanksDestroyed,
        armoredTanksDestroyed,
        totalTanksDestroyed;

    int smallTankScore, fastTankScore, bigTankScore, armoredTankScore;
    Tracker masterTracker;
    int smallTankPointsWorth, fastTankPointsWorth, bigTankPointsWorth, armoredTankPointsWorth;

    void Start()
    {
        masterTracker = GameObject.Find("Tracker").GetComponent<Tracker>();
        smallTankPointsWorth = masterTracker.smallTankPointsWorth;
        fastTankPointsWorth = masterTracker.fastTankPointsWorth;
        bigTankPointsWorth = masterTracker.bigTankPointsWorth;
        armoredTankPointsWorth = masterTracker.armoredTankPointsWorth;
        stageText.text = "STAGE " + Tracker.stageNumber;
        playerScoreText.text = Tracker.playerScore.ToString();
        StartCoroutine(UpdateTankPoints());
    }

    IEnumerator UpdateTankPoints()
    {
        for (int i = 0; i <= Tracker.smallTankDestroyed; i++)
        {
            smallTankScore = smallTankPointsWorth * i;
            smallTankScoreText.text = smallTankScore.ToString();
            smallTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i <= Tracker.fastTankDestroyed; i++)
        {
            fastTankScore = fastTankPointsWorth * i;
            fastTankScoreText.text = fastTankScore.ToString();
            fastTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i <= Tracker.bigTankDestroyed; i++)
        {
            bigTankScore = bigTankPointsWorth * i;
            bigTankScoreText.text = bigTankScore.ToString();
            bigTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i <= Tracker.armoredTankDestroyed; i++)
        {
            armoredTankScore = armoredTankPointsWorth * i;
            armoredTankScoreText.text = armoredTankScore.ToString();
            armoredTanksDestroyed.text = i.ToString();
            yield return new WaitForSeconds(0.2f);
        }

        totalTanksDestroyed.text = (Tracker.smallTankDestroyed + Tracker.fastTankDestroyed + Tracker.bigTankDestroyed +
                                    Tracker.armoredTankDestroyed).ToString();
        Tracker.totalScore += (smallTankScore + fastTankScore + bigTankScore + armoredTankScore);

        playerScoreText.text = Tracker.totalScore.ToString();
        hiScoreText.text = Tracker.highScore.ToString();

        if (Tracker.totalScore > Tracker.highScore) Tracker.highScore = Tracker.totalScore;

        yield return new WaitForSeconds(5f);

        if (Tracker.cleared)
        {
            ClearStatistics();
            if (2 == Tracker.stageNumber)
            {
                SceneManager.LoadScene("Won");
            }
            else
            {
                SceneManager.LoadScene("Stage" + (Tracker.stageNumber + 1));
                Tracker.stageNumber++;
            }
        }
        else
        {
            ClearStatistics();
            SceneManager.LoadScene("Stage1");
        }
    }

    void ClearStatistics()
    {
        Tracker.smallTankDestroyed = 0;
        Tracker.fastTankDestroyed = 0;
        Tracker.bigTankDestroyed = 0;
        Tracker.armoredTankDestroyed = 0;
        Tracker.totalScore = 0;
        Tracker.cleared = false;
    }
}