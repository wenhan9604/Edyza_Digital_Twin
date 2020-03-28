using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;

public class SensorManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    private NetworkService _network;

    public delegate void SensorLayoutUpdated();
    public static event SensorLayoutUpdated OnSensorLayoutUpdated;
    public delegate void SensorTempUpdated();
    public static event SensorTempUpdated OnSensorTempUpdated;

    public Items itemsInJson { get; private set; }
    public List<SensorTemp> ListOfSensorTemp { get; private set; }
    public List<int> EpochTimings { get; private set; }
    public Dictionary<int, double> air_temperature;



    public void Startup(NetworkService service)
    {
        Debug.Log("Sensor Manager starting...");
        _network = service;
        //StartCoroutine(_network.GetSensorTempJson(107, OnSensorTempLoaded));
        StartCoroutine(_network.GetSensorLayoutJson(OnSensorLayoutLoaded));

        status = ManagerStatus.Initializing;
    }

    public void OnSensorLayoutLoaded(string data)
    {
        itemsInJson = JsonUtility.FromJson<Items>(data);
        if (OnSensorLayoutUpdated != null)
        {
            OnSensorLayoutUpdated();
        }
        Debug.Log("broadcasting Sensor Layout from OnSensorLayoutLoaded");

        StartCoroutine(_network.GetSensorTempJson(107, OnSensorTempLoaded));
    }

    public void OnSensorTempLoaded(string data)
    {
        JSONNode N = JSON.Parse(data);
        ListOfSensorTemp = new List<SensorTemp>();
        EpochTimings = new List<int>();

        foreach (JSONNode item_value in N["time"])
        {
            EpochTimings.Add(item_value);
        }

        foreach (JSONNode item in N["data"])
        {
            SensorTemp Sensor = new SensorTemp();
            Sensor.min = new List<double>();
            Sensor.max = new List<double>();
            Sensor.count = new List<double>();
            Sensor.value = new List<double>();

            Sensor.sensor_name = item["sensor"][0].Value;
            Sensor.parameter = item["parameter"][0].Value;
            //Debug.Log("Sensor Parsed: " + Sensor.sensor_name);

            foreach (JSONNode item_value in item["min"])
            {
                Sensor.min.Add(item_value.AsDouble);
            }
            foreach (JSONNode item_value in item["max"])
            {
                Sensor.max.Add(item_value.AsDouble);
            }
            foreach (JSONNode item_value in item["count"])
            {
                Sensor.count.Add(item_value.AsDouble);
            }
            foreach (JSONNode item_value in item["value"])
            {
                Sensor.value.Add(item_value.AsDouble);
            }

            ListOfSensorTemp.Add(Sensor);
        }

        if (OnSensorTempUpdated != null)
        { 
            OnSensorTempUpdated();
        }
        Debug.Log("broadcasting SensorTemp from OnSensorTempLoaded");

        status = ManagerStatus.Started;
    }

    public Dictionary<int, double> GetAirTemp(string GameObjectName)
    {
        //Dictionary<int, double> air_temperature;
        air_temperature = new Dictionary<int, double>();

        foreach (var SensorTemp in ListOfSensorTemp) // store each Temp(key) with each Timings(value) in Dictionary 
        {
            if (GameObjectName == SensorTemp.sensor_name)
            {
                //Debug.Log("airtemp added for sensor: " + GameObjectName);
                for (int ii = 0; ii < EpochTimings.Count; ii++)
                {
                    air_temperature.Add(EpochTimings[ii], SensorTemp.value[ii]);
                    Debug.Log("airtemp added for sensor: " + GameObjectName);
                    Debug.Log("SensorTemp.value: " + SensorTemp.value[ii]); ;
                }
                foreach (var items in air_temperature)
                {
                    Debug.Log(items.Key + " " + items.Value + " SensorName " + GameObjectName);
                }
            }
            else
                Debug.Log("Game Object Name: " + GameObjectName + "didnt match with name: " + SensorTemp.sensor_name);
        }
        return air_temperature;
    }
}

public class SensorTemp //need to put out of main class so that can call in Script:EVSensor using Foreach (SensorTemp varaible)
{
    public List<double> min { get; set; }
    public List<double> max { get; set; }
    public List<double> count { get; set; }
    public List<double> value { get; set; }
    public string parameter { get; set; }
    public string sensor_name { get; set; } // why cant private set?
}

[Serializable]
public class Items
{
    public Sensor[] items;
}

[Serializable]
public class Sensor
{
    public string item_name;
    public float xpos;
    public float ypos;
    public float zpos;
    public string sensor_name;
}



