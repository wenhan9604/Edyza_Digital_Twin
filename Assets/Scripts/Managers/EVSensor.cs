using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVSensor : MonoBehaviour
{
    private Dictionary<int, double> air_temperature;
    public  double Temp { get; private set; }
    private List<int> SetOfEpochTimings;
    private int startTiming;

    void Awake()
    {
        air_temperature = new Dictionary<int, double>();
        SetOfEpochTimings = new List<int>();
        Messenger.AddListener(GameEvent.INSTANTIATED_SENSORS, OnSensorsInstantiated);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.INSTANTIATED_SENSORS, OnSensorsInstantiated);
    }

    void Update()
    {
        foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value);
        }
        foreach (var items in SetOfEpochTimings)
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

    private void OnSensorsInstantiated()
    {
        SetSensorTemp(Managers.Sensors.EpochTimings, Managers.Sensors.ListOfSensorTemp);
    }

    void SetSensorTemp(List<int> EpochTimings , List<SensorTemp> ListOfSensorTemp)  // Store EpochTiming as key, Temp as value in Dictionary
    {
        foreach (var timings in EpochTimings) //get Epoch Timings for the sensor
        {
            SetOfEpochTimings.Add(timings);
            Debug.Log("Set of Epoch Timings: " + timings);
        }

        startTiming = SetOfEpochTimings[0];
        Debug.Log("SetOfEpochTimings.Count: " + SetOfEpochTimings.Count);

        foreach (var SensorTemp in ListOfSensorTemp) // store each Temp with each Timings
        {
            Debug.Log("SensorName: " + SensorTemp.sensor_name + "SensorTemp.Count: " + SensorTemp.value.Count);
            if (this.gameObject.name == SensorTemp.sensor_name)
            {
                for (int ii = 0; ii < SetOfEpochTimings.Count; ii++)
                {
                    air_temperature.Add(SetOfEpochTimings[ii], SensorTemp.value[ii]);
                }
            }
        }
    }
        void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        Debug.Log(this.gameObject.name + " " + Temp);
    }
}
