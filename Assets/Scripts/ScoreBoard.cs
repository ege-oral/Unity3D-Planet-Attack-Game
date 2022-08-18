using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score = 0;

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        print($"Score is now: {score}");
    }
}
