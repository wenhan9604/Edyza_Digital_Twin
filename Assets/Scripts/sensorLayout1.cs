/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorLayout : MonoBehaviour
{
    jsonParser jpInstance;
    [SerializeField] GameObject EVSensorPrefab;
    [SerializeField] GameObject CSensorPrefab;
    private GameObject sensorInstance;

    public List<GameObject> EVList;
    public List<GameObject> CanopyList;
    public List<GameObject> OthersList;
    private GameObject EVSensorsParent;
    private GameObject CanopySensorsParent;
    private GameObject OtherSensorsParent;

    private void Awake()
    {
        jpInstance = GetComponent<jsonParser>();
        EVList = new List<GameObject>();
        CanopyList = new List<GameObject>();
        OthersList = new List<GameObject>();
        EVSensorsParent = new GameObject("EVSensors");
        CanopySensorsParent = new GameObject("CanopySensors");
        OtherSensorsParent = new GameObject("OtherSensors");
    }
    void InstantiateSensors()
    {
        foreach (Sensor sensor in jpInstance.itemsinJson.items) 
        {
            if (sensor.item_name == "Edyza sensor")
            {
                if (sensor.sensor_name.EndsWith("C"))
                {
                    sensorInstance = Instantiate(CSensorPrefab) as GameObject;
                    sensorInstance.tag = "CanopySensor";
                    CanopyList.Add(sensorInstance);
                }
                else if (sensor.sensor_name.EndsWith("E"))
                {
                    sensorInstance = Instantiate(EVSensorPrefab) as GameObject;
                    sensorInstance.tag = "EVSensor";
                    EVList.Add(sensorInstance);
                }
                else
                {
                    //sensorInstance.tag = "Untagged"; //why do i even need to do this?
                    sensorInstance.tag = "OtherSensor";
                    OthersList.Add(sensorInstance);
                }
                Vector3 position = convertPosToMetres(sensor.xpos, sensor.ypos, sensor.zpos);
                sensorInstance.transform.position = position;
                sensorInstance.name = sensor.sensor_name;
            }

        }
        SetObjectsAsChild(EVSensorsParent, EVList);
        SetObjectsAsChild(CanopySensorsParent, CanopyList);
        SetObjectsAsChild(OtherSensorsParent, OthersList);

        SetEVSensorsActiveOnly();
    }
    private Vector3 convertPosToMetres(float input, float input2, float input3)
    {
        input *= 0.01f;
        input2 *= 0.01f;
        input3 *= 0.01f;
        return new Vector3(input, input2, input3);
    }

    void SetEVSensorsActiveOnly()
    {
        CanopySensorsParent.SetActive(false);
        OtherSensorsParent.SetActive(false);
    }

    void SetObjectsAsChild(GameObject parent , List<GameObject> list)
    {
        foreach (GameObject sensor in list)
        {
            //Debug.Log("EV Sensor position: " + GameObject.FindWithTag("EVSensor").transform.position.ToString("F4"));
            sensor.transform.parent = parent.transform;
        }

    }
}*/
