using UnityEngine;
using UnityEngine.UI;

public class SurvivalTimer : MonoBehaviour
{
    public Text timerText;
    private float survivalTime;

    void Start()
    {
        survivalTime = 0;
    }

    void Update()
    {
        survivalTime += Time.deltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        // Format the time however you prefer. Here it's minutes:seconds.
        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);
        timerText.text = string.Format("Survival Time: {0:00}:{1:00}", minutes, seconds);
    }




}
