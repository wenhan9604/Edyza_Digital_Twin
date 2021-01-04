using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    //SwitchView between player view and Birds Eye View
    [SerializeField] private GameObject PlayerView;
    private Vector3 lastPlayerPos;
    private bool isPlayerView = true;

    //SetPixel's Collder 
    public delegate void OnSwitchtoPlayerView (bool input);
    public static event OnSwitchtoPlayerView onSwitchtoPlayerView;

    //ChangeHeatMapHeight
    [SerializeField] private GameObject pixelParent;
    [SerializeField] private TextMeshProUGUI heatmapHeightText;

    //Pixel Canvas settings
    [SerializeField] private GameObject pixelCanvas;
    [SerializeField] private TextMeshProUGUI pixelPosText;
    [SerializeField] private TextMeshProUGUI pixelTempText;  
    [SerializeField] private float zOffSet;  
    [SerializeField] private float xOffSet;  

    //reason why we using a switch function is because we want this method to be called with a button click.
    // with same button, its like switching a switch on/off with isPlayerView
    // SwitchView(true) = player view
    // SwitchView(false) = BirdEyeView
    public void SwitchView( ) 
    {
        if(isPlayerView)  //change view from Player to BirdEyeView
        {
            //Set Canvas
            pixelCanvas.SetActive(true);
            //Set Pixel Collider
            onSwitchtoPlayerView(true);
            //Change Player Pose
            lastPlayerPos = PlayerView.transform.position;
            PlayerView.transform.position = new Vector3 (10,24,5);
            PlayerView.transform.rotation = Quaternion.Euler(90,0,0);
            PlayerView.GetComponent<PlayerMovement>().enabled = false;
            //switch will be turned to BirdEyeView 
            isPlayerView = false;
        }
        else  //Change view from BirdEyeView to Player
        {
            //Set Canvas
            pixelCanvas.SetActive(false);
            //Set Pixel Collider
            onSwitchtoPlayerView(false);
            //Change Player Pose
            PlayerView.transform.position = lastPlayerPos;
            PlayerView.transform.rotation = Quaternion.Euler(0,90,0);
            PlayerView.GetComponent<PlayerMovement>().enabled = true;
            //switch will be turned to Playerview
            isPlayerView = true;
        } 
    }




    public void ChangeHeatMapHeight (float input)
    {
        pixelParent.transform.position = new Vector3 (pixelParent.transform.position.x, input , pixelParent.transform.position.z);
        heatmapHeightText.text = input.ToString("F2");
    }

    void MovePixelCanvas(GameObject pixel)
    {
        //move pixel canvas' x & z position,z position will need an offset off the pixel position 
        //get pixel's temp and original position
        //display them in canvas text 
        Vector3 pixelPosition = pixel.transform.position;
        float pixelTemp = pixel.GetComponent<Pixel>().Temp;

        pixelPosText.text = "Pos: " + pixelPosition.ToString();
        pixelTempText.text = "Temp: " + pixelTemp.ToString("F2");
        pixelCanvas.transform.position = new Vector3 (pixelPosition.x + xOffSet,pixelCanvas.transform.position.y,pixelPosition.z + zOffSet);
    
    }

    void Awake()
    {
        PlayerRayCast.onPixelHit += MovePixelCanvas;
        heatmapHeightText.text = pixelParent.transform.position.y.ToString("F2");
        pixelPosText = pixelCanvas.transform.Find("PixelPos Text").GetComponent<TextMeshProUGUI>();
        pixelTempText = pixelCanvas.transform.Find("PixelTemp Text").GetComponent<TextMeshProUGUI>();
    }

    void OnDisable()
    {
        PlayerRayCast.onPixelHit -= MovePixelCanvas;
    }
}
