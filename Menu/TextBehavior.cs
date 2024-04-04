using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBehavior : MonoBehaviour
{
    [SerializeField] private Image buttonImg;
    [SerializeField] public Color wantedColor;
    [SerializeField] public Color originalColor;
    
    private void Start()
    {
        buttonImg = GetComponent<Image>();

        originalColor.a = 0;

        //WanterColor
        wantedColor.r = 0.33f;
        wantedColor.g = 0.33f;
        wantedColor.b = 0.33f;
        wantedColor.a = 15;
        
    }

    public void IsOver()
    {
        buttonImg.color = wantedColor;       
    }

    public void IsNotOver()
    {
        buttonImg.color = originalColor;
    }

}
