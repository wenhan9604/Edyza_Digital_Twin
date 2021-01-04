using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    public Vector3 dirVector;

    void Update()
    {
        this.transform.LookAt(targetObject.transform,dirVector);
    }
}
