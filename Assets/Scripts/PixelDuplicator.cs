using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelDuplicator : MonoBehaviour
{
    [SerializeField] private GameObject PixelPrefab;
    [SerializeField] private GameObject SizeOfRoom;
    [SerializeField] private GameObject FirstPixel;
    private GameObject PixelInstance;
    private List<GameObject> PixelList;
    private GameObject PixelParent;
    private float offsetX; 
    private float offsetZ;

    void Start()
    {
        PixelParent = new GameObject("Pixels");
        PixelList = new List<GameObject>();
        DuplicateByGrid();
    }
    void DuplicateByGrid ()
    {
        offsetX = GetBounds(PixelPrefab).x;
        offsetZ = GetBounds(PixelPrefab).z;
        float NumbOfPixelX = GetBounds(SizeOfRoom).x / offsetX;
        float NumbOfPixelZ = GetBounds(SizeOfRoom).z / offsetZ;


        for (int ii = 0; ii < NumbOfPixelX; ii++)
        {
            for (int jj = 0; jj < NumbOfPixelZ; jj++)
            {
                PixelInstance = Instantiate(PixelPrefab) as GameObject;
                float posX = (offsetX * ii) + FirstPixel.transform.position.x;
                float posZ = (offsetZ * jj) + FirstPixel.transform.position.z;
                PixelInstance.transform.position = new Vector3(posX, FirstPixel.transform.position.y, posZ);
                PixelList.Add(PixelInstance);
            }
        }
        Debug.Log("PixelList Number now : " + PixelList.Count);
        foreach (GameObject Pixel in PixelList)
        {
            Pixel.transform.parent = PixelParent.transform;
        }
    }

    Vector3 GetBounds (GameObject item)
    {
        if (item.name == "Pixels") //do this because of prefabs
        {
            Mesh itemMesh = item.GetComponent<MeshFilter>().sharedMesh;
            Bounds itemBounds = itemMesh.bounds;
            float boundsX = item.transform.localScale.x * itemBounds.size.x;
            float boundsY = item.transform.localScale.y * itemBounds.size.y;
            float boundsZ = item.transform.localScale.z * itemBounds.size.z;
            Vector3 size = new Vector3(boundsX, boundsY, boundsZ);
            return size;
        }
        else
        {
            Mesh itemMesh = item.GetComponent<MeshFilter>().mesh;
            Bounds itemBounds = itemMesh.bounds;
            float boundsX = item.transform.localScale.x * itemBounds.size.x;
            float boundsY = item.transform.localScale.y * itemBounds.size.y;
            float boundsZ = item.transform.localScale.z * itemBounds.size.z;
            Vector3 size = new Vector3(boundsX, boundsY, boundsZ);
            return size;
        }
        
    }
}
