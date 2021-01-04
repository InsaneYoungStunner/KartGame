using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore { get; private set; }
    static public int bestScore { get; set; }
    public static Action<int> OnAdjustScore;
    private bool raceStarted;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
     
        OnAdjustScore += AdjustScore;   
    }

    private void OnDisable()
    {
        OnAdjustScore -= AdjustScore;
    }

    private void AdjustScore(int score)
    { 
        currentScore += score;
    }

    void Update()
    {
        if (!raceStarted) return;

       
    }

    public void StartRace()
    {
        raceStarted = true;
        bestScore = 0;
    }

    public void StopRace()
    {
        raceStarted = false;
    }
}
