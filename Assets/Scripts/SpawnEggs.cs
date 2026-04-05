using System.Collections;
using UnityEngine;

public class SpawnEggs : MonoBehaviour
{
    public GameObject eggObject;
    public GameObject rottenObject;
    public static float spawnRateMax=10;

    void Start()
    {
        StartCoroutine(spawnEggs());
    }

    public static void changeMax(float change)
    {
        spawnRateMax*=change;
    }

    IEnumerator spawnEggs()
    {
        while (true)
        {
            if (GameController.gameStarted)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0f, spawnRateMax));
                int spawn = UnityEngine.Random.Range(0, 10);
                if (spawn%3!=0 || LevelController.level<=0)
                {
                    Instantiate(eggObject, transform);
                } else {
                    Instantiate(rottenObject, transform);
                }
            }

            yield return null;
        }
    }
}