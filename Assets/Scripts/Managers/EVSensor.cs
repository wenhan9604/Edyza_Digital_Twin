using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVSensor : MonoBehaviour
{
    private Dictionary<int, float> air_temperature;
    public float Temp { get;  set; }
    [SerializeField] private int startTiming;

    void Awake()
    {
        SensorManager.OnSensorTempUpdated += OnSensorLayoutAndTempUpdated;
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

        air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        /*foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName: " + gameObject.name);
        }*/
    }

    void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        Debug.Log(this.gameObject.name + " " + Temp);
    }
}
