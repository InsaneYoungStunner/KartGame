using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using KartGame.KartSystems;
using UnityEngine;
using UnityEngine.UI;
namespace KartGame.Track {
    /// <summary>
    /// A MonoBehaviour to deal with all the time and positions for the racers.
    /// </summary>
    public class TrackManager : MonoBehaviour {
        public Text showText;
        [Tooltip ("The name of the track in this scene.  Used for track time records.  Must be unique.")]
        public string trackName;
        [Tooltip ("Number of laps for the race.")]
        public int raceLapTotal;
        [Tooltip ("All the checkpoints for the track in the order that they should be completed starting with the start/finish line checkpoint.")]
        public List<PickupObject> checkpoints = new List<PickupObject> ();
        [Tooltip ("Reference to an object responsible for repositioning karts.")]
        public KartRepositioner kartRepositioner;
       

        bool m_IsRaceRunning;
        //Dictionary<IRacer, PickupObject> m_RacerNextCheckpoints = new Dictionary<IRacer, PickupObject> (16);
        TrackRecord m_SessionBestTime = TrackRecord.CreateDefault ();
        TrackRecord m_SessionBestScore = TrackRecord.CreateDefault ();
  

        public bool IsRaceRunning => m_IsRaceRunning;

        /// <summary>
        /// Returns the best lap time recorded this session.  If no record is found, -1 is returned.
        /// </summary>
        //public float SessionBestTime
        //{
        //    get {
        //        if (m_SessionBestTime != null && m_SessionBestTime.time < float.PositiveInfinity)
        //            return m_SessionBestTime.time;
        //        return -1f;
        //    }
        //}

        ///// <summary>
        ///// Returns the best race time recorded this session.  If no record is found, -1 is returned.
        ///// </summary>
        //public float SessionBestScore
        //{
        //    get {
        //        if (m_SessionBestScore != null && m_SessionBestScore.time < float.PositiveInfinity)
        //            return m_SessionBestScore.time;
        //        return -1f;
        //    }
        //}

        public void BackMain()
        {
            GameObject.Find("Canvas").transform.Find("StartButton").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("StartButton2").gameObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("Kart_Display").gameObject.SetActive(true);

            GameObject.Find("CancelButton").SetActive(false);
            GameObject.Find("Scroll View").SetActive(false);
            showText.text = "";



        }


        public void ShowTimeRecord()
        {
            GameObject.Find("Canvas").transform.Find("StartButton").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("StartButton2").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Kart_Display").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("CancelButton").gameObject.SetActive(true);
            //GameObject.FindGameObjectWithTag("CancelButton").transform.gameObject.SetActive(true);
            
            GameObject.Find("zhanji").transform.Find("Scroll View").gameObject.SetActive(true);
            List<TrackRecord> list = getBestTimeHistory();
            showText.text = "";

            for (int i = 0; i < list.Count; i++)
            {
                showText.text += "第" + i + "条战绩\n";
                showText.text += "Name:" + list[i].name+ "\n";
                showText.text += "Score:" + list[i].score+ "\n";
                showText.text += "Time:" + list[i].time + "\n";
                showText.text += "costTime:" + list[i].costTime + "\n";
                showText.text += "coinCount:" + list[i].coinCount + "\n";
                showText.text += "padCount:" + list[i].padCount + "\n" + "\n";
            }

        }

        public void ShowScoreRecord()
        {
            GameObject.Find("Canvas").transform.Find("StartButton").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("StartButton2").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Kart_Display").gameObject.SetActive(false);

            GameObject.Find("Canvas").transform.Find("CancelButton").gameObject.SetActive(true);
            //GameObject.FindGameObjectWithTag("CancelButton").transform.gameObject.SetActive(true);

            GameObject.Find("zhanji").transform.Find("Scroll View").gameObject.SetActive(true);
            List<TrackRecord> list = getBestScoreHistory();
            showText.text = "";

            for (int i = 0; i < list.Count; i++)
            {
                showText.text += "第" + i + "条战绩\n";
                showText.text += "Name:" + list[i].name + "\n";
                showText.text += "Score:" + list[i].score + "\n";
                showText.text += "Time:" + list[i].time + "\n";
                showText.text += "costTime:" + list[i].costTime + "\n";
                showText.text += "coinCount:" + list[i].coinCount + "\n";
                showText.text += "padCount:" + list[i].padCount + "\n" + "\n";
            }
            Debug.Log(showText.text);
        }


        public void ShowCoinRecord()
        {
            GameObject.Find("Canvas").transform.Find("StartButton").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("StartButton2").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("Kart_Display").gameObject.SetActive(false);

            GameObject.Find("Canvas").transform.Find("CancelButton").gameObject.SetActive(true);
            //GameObject.FindGameObjectWithTag("CancelButton").transform.gameObject.SetActive(true);

            GameObject.Find("zhanji").transform.Find("Scroll View").gameObject.SetActive(true);
            List<TrackRecord> list = getBestCoinHistory();
            showText.text = "";

            for (int i = 0; i < list.Count; i++)
            {
                showText.text += "第" + i + "条战绩\n";
                showText.text += "Name:" + list[i].name + "\n";
                showText.text += "Score:" + list[i].score + "\n";
                showText.text += "Time:" + list[i].time + "\n";
                showText.text += "costTime:" + list[i].costTime + "\n";
                showText.text += "coinCount:" + list[i].coinCount + "\n";
                showText.text += "padCount:" + list[i].padCount + "\n" + "\n";
            }

        }



        public List<TrackRecord> getBestTimeHistory()
        {
            return BestTimeRecord.getBestTimeRecord().records;
        }

        public List<TrackRecord> getBestCoinHistory()
        {
            return BestCoinRecord.getBestCoinRecord().records;
        }

        public List<TrackRecord> getBestScoreHistory()
        {
            return BestScoreRecord.getBestScoreRecord().records;
        }

        /// <summary>
        /// Returns the rank of Player.
        /// </summary>
        public string GamerRank {
            get {
                //if(m_trackRecorddata != null)
                //    return Convert.ToString(m_trackRecorddata.rank);
                return "---";
            }
        }

        void Awake () {
            if (checkpoints.Count < 3)
                Debug.LogWarning ("There are currently " + checkpoints.Count + " checkpoints set on the Track Manager.  A minimum of 3 is recommended but kart control will not be enabled with 0.");

            // m_HistoricalBestLap = TrackRecord.Load (trackName, 1);
            // m_HistoricalBestRace = TrackRecord.Load (trackName, raceLapTotal);
        }

        void OnEnable () {
            for (int i = 0; i < checkpoints.Count; i++) {
                //checkpoints[i].OnKartHitCheckpoint += CheckRacerHitCheckpoint;
            }
        }

        void OnDisable () {
            for (int i = 0; i < checkpoints.Count; i++) {
                //checkpoints[i].OnKartHitCheckpoint -= CheckRacerHitCheckpoint;
            }
        }

        void Start () {
            Debug.Log("TrackManager" + JsonUtility.ToJson(BestTimeRecord.getBestTimeRecord().records));
        }

        /// <summary>
        /// Starts the timers and enables control of all racers.
        /// </summary>
        public void StartRace () {
            if (checkpoints.Count == 0)
                return;

        

            UnityEngine.Object[] allRacerArray = FindObjectsOfType<UnityEngine.Object> ().Where (x => x is IRacer).ToArray ();


            m_IsRaceRunning = true;

            
        }

        /// <summary>
        /// Stops the timers and disables control of all racers, also saves the historical records.
        /// </summary>
        public void StopRace (string name, int score, int padCount, int coinCount, string newTime, float costTime) {
            m_IsRaceRunning = false;
            TrackRecord tr = TrackRecord.CreateDefault();
            tr.SetRecord(name,score, padCount, coinCount, newTime, costTime);
            Debug.Log(JsonUtility.ToJson(tr));
            BestTimeRecord bt = BestTimeRecord.getBestTimeRecord();
            BestTimeRecord.Add(tr);
            BestTimeRecord.Save(bt);
            //Debug.Log(JsonUtility.ToJson(BestTimeRecord.getBestTimeRecord()));
          
            

            BestScoreRecord bs = BestScoreRecord.getBestScoreRecord();
            BestScoreRecord.Add(tr);
            BestScoreRecord.Save(bs);

            BestCoinRecord bc = BestCoinRecord.getBestCoinRecord();
      
            BestCoinRecord.Add(tr);
            BestCoinRecord.Save(bc);

           

            
        }


        public void StopRace()
        {
            m_IsRaceRunning = false;
         
        }
      
    }
}