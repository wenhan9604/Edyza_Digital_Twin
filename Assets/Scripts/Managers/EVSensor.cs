using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVSensor : MonoBehaviour
{
    private Dictionary<int, float> air_temperature;
    public float Temp { get; private set; }
    [SerializeField] private int startTiming;

    void Awake()
    {
        SensorManager.OnSensorTempUpdated += OnSensorLayoutAndTempUpdated;
        air_temperature = new Dictionary<int, float>();
    }

    void OnDestroy()
    {
        SensorManager.OnSensorTempUpdated -= OnSensorLayoutAndTempUpdated;
    }

    void Update()
    {
        /*Debug.Log("Startof Timer " + startTiming);       //Timer
        for (int jj = 0; jj < TempTimings.Count; jj++)
            if (startTiming == TempTimings[jj])
                ChangeSensorTemp(startTiming);

        startTiming += (int)Time.deltaTime;
        */
    }

    
    private void OnSensorLayoutAndTempUpdated()
    {
        Debug.Log("received SensorTemp from OnSensorTempLoaded for Sensor: " + gameObject.name);
        //Debug.Log("Initiated SensorName: " + this.gameObject.name);

        foreach (var SensorTemp in Managers.Sensors.ListOfSensorTemp)
        {
            if (gameObject.name == SensorTemp.sensor_name)
            {
                Temp = SensorTemp.value[0];
                Debug.Log("Temp of Sensor: " + gameObject.name + " " + Temp);
            }
        }
        //GetAirTemp(Managers.Sensors.EpochTimings, Managers.Sensors.ListOfSensorTemp);
        //air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        /*foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName " + gameObject.name);
        }*/
    }

    private void GetAirTemp(List<int> EpochTimings, List<SensorTemp> ListOfSensorTemp)
    { 

        foreach (var SensorTemp in ListOfSensorTemp) // store each Temp(key) with each Timings(value) in Dictionary 
        {
            if (gameObject.name == SensorTemp.sensor_name)
            {
                Debug.Log("airtemp added for sensor: " + gameObject.name);
                for (int ii = 0; ii < EpochTimings.Count; ii++)
                {
                    air_temperature.Add(EpochTimings[ii], SensorTemp.value[ii]);
                    Debug.Log("SensorTemp.value added: " + SensorTemp.value[ii]);
                }
                /*foreach (var items in air_temperature)
                {
                    Debug.Log(items.Key + " " + items.Value + " SensorName: " + GameObjectName);
                }*/
            }
            else
                Debug.Log("Game Object Name: " + gameObject.name + "didnt match with name: " + SensorTemp.sensor_name);
        }
    }

    void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        Debug.Log(this.gameObject.name + " " + Temp);
    }
}
