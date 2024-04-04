using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutHUD : MonoBehaviour
{
    public static void FadeIn(CanvasGroup canvas)
    {
        if(canvas.alpha < 1)
        {
            canvas.alpha += Time.deltaTime;            
        }
    }

    public static void FadeOut(CanvasGroup canvas)
    {
        if (canvas.alpha >= 0)
        {
            canvas.alpha -= Time.deltaTime;
        }
    }
}
