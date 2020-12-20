using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
using System;

public class TaillObject : MonoBehaviour
{
    [Header("TaillObject")]

    [Tooltip("New Gameobject (a VFX for example) to spawn when you trigger this PickupObject")]
    public GameObject spawnPrefabOnPickup;

    [Tooltip("Destroy the spawned spawnPrefabOnPickup gameobject after this delay time. Time is in seconds.")]
    public float destroySpawnPrefabDelay = 10;

    [Tooltip("Destroy this gameobject after collectDuration seconds")]
    public float collectDuration = 0f;
    /// <summary>
    /// This is subscribed to by the TrackManager in order to measure a racer's progress around the track.
    /// </summary>

    [Tooltip("The amount of time the pickup gives in secs")]
    public float TimeGained;

    [Tooltip("The amount of score the pickup gives in secs")]
    public int ScoreGained;

    [Tooltip("The point at which the collect VFX is spawned")]
    public Transform CollectVFXSpawnPoint;

    [Header("Sounds")]

    [Tooltip("Sound played when receiving damages")]
    public AudioClip CollectSound;


    void OnEnable()
    {
        
    }


    void Start()
    {

    }

    void OnCollect()
    {
        if (CollectSound)
        {
            AudioUtility.CreateSFX(CollectSound, transform.position, AudioUtility.AudioGroups.Pickup, 0f);
        }



        TimeManager.OnAdjustTime(TimeGained);
        ScoreManager.OnAdjustScore(ScoreGained);
      
     
    }

    void OnTriggerEnter(Collider other)
    {

        if ( other.gameObject.CompareTag("Player"))
        {

            KartMovement km = other.GetComponent<KartMovement>();

            if (km.IsOtherKart)
            {
                OnCollect();
            }
        }
    }
}
