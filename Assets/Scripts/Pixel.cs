using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] private Color coolColor = new Vector4(0, 0.8f, 0.8f, 0.3f);
    [SerializeField] private Color warmColor = new Vector4(0.8f, 0, 0, 0.3f);
    [SerializeField] private float MaxTemp = 100f;
    [SerializeField] private float MinTemp = 0f;
    [SerializeField] private float Temp;
    private GameObject[] EVArray;

    void Start()
    {
        EVArray = GameObject.FindGameObjectsWithTag("EVSensor");
        //Debug.Log(EVArray.Length);
        
    }

    void Update()
    {
        /*Temp = TempCal(EVArray);
        //Debug.Log("Temp of Spot " + Temp);
        ColorChanger(Temp);*/
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

    void ColorChanger(float Temp)
    {
        var percentage = Mathf.InverseLerp(MinTemp, MaxTemp, Temp);
        GetComponent<MeshRenderer>().material.color = Color.Lerp(coolColor, warmColor, percentage);
        //Debug.Log("Colour %: " + percentage);
    }
}
