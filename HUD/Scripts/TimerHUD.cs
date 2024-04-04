using UnityEngine;
using TMPro;

public class TimerHUD : MonoBehaviour
{
    private float timeStart = 0f;

    private float timer;

    [SerializeField] private TextMeshProUGUI minute1;
    [SerializeField]  private TextMeshProUGUI minute2;
    [SerializeField] private TextMeshProUGUI midle;
    [SerializeField] private TextMeshProUGUI second1;
    [SerializeField] private TextMeshProUGUI second2;

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerHUD(timer);
    }

    private void ResetTimer()
    {
        timer = timeStart;
    }

    private void UpdateTimerHUD(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        minute1.text = currentTime[0].ToString();
        minute2.text = currentTime[1].ToString();
        second1.text = currentTime[2].ToString();
        second2.text = currentTime[3].ToString();
    }
}
