using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLeft; // Seconds

    void Update()
    {
        // Timer
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft/60);
        int seconds = Mathf.FloorToInt(timeLeft%60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // If the player is out of time
        if (timeLeft<=0)
        {
            Object.FindFirstObjectByType<MenuController>().endGame();
        }
    }
}