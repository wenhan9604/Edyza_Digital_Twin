using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] private Color RedColor = new Vector4(1f, 0, 0, 0.3f);
    [SerializeField] private Color YellowColor = new Vector4(1f, 1f, 0, 0.3f);
    [SerializeField] private Color GreenColor = new Vector4(0, 1f, 0, 0.3f);
    [SerializeField] private Color CyanColor = new Vector4(0, 1f, 1f, 0.3f);
    [SerializeField] private Color BlueColor = new Vector4(0, 0, 0.8f, 0.3f);

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

    void OnSensorLayoutAndTempUpdated()
    {
        EVArray = GameObject.FindGameObjectsWithTag("EVSensor");
        //Debug.Log(EVArray.Length);
    }

    void OnChangeEVSensorTemp()
    {
        Temp = TempCal(EVArray);
        MaxTemp = FindMaxTemp(EVArray);
        MinTemp = FindMinTemp(EVArray);
        Debug.Log("Temp of Spot " + Temp);

        //Problem with Min Temp during Startup
        //Debug.Log("MaxTemp is: " + MaxTemp);
        //Debug.Log("MinTemp Is: " + MinTemp);

        ColorChanger(Temp);
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
        float intervals = (MaxTemp - MinTemp) / 4;
        float UpperPoint = intervals * 3 + MinTemp;
        float MidPoint = intervals*2  + MinTemp;
        float LowerPoint = intervals + MinTemp;

        //Debug.Log("This is UpperPoint: " + UpperPoint);
        //Debug.Log("This is MidPoint: " + MidPoint);
        //Debug.Log("This is LowerPoint: " + LowerPoint);

        if (Temp<=MaxTemp && Temp > UpperPoint )
        {
            var percentage = Mathf.InverseLerp(UpperPoint, MaxTemp, Temp);
            GetComponent<MeshRenderer>().material.color = Color.Lerp(YellowColor, RedColor, percentage);
        }

        else if (Temp <= UpperPoint && Temp > MidPoint)
        {
            var percentage = Mathf.InverseLerp(MidPoint, UpperPoint, Temp);
            GetComponent<MeshRenderer>().material.color = Color.Lerp(GreenColor, YellowColor, percentage);
        }

        else if (Temp <= MidPoint && Temp > LowerPoint)
        {
            var percentage = Mathf.InverseLerp(LowerPoint, MidPoint, Temp);
            GetComponent<MeshRenderer>().material.color = Color.Lerp(CyanColor, GreenColor, percentage);
        }

        else if (Temp <= LowerPoint && Temp > MinTemp)
        {
            var percentage = Mathf.InverseLerp(MinTemp, LowerPoint, Temp);
            GetComponent<MeshRenderer>().material.color = Color.Lerp(BlueColor, CyanColor, percentage);
        }
    }
}
