using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EVSensor : MonoBehaviour
{
    private Dictionary<int, float> air_temperature;
    private Dictionary<int, float> max_air_temperature;
    private Dictionary<int, float> min_air_temperature;
    private List<int> SetOfTimings;
    private int FirstTiming;
    private int LastTiming;

    public float Temp { get; private set; }
    public float MaxTemp { get; private set; }
    public float MinTemp { get; private set; }

    public delegate void ChangeEVSensorTemp();
    public static event ChangeEVSensorTemp OnChangeEVSensorTemp;

    void Awake()
    {
        SensorManager.OnSensorTempUpdated += OnSensorLayoutAndTempUpdated;
    }

    void OnDestroy()
    {
        SensorManager.OnSensorTempUpdated -= OnSensorLayoutAndTempUpdated;
    }

    private void OnSensorLayoutAndTempUpdated()
    {
        Debug.Log("received SensorTemp from OnSensorTempLoaded for Sensor: " + gameObject.name);

        air_temperature = Managers.Sensors.GetAirTemp(gameObject.name);
        SetOfTimings = new List<int>(air_temperature.Keys);
        max_air_temperature = Managers.Sensors.GetMaxAirTemp(gameObject.name);
        min_air_temperature = Managers.Sensors.GetMinAirTemp(gameObject.name);

        if (SetOfTimings.Count != 0) 
        {
            FirstTiming = SetOfTimings[0];
            LastTiming = SetOfTimings[SetOfTimings.Count - 1];
        }

        StartCoroutine(StartTimer(FirstTiming));

        /*Debug.Log(FirstTiming + " FirstTiming SensorName: " + gameObject.name);
        Debug.Log(LastTiming + " LastTiming SensorName: " + gameObject.name);
        
        foreach (var items in SetOfTimings)
        {
            Debug.Log(items + " SensorName: " + gameObject.name);
        }
        
        foreach (var items in min_air_temperature)
        {
            Debug.Log(items.Key + " " + items.Value + " SensorName: " + gameObject.name);
        }*/
    }


    private IEnumerator StartTimer(int counter)
    {
        while (counter<=LastTiming)
        {
            for (int ii = 0; ii < SetOfTimings.Count; ii++)
            {
                if (counter == SetOfTimings[ii])
                    ChangeSensorTemp(SetOfTimings[ii]);
            }

            //Debug.Log("Counter: " + counter + " " + gameObject.name);
            counter += 1;
            yield return new WaitForSeconds(1f);
        }      
    }

    void ChangeSensorTemp (int key)
    {
        Temp = air_temperature[key];
        MaxTemp = max_air_temperature[key];
        MinTemp = min_air_temperature[key];

        Debug.Log("broadcasting EVSensorTemp from ChangeSensorTemp,EVSensor");
        if (OnChangeEVSensorTemp != null)
        {
            OnChangeEVSensorTemp();
        }

        //Debug.Log("Temp changed: " + gameObject.name + " " + Temp);
        //Debug.Log("MaxTemp changed: " + gameObject.name + " " + MaxTemp);
        //Debug.Log("MinTemp changed: " + gameObject.name + " " + MinTemp
    }
}
