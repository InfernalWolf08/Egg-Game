using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Player")]
    public PlayerController pc;

    [Header("Settings")]
    public static int level=1;
    public static int goal=10;

    void Start()
    {
        // Initialize
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        // Increase level when goal is reached
        if (pc.score==level*goal)
        {
            levelUp();
        }
    }

    void levelUp()
    {
        // Increment level
        level++;

        // Convert points to money
        StartCoroutine(pointsToMoney());

        // Bring up shop
        GetComponent<MenuController>().openShop();
    }

    IEnumerator pointsToMoney()
    {
        while (pc.score>0)
        {
            pc.score--;
            pc.money++;
            yield return new WaitForSeconds(0.01f);
            yield return null;
        }
    }
}