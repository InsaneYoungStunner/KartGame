using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class KartSpeedPad : MonoBehaviour
{

    public MultiplicativeKartModifier boostStats;

    [Range (0, 5)]
    public float duration = 1f;
    [Tooltip("The amount of score the pickup gives in secs")]
    public int ScoreGained;

    void OnTriggerEnter(Collider other){
        var rb = other.attachedRigidbody;
        if (rb == null) return;
        var kart = rb.GetComponent<KartMovement>();
        kart.StartCoroutine(KartModifier(kart, duration));

        if (!kart.IsOtherKart)
        {
            ScoreManager.OnAdjustScore(ScoreGained);
        }

        
    }

    IEnumerator KartModifier(KartGame.KartSystems.KartMovement kart, float lifetime){
        kart.AddKartModifier(boostStats);
        yield return new WaitForSeconds(lifetime);
        kart.RemoveKartModifier(boostStats);
    }

}
