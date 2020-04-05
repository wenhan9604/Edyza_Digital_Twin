using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] private Color coolColor = new Vector4(0, 0.8f, 0.8f, 0.3f);
    [SerializeField] private Color warmColor = new Vector4(0.8f, 0, 0, 0.3f);
    [SerializeField] private float MaxTemp;
    [SerializeField] private float MinTemp; //have to set in inspector
    [SerializeField] private float Temp;
    private GameObject[] EVArray;

    void Awake()
    {
        SensorManager.OnSensorTempUpdated += OnSensorLayoutAndTempUpdated;
        EVSensor.OnChangeEVSensorTemp += OnChangeEVSensorTemp;
    }

    void OnDestroy()
    {
        SensorManager.OnSensorTempUpdated -= OnSensorLayoutAndTempUpdated;
        EVSensor.OnChangeEVSensorTemp -= OnChangeEVSensorTemp;

    }

    void Update()
    {
        //Temp = TempCal(EVArray);
        //MaxTemp = FindMaxTemp(EVArray);
        //MinTemp = FindMinTemp(EVArray);

        //Debug.Log("Temp of Spot " + Temp);
        //Debug.Log("Max Temp: " + MaxTemp);
        //Debug.Log("Min Temp: " + MinTemp);

        //ColorChanger(Temp);
    }

    void OnSensorLayoutAndTempUpdated()
    {
        EVArray = GameObject.FindGameObjectsWithTag("EVSensor");
        //Debug.Log(EVArray.Length);
    }


    private float TempCal(GameObject[] EVArray) 
    {
        float numerator =0;
        float denom =0;

            foreach (GameObject sensor in EVArray)
            {
                EVSensor EVScript = sensor.GetComponent<EVSensor>();
                float dist = Vector3.Distance(sensor.transform.position, transform.position); // Optimize: will need to optimize this once temp comes from JSON file
                numerator += EVScript.Temp / dist;
                denom += 1 / dist;
            }
        return numerator / denom;
    }

    private float FindMaxTemp (GameObject[] EVArray)
    {
        List<float> MaxTempFromEVSensors = new List<float>();

        foreach (GameObject sensor in EVArray)
        {
            EVSensor EVScript = sensor.GetComponent<EVSensor>();
            MaxTempFromEVSensors.Add(EVScript.MaxTemp);
        }
        return Mathf.Max(MaxTempFromEVSensors.ToArray());
    }

    private float FindMinTemp(GameObject[] EVArray)
    {
        List<float> MinTempFromEVSensors = new List<float>();

        foreach (GameObject sensor in EVArray)
        {
            EVSensor EVScript = sensor.GetComponent<EVSensor>();
            MinTempFromEVSensors.Add(EVScript.MinTemp);
        }
        return Mathf.Min(MinTempFromEVSensors.ToArray());
    }

    void ColorChanger(float Temp)
    {
        var percentage = Mathf.InverseLerp(MinTemp, MaxTemp, Temp);
        GetComponent<MeshRenderer>().material.color = Color.Lerp(coolColor, warmColor, percentage);
        //Debug.Log("Colour %: " + percentage);
    }

    void OnChangeEVSensorTemp ()
    {
        Temp = TempCal(EVArray);
        MaxTemp = FindMaxTemp(EVArray);
        MinTemp = FindMinTemp(EVArray);

        Debug.Log("Temp of Spot " + Temp);
        ColorChanger(Temp);

    }
}
