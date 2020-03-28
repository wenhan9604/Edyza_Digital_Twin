using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVSensor : MonoBehaviour
{
    private Dictionary<int, double> air_temperature;
    public double Temp { get; private set; }
    [SerializeField] private int startTiming;

    void Awake()
    {
        SensorManager.OnSensorTempUpdated += OnSensorLayoutAndTempUpdated;
        Debug.Log("SensorName: " + this.gameObject.name);
    }

    void OnDestroy()
    {
        SensorManager.OnSensorTempUpdated -= OnSensorLayoutAndTempUpdated;
    }

    /*private void Start()
    {
        //Debug.Log("SensorName: " + this.gameObject.name);
        Debug.Log("EVsensor Initialized: " + gameObject.name);
        startTiming = Managers.Sensors.EpochTimings[0];
        Debug.Log("EpochTimings.Count: " + startTiming);
        air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName " + gameObject.name);

        }
    }*/
    void Update()
    {
        //Debug.Log("SensorName: " + this.gameObject.name);
        foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value);
        }
        foreach (var items in Managers.Sensors.EpochTimings)
        {
            Debug.Log("Set of Epoch Timings: " + items);
        }
            //Debug.Log(TempTimings[0]);
        //Debug.Log(startTiming);


        /*Debug.Log("Startof Timer " + startTiming);       //Timer
        for (int jj = 0; jj < TempTimings.Count; jj++)
            if (startTiming == TempTimings[jj])
                ChangeSensorTemp(startTiming);

        startTiming += (int)Time.deltaTime;
        */
    }

    
    private void OnSensorLayoutAndTempUpdated()
    {
        Debug.Log("received SensorTemp from OnSensorTempLoaded");
        Debug.Log("SensorName: " + this.gameObject.name);
        //SetSensorTemp(Managers.Sensors.EpochTimings, Managers.Sensors.ListOfSensorTemp);

        startTiming = Managers.Sensors.EpochTimings[0];
        Debug.Log("EpochTimings.Count: " + startTiming);
        air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName " + gameObject.name);
        }
    }

    /*
    void SetSensorTemp(List<int> EpochTimings , List<SensorTemp> ListOfSensorTemp)  // Store EpochTiming as key, Temp as value in Dictionary
    {
        foreach (var timings in EpochTimings) //get Epoch Timings for the sensor
        {
            SetOfEpochTimings.Add(timings);
            //Debug.Log("Set of Epoch Timings: " + timings);
        }

        startTiming = SetOfEpochTimings[0];
        Debug.Log("SetOfEpochTimings.Count: " + SetOfEpochTimings.Count);

        foreach (var SensorTemp in ListOfSensorTemp) // store each Temp(key) with each Timings(value) in Dictionary 
        {
            //Debug.Log("SensorName: " + SensorTemp.sensor_name + "SensorTemp.Count: " + SensorTemp.value.Count);
            if (this.gameObject.name == SensorTemp.sensor_name)
            {
                for (int ii = 0; ii < SetOfEpochTimings.Count; ii++)
                {
                    air_temperature.Add(SetOfEpochTimings[ii], SensorTemp.value[ii]);
                }
            }
            else
                Debug.Log("Sensortemp has not been added to this sensor: " + this.gameObject.name);
        }
    }*/
        void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        Debug.Log(this.gameObject.name + " " + Temp);
    }
}
