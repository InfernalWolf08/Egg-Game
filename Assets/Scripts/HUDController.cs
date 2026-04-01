using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI goalText;
    public TextMeshProUGUI moneyText;

    [Header("Values")]
    public PlayerController pc;

    void Start()
    {
        // Initialize
        if (pc==null)
        {
            pc = Object.FindFirstObjectByType<PlayerController>();
        }
    }

    void Update()
    {
        scoreText.text = $"Score: {pc.score}";
        goalText.text = $"Goal: {LevelController.level*LevelController.goal}";
        moneyText.text = $"$$$: {pc.money}";
    }
}