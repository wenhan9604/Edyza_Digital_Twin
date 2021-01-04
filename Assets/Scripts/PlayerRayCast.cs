using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRayCast : MonoBehaviour
{
    [SerializeField] private Image imgGaze;

    [SerializeField] private float totalTime = 2f;
    private float gvrTimer;
    private bool gvrStatus;
    public bool GvrStatus
    {
        get
        {
            return gvrStatus;
        }
        set
        {
            if(value == false)
            {
                gvrTimer = 0;
                imgGaze.fillAmount = 0;
            }
            gvrStatus = value; 
        }
    }

    [SerializeField] private int distanceOfRay = 30;
    private RaycastHit _hit;
    private Camera playerCamera;

    //Settings for PixelCanvas
    public delegate void OnPixelHit(GameObject pixel);
    public static event OnPixelHit onPixelHit;

    void Start()
    {
        playerCamera = this.transform.GetComponentInChildren(typeof(Camera)) as Camera;
    }

    // Update is called once per frame
    void Update()
    {
        if(gvrStatus)
        {
            gvrTimer += Time.deltaTime;
            imgGaze.fillAmount = gvrTimer/totalTime;
        }

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));

        if(Physics.Raycast(ray,out _hit, distanceOfRay))
        {
            /*SwitchView switchViewScript = _hit.transform.gameObject.GetComponent<SwitchView>(); 
        
            if(imgGaze.fillAmount == 1 && switchViewScript != null)
            {
                switchViewScript.SwitchPlayerView();
            }*/
        }

        //Raycast to detect Pixels
        if(Physics.Raycast(ray,out _hit,distanceOfRay))
        {
            Pixel pixelScript = _hit.transform.gameObject.GetComponent<Pixel>();

            if(pixelScript != null)
            {
                onPixelHit(_hit.transform.gameObject);
                Debug.Log("Broadcasting Pixel from Raycast");
            }
        }

    }
}
