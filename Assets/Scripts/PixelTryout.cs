using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelTryout : MonoBehaviour
{
    public GameObject sensor1;
    public GameObject sensor2;
    [SerializeField] private Color coolColor = new Vector4(0, 0.8f, 0, 0.3f);
    [SerializeField] private Color warmColor = new Vector4(0.8f, 0, 0, 0.3f);
    [SerializeField] private float MaxTemp = 80;
    [SerializeField] private float MinTemp = 70;
    float Temp1;
    float Temp2;

    void Start()
    {
        //Sensors sensorScript1 = sensor1.GetComponent<Sensors>();
        //Sensors sensorScript2 = sensor2.GetComponent<Sensors>();
        //Temp1 = sensorScript1.Temp;
        //Temp2 = sensorScript2.Temp;
    }

    void Update ()
    { 
        float Temp = TempCal(sensor1, sensor2, Temp1, Temp2);
        Debug.Log("Temp of Spot " + Temp);
        ColorChanger(Temp);
    }

    float TempCal(GameObject S1, GameObject S2, float T1, float T2)
    {
        float dist1 = Vector3.Distance(S1.transform.position, transform.position);
        float dist2 = Vector3.Distance(S2.transform.position, transform.position);
        float var1 = T1 / dist1;
        float var2 = T2 / dist2;
        return (var1 + var2) / (1 / dist1 + 1 / dist2);
    }

    void ColorChanger(float Temp)
    {
        var percentage = Mathf.InverseLerp(MinTemp, MaxTemp, Temp);
        GetComponent<MeshRenderer>().material.color = Color.Lerp(coolColor, warmColor, percentage);
        Debug.Log("Colour %: " + percentage);
    }
}
