using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkService 
{
    private const string SensorLayoutURL = "http://legacy.edyza.net/php/get_file.php?file_name=/uploads/hello_edyza_107_3dlayout.json";

    private IEnumerator CallAPI(string url, Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            callback(www.downloadHandler.text);
        }
    }

    public IEnumerator GetSensorLayoutJson (Action<string> callback)
    {
        return CallAPI(SensorLayoutURL, callback);
    }

    public IEnumerator GetSensorTempJson (int company_ID, Action<string> callback)
    {
        DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        int cur_time = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
        Debug.Log("current time for Epoch" + cur_time);
        int start_time = cur_time - 1800; //1800sec = 30 mins, duration = 30mins til now
        string parameter = "air_temperature";
        int count = 60;

        string SensorTempURL = "https://timescale.edyza.net/get_multivariate_data?end_tm=" + cur_time + "&start_tm=" + start_time + "&parameter=" + parameter + "&company_id=" + company_ID + "&count=60";
        //string SensorTempURL = "https://timescale.edyza.net/get_multivariate_data?end_tm=1585999800&start_tm=1585998000&parameter=air_temperature&company_id=107&count=60";
        //current time , 30 mins apart , 60 counts = 30 seconds interval 

        return CallAPI(SensorTempURL, callback);
    }
}
