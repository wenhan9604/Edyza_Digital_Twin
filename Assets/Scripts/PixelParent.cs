using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelParent : MonoBehaviour
{
    private float HeightChecker;

    public delegate void PixelParentHeight();
    public static event PixelParentHeight OnPixelParentHeightChange;
    
    void Start()
    {
        HeightChecker = gameObject.transform.position.y;
    }

    void Update()
    {
        if (gameObject.transform.position.y != HeightChecker && OnPixelParentHeightChange!=null)
        {
            Debug.Log("Pixel Height is Changing");
            OnPixelParentHeightChange();
            HeightChecker = gameObject.transform.position.y;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangePixelParentHeight(-0.1f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangePixelParentHeight(0.1f);
        }
    }

    public void ChangePixelParentHeight (float delta)
    {
        float newHeight;
        newHeight = gameObject.transform.position.y + delta;
        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}
