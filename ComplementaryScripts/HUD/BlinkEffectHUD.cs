using UnityEngine;
using UnityEngine.UI;

public class BlinkEffectHUD : MonoBehaviour
{
    public static void BlinkEffect(GameObject obj ,Color startColor, Color endColor, float speed)
    {
        obj.GetComponent<Image>().color = Color.Lerp(startColor, endColor, Mathf.PingPong(Time.time * speed, 1));
    }
}
