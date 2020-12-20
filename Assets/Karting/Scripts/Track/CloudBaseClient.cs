using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.unity.cloudbase;
using Newtonsoft.Json;
using UnityEngine;

namespace KartGame.Track
{
    // <summary>
    // CloudBase Client can upload TrackRecord and UserInfo to Tencent cloud server
    // </summary>
    [Serializable]
    public class CloudBaseClient
    {
        private static CloudBaseApp _app = CloudBaseApp.Tcb("59eb4700a3c34", 3000);
        private static Dictionary<string, dynamic> _params;
        private static string _name;
        private static string _mgobeId;

        // Unity CloudBase Client
        async public static void Init(string name, string mgobeId)
        {
            AuthState state = await _app.Auth.GetAuthStateAsync();
            if (state == null)
            {
                // 匿名登录
                state = await _app.Auth.SignInAnonymouslyAsync();
            }
            _name = name;
            _mgobeId = mgobeId;
        }

        //async public static Task<TrackRecorddata> GetHistoryRecord()
        //{
        //    _params = new Dictionary<string, dynamic> { { "name", _name }, { "mgobeId", _mgobeId } };
        //    FunctionResponse res = await _app.Function.CallFunctionAsync("getHistoryRecord", _params);
        //    var dat = JsonConvert.SerializeObject(res.data);
        //    return JsonConvert.DeserializeObject<TrackRecorddata>(dat);
        //}

        //async public static Task<string> GetHistoryRecordJson()
        //{
        //    _params = new Dictionary<string, dynamic> { { "name", _name }, { "mgobeId", _mgobeId } };
        //    FunctionResponse res = await _app.Function.CallFunctionAsync("getHistoryRecord", _params);
        //    var dat = JsonConvert.SerializeObject(res.data);
        //    return dat;
        //}

        //async public static void UploadRecord(TrackRecorddata record)
        //{
        //    _params = new Dictionary<string, dynamic> { { "name", _name },
        //        { "mgobeId", _mgobeId },
        //        { "bestLapTime", record.bestLapTime },
        //        { "bestRaceTime", record.bestRaceTime }
        //    };
        //    FunctionResponse re = await _app.Function.CallFunctionAsync("uploadTrackRecord", _params);
        //    return;
        //}
    }

}