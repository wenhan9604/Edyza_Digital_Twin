using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Window : MonoBehaviour
{
    [SerializeField] GameObject PixelParentHeightDisplay;
    private TextMeshProUGUI uiText;

    [SerializeField] GameObject HeatMap;
    private float PixelParentHeight;

    void Awake()
    {
        PixelParent.OnPixelParentHeightChange += ChangePixelParentHeightDisplay;
    }

    void OnDestroy()
    {
        PixelParent.OnPixelParentHeightChange -= ChangePixelParentHeightDisplay;
    }

    void Start()
    {
        PixelParentHeight = HeatMap.transform.position.y;
        uiText = PixelParentHeightDisplay.GetComponent<TextMeshProUGUI>();
        uiText.text = "HeatMap Height: " + PixelParentHeight;
    }

    private void ChangePixelParentHeightDisplay()
    {
        PixelParentHeight = HeatMap.transform.position.y;
        uiText.text = "HeatMap Height: " + PixelParentHeight;
    }
}
