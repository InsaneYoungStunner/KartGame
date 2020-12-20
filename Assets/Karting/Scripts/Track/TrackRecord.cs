using System;
using System.IO;
using System.Text; //引用这个命名空间是用于接下来用可变的字符串的
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace KartGame.Track {
    /// <summary>
    /// A serializable record for the time on a track.
    /// </summary>
  
    [Serializable]
    public class TrackRecord {
        
        /// <summary>
        /// The time of this record.
        /// </summary>
        public string time;
        /// <summary>
        /// The name of the racer who recorded this time.
        /// </summary>
        public string name;

        public float costTime;

        public int score;

        public int padCount;

        public int coinCount;
        /// <summary>
        /// The rank of this record 
        /// </summary>
        //[SerializeField]
        //public int rank;
        const float k_DefaultTime = float.PositiveInfinity;

        /// <summary>
        /// Set all the information in a record.
        /// </summary>
        /// <param name="track">The new name of the track.</param>
        /// <param name="lapCount">The new lap count.</param>
        /// <param name="racer">The new racer whose name will be recorded.</param>
        /// <param name="newTime">The new time for the record.</param>
        public void SetRecord (string name, int score, int padCount, int coinCount,string newTime, float costTime) {
           
            this.score = score;
            this.name = name;
            this.padCount = padCount;
            this.costTime = costTime;
            this.coinCount = coinCount;
            time = newTime;
        }

        /// <summary>
        /// Creates a TrackRecord with default values.
        /// </summary>
        public static TrackRecord CreateDefault () {
            TrackRecord defaultRecord = new TrackRecord ();
  
            return defaultRecord;
        }

       
    }
    public class FloatSort : IComparer
    {
        #region  IComparer 成员
        public int Compare(object x, object y)
        {
            int iResult;
            if ((float)x > (float)y)
                iResult = 1;
            else
                iResult = -1;
            return iResult;
        }
        #endregion
    }
    public class IntSort : IComparer
    {
      
        public int Compare(object x, object y)
        {
            int iResult;
            if ((int)x > (int)y)
                iResult = 1;
            else
                iResult = -1;
            return iResult;
        }
        
    }
    [Serializable]
    public class BestTimeRecord 
    {
        public List<TrackRecord> records;
        public static SortedList Sortedrecord = new SortedList(new FloatSort());
        public static int BestTimeRecordSize = 10;

        
        const string k_FolderName = "BinaryTrackRecordData";
        const string k_FileName = "BestTimeRecordData";
        const string k_FileExtension = ".json";

        public static void Add(TrackRecord record)
        {
            if (Sortedrecord.Count < BestTimeRecordSize)
            {
                Sortedrecord.Add(record.costTime, record);
            }
            else
            {
                Sortedrecord.RemoveAt(0);
                Debug.Log(JsonUtility.ToJson(Sortedrecord.Values));
                Sortedrecord.Add(record.costTime, record);
                Debug.Log(JsonUtility.ToJson(Sortedrecord.Values));
            }
            
        }

        public static void Save(BestTimeRecord bestrecords)
        {
            bestrecords.records = new List<TrackRecord>();
            foreach (TrackRecord sr in Sortedrecord.Values)
            {
                bestrecords.records.Add(sr);
            }
            Debug.Log(JsonUtility.ToJson(Sortedrecord.Values));
            Debug.Log(JsonUtility.ToJson(bestrecords.records));
            string folderPath = Path.Combine(Application.persistentDataPath, k_FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            
            string dataPath = Path.Combine(folderPath, k_FileName + k_FileExtension);
            Debug.Log(dataPath);
            
            if (File.Exists(dataPath))
            {
                Debug.Log("delete");
                File.Delete(dataPath);
            }
            FileStream fileStream = File.Open(dataPath, FileMode.Create);
            byte[] bytes = new UTF8Encoding().GetBytes(JsonUtility.ToJson(bestrecords));
           
            fileStream.Write(bytes, 0, bytes.Length);
            //每次读取文件后都要记得关闭文件
            fileStream.Close();

        }
        public static BestTimeRecord CreateDefault()
        {
            BestTimeRecord defaultRecord = new BestTimeRecord();
            defaultRecord.records = new List<TrackRecord>();
            Sortedrecord.Capacity = BestTimeRecordSize;
            return defaultRecord;
        }

        public static BestTimeRecord getBestTimeRecord()
        {
            BestTimeRecord br = Load();
           
            Sortedrecord = new SortedList(new FloatSort());
            Debug.Log(JsonUtility.ToJson(br.records));
            foreach (TrackRecord record in br.records)
            {
                Sortedrecord.Add(record.costTime,record);
            }
            return br;
        }

        public static BestTimeRecord Load()
        {
            string folderPath = Path.Combine(Application.persistentDataPath, k_FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dataPath = Path.Combine(folderPath, k_FileName + k_FileExtension);

            if (!File.Exists(dataPath))
            {
                return CreateDefault();
            }

            using (FileStream fileStream = File.Open(dataPath, FileMode.Open))
            {
                if (fileStream.Length == 0)
                    return CreateDefault();

                try
                {
                    byte[] bytes = new byte[10000];
                    fileStream.Read(bytes, 0, bytes.Length);
                    //将读取到的二进制转换成字符串
                    string s = new UTF8Encoding().GetString(bytes);
                    BestTimeRecord loadedRecord = JsonUtility.FromJson<BestTimeRecord>(s);
        
                    if (loadedRecord == null)
                        return CreateDefault();
                    return loadedRecord;
                }
                catch (Exception)
                {
                    return CreateDefault();
                }
            }
        }

    }

    [Serializable]
    public class BestScoreRecord
    {
        public List<TrackRecord> records;
        public static SortedList Sortedrecord = new SortedList(new IntSort());
        public static int BestTimeRecordSize = 10;


        const string k_FolderName = "BinaryTrackRecordData";
        const string k_FileName = "BestScoreRecordData";
        const string k_FileExtension = ".json";

        public static void Add(TrackRecord record)
        {
            if (Sortedrecord.Count < BestTimeRecordSize)
            {
                Sortedrecord.Add(record.score, record);
            }
            else
            {
                Sortedrecord.RemoveAt(0);
                //Debug.Log(JsonUtility.ToJson(Sortedrecord.Values));
                Sortedrecord.Add(record.score, record);
                //Debug.Log(JsonUtility.ToJson(Sortedrecord.Values));
            }

        }

        public static void Save(BestScoreRecord bestrecords)
        {
            bestrecords.records = new List<TrackRecord>();
            foreach (TrackRecord sr in Sortedrecord.Values)
            {
               
                bestrecords.records.Add(sr);
            }
            string folderPath = Path.Combine(Application.persistentDataPath, k_FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dataPath = Path.Combine(folderPath, k_FileName + k_FileExtension);
            Debug.Log(dataPath);
            
            if (File.Exists(dataPath))
            {
                Debug.Log("delete");
                File.Delete(dataPath);
            }
            //using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate))
            //{
            //    binaryFormatter.Serialize(fileStream, bestrecords);
            //}
            FileStream fileStream = File.Open(dataPath, FileMode.Create);
            byte[] bytes = new UTF8Encoding().GetBytes(JsonUtility.ToJson(bestrecords));
            
            fileStream.Write(bytes, 0, bytes.Length);
            //每次读取文件后都要记得关闭文件
            fileStream.Close();
        }
        public static BestScoreRecord CreateDefault()
        {
            BestScoreRecord defaultRecord = new BestScoreRecord();
            Sortedrecord.Capacity = BestTimeRecordSize;
            defaultRecord.records = new List<TrackRecord>();
            return defaultRecord;
        }

        public static BestScoreRecord getBestScoreRecord()
        {
            BestScoreRecord br = Load();
            Sortedrecord = new SortedList(new IntSort());
            foreach (TrackRecord record in br.records)
            {
                Sortedrecord.Add(record.score, record);
            }
            return br;
        }

        public static BestScoreRecord Load()
        {
            string folderPath = Path.Combine(Application.persistentDataPath, k_FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dataPath = Path.Combine(folderPath, k_FileName + k_FileExtension);

            if (!File.Exists(dataPath))
            {
                return CreateDefault();
            }

            using (FileStream fileStream = File.Open(dataPath, FileMode.Open))
            {
                if (fileStream.Length == 0)
                    return CreateDefault();

                try
                {
                    byte[] bytes = new byte[10000];
                    fileStream.Read(bytes, 0, bytes.Length);
                    //将读取到的二进制转换成字符串
                    string s = new UTF8Encoding().GetString(bytes);
                    BestScoreRecord loadedRecord = JsonUtility.FromJson<BestScoreRecord>(s);

                    //BestScoreRecord loadedRecord = binaryFormatter.Deserialize(fileStream) as BestScoreRecord;

                    if (loadedRecord == null)
                        return CreateDefault();
                    return loadedRecord;
                }
                catch (Exception)
                {
                    return CreateDefault();
                }
            }
        }

    }

    [Serializable]
    public class BestCoinRecord
    {
        public List<TrackRecord> records;
        public static SortedList Sortedrecord = new SortedList(new IntSort());
        public static int BestTimeRecordSize = 10;


        const string k_FolderName = "BinaryTrackRecordData";
        const string k_FileName = "BestCoinRecordData";
        const string k_FileExtension = ".json";

        public static void Add(TrackRecord record)
        {
            if (Sortedrecord.Count < BestTimeRecordSize)
            {
                Sortedrecord.Add(record.coinCount, record);
            }
            else
            {
                Sortedrecord.RemoveAt(0);
                
                Sortedrecord.Add(record.coinCount, record);
                //Debug.Log(JsonUtility.ToJson(Sortedrecord.Values));
            }

        }

        public static void Save(BestCoinRecord bestrecords)
        {
            bestrecords.records = new List<TrackRecord>();
            foreach (TrackRecord sr in Sortedrecord.Values)
            {
                bestrecords.records = new List<TrackRecord>();
                bestrecords.records.Add(sr);
            }
            string folderPath = Path.Combine(Application.persistentDataPath, k_FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dataPath = Path.Combine(folderPath, k_FileName + k_FileExtension);
            
            
            if (File.Exists(dataPath))
            {
                Debug.Log("delete");
                File.Delete(dataPath);
            }
            //using (FileStream fileStream = File.Open(dataPath, FileMode.OpenOrCreate))
            //{
            //    binaryFormatter.Serialize(fileStream, bestrecords);
            //}
            FileStream fileStream = File.Open(dataPath, FileMode.Create);
            byte[] bytes = new UTF8Encoding().GetBytes(JsonUtility.ToJson(bestrecords));
            
            fileStream.Write(bytes, 0, bytes.Length);
            //每次读取文件后都要记得关闭文件
            fileStream.Close();
        }
        public static BestCoinRecord CreateDefault()
        {
            BestCoinRecord defaultRecord = new BestCoinRecord();
            Sortedrecord.Capacity = BestTimeRecordSize;
            defaultRecord.records = new List<TrackRecord>();
            return defaultRecord;
        }

        public static BestCoinRecord getBestCoinRecord()
        {
            BestCoinRecord br = Load();
            Sortedrecord = new SortedList(new IntSort());
            foreach (TrackRecord record in br.records)
            {
                Sortedrecord.Add(record.coinCount, record);
            }
            return br;
        }

        public static BestCoinRecord Load()
        {
            string folderPath = Path.Combine(Application.persistentDataPath, k_FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string dataPath = Path.Combine(folderPath, k_FileName + k_FileExtension);
            //Debug.Log(dataPath);
            if (!File.Exists(dataPath))
            {
                return CreateDefault();
            }

            using (FileStream fileStream = File.Open(dataPath, FileMode.Open))
            {
                if (fileStream.Length == 0)
                    return CreateDefault();

                try
                {
                    //BestCoinRecord loadedRecord = binaryFormatter.Deserialize(fileStream) as BestCoinRecord;
                    byte[] bytes = new byte[10000];
                    fileStream.Read(bytes, 0, bytes.Length);
                    //将读取到的二进制转换成字符串
                    string s = new UTF8Encoding().GetString(bytes);
                    BestCoinRecord loadedRecord = JsonUtility.FromJson<BestCoinRecord>(s);


                    if (loadedRecord == null)
                        return CreateDefault();
                    return loadedRecord;
                }
                catch (Exception)
                {
                    return CreateDefault();
                }
            }
        }

    }


}