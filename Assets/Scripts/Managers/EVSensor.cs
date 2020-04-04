using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EVSensor : MonoBehaviour
{
    private Dictionary<int, float> air_temperature;
    public float Temp { get; set; }
    private List<int> SetOfTimings;
    private int FirstTiming;
    private int LastTiming;

   
    void Awake()
    {
        SensorManager.OnSensorTempUpdated += OnSensorLayoutAndTempUpdated;
    }

    void OnDestroy()
    {
        SensorManager.OnSensorTempUpdated -= OnSensorLayoutAndTempUpdated;
    }

    private void Update()
    {
        //Debug.Log("Current Temp" + Temp + " " + gameObject.name);
    }

    private void OnSensorLayoutAndTempUpdated()
    {
        Debug.Log("received SensorTemp from OnSensorTempLoaded for Sensor: " + gameObject.name);

        air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        SetOfTimings = new List<int>(air_temperature.Keys);

        if (SetOfTimings.Count != 0) // LP: to set boolean for int variables, set != 0 instead of null
        {
            FirstTiming = SetOfTimings[0];
            LastTiming = SetOfTimings[SetOfTimings.Count - 1];
        }

        Temp = air_temperature[FirstTiming];
        Debug.Log("Temp at First timing: " + Temp + " " + gameObject.name);

        StartCoroutine(StartTimer(FirstTiming));

        /*Debug.Log(FirstTiming + " FirstTiming SensorName: " + gameObject.name);
        Debug.Log(LastTiming + " LastTiming SensorName: " + gameObject.name);
        
        foreach (var items in SetOfTimings)
        {
            Debug.Log(items + " SensorName: " + gameObject.name);
        }
        
        foreach (var items in air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName: " + gameObject.name);
        }*/
    }


    private IEnumerator StartTimer(int counter)
    {
        while ( counter<=LastTiming)
        {
            if (counter == 1582834505)
                ChangeSensorTemp(1582834950);

            Debug.Log("Counter: " + counter + " " + gameObject.name);
            counter += 1;
            yield return new WaitForSeconds(1f);
        }      
    }

    void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        Debug.Log("Temp changed: " + gameObject.name + " " + Temp);
    }
}
