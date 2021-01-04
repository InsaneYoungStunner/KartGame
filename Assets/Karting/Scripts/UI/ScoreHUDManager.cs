using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ScoreHUDManager : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI bestScoreText;
    ScoreManager m_ScoreManager;
    // Start is called before the first frame update
    void Start()
    {
        m_ScoreManager = FindObjectOfType<ScoreManager>();
        DebugUtility.HandleErrorIfNullFindObject<ScoreManager, ObjectiveReachTargets>(m_ScoreManager, this);


        //if (m_ScoreManager.IsFinite)
        //{
        //    currentScoreText.text = "";
        //    bestScoreText.text = "";
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
        currentScoreText.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
        int currentScore = m_ScoreManager.currentScore;
        int bestScore = ScoreManager.bestScore;
        
        currentScoreText.text = string.Format("{0}", currentScore);
        bestScoreText.text = string.Format("{0}", bestScore);
    }
}
