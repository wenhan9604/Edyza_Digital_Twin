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
        /*Debug.Log("SensorName: " + this.gameObject.name);
        foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value);
        }
        foreach (var items in Managers.Sensors.EpochTimings)
        {
            Debug.Log("Set of Epoch Timings: " + items);
        }*/
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
        Debug.Log("received SensorTemp from OnSensorTempLoaded for Sensor: " + gameObject.name);
        //Debug.Log("Initiated SensorName: " + this.gameObject.name);

        //air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        /*foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName " + gameObject.name);
        }*/
    }


        void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        Debug.Log(this.gameObject.name + " " + Temp);
    }
}
