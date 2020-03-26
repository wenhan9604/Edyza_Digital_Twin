/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;


public class jsonParser : MonoBehaviour
{
    [SerializeField] GameObject EVSensorPrefab;
    [SerializeField] GameObject CSensorPrefab;
    private GameObject sensorInstance;

    public static List<GameObject> EVList = new List<GameObject>();
    public static List<GameObject> CanopyList = new List<GameObject>();
    public static List<GameObject> OthersList = new List<GameObject>();
    private GameObject EVSensorsParent;
    private GameObject CanopySensorsParent;
    private GameObject OtherSensorsParent;

    void Awake()
    {
        EVSensorsParent = new GameObject("EVSensors");
        CanopySensorsParent = new GameObject("CanopySensors");
        OtherSensorsParent = new GameObject("OtherSensors");
        StartCoroutine(GetHTTP(ParseJson));    
    }
    public void ParseJson(string jsonString)
    {
        Items itemsinJson;
        itemsinJson = Items.CreateFromJSON(jsonString);

        foreach (Sensor sensor in itemsinJson.items)
        {
            if (sensor.item_name == "Edyza sensor")
            {
                if (sensor.sensor_name.EndsWith("C"))
                {
                    sensorInstance = Instantiate(CSensorPrefab) as GameObject;
                    sensorInstance.tag = "CanopySensor";
                    CanopyList.Add(sensorInstance);
                }
                else if (sensor.sensor_name.EndsWith("E"))
                {
                    sensorInstance = Instantiate(EVSensorPrefab) as GameObject;
                    sensorInstance.tag = "EVSensor";
                    EVList.Add(sensorInstance);
                }
                else
                {
                    //sensorInstance.tag = "Untagged"; //why do i even need to do this?
                    sensorInstance.tag = "OtherSensor";
                    OthersList.Add(sensorInstance);
                }
                Vector3 position = convertPosToMetres(sensor.xpos, sensor.ypos, sensor.zpos);
                sensorInstance.transform.position = position;
                sensorInstance.name = sensor.sensor_name;
            }

        }
        SetObjectsAsChild(EVSensorsParent, EVList);
        SetObjectsAsChild(CanopySensorsParent, CanopyList);
        SetObjectsAsChild(OtherSensorsParent, OthersList);
        SetEVSensorsActiveOnly();
    }

    public static IEnumerator GetHTTP( Action<string> HTTPFile)
    {
        UnityWebRequest www = UnityWebRequest.Get("http://legacy.edyza.net/php/get_file.php?file_name=/uploads/hello_edyza_107_3dlayout.json");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            HTTPFile(www.downloadHandler.text);
        }
    }
    private Vector3 convertPosToMetres(float input, float input2, float input3)
    {
        input *= 0.01f;
        input2 *= 0.01f;
        input3 *= 0.01f;
        return new Vector3(input, input2, input3);
    }
    void SetEVSensorsActiveOnly()
    {
        CanopySensorsParent.SetActive(false);
        OtherSensorsParent.SetActive(false);
    }
    void SetObjectsAsChild(GameObject parent, List<GameObject> list)
    {
        foreach (GameObject sensor in list)
        {
            //Debug.Log("EV Sensor position: " + GameObject.FindWithTag("EVSensor").transform.position.ToString("F4"));
            sensor.transform.parent = parent.transform;
        }

    }
}

[System.Serializable]
 public class Items
{
    public Sensor[] items;
    public static Items CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Items>(jsonString);
    }
}

[System.Serializable]
public class Sensor
    {
        public string item_name;
        public float xpos;
        public float ypos;
        public float zpos;
        public string sensor_name;
    }*/







