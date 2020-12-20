using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TailManager : MonoBehaviour
{
    public int currentCount { get; private set; }

    public static Action<int> OnAdjustCount;
    private bool raceStarted;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnEnable()
    {

        OnAdjustCount += AdjustCount;
    }

    private void OnDisable()
    {
        OnAdjustCount -= AdjustCount;
    }

    private void AdjustCount(int count)
    {
        currentCount += count;
    }

    void Update()
    {
        if (!raceStarted) return;


    }

    public void StartRace()
    {
        raceStarted = true;
    }

    public void StopRace()
    {
        raceStarted = false;
    }
}
