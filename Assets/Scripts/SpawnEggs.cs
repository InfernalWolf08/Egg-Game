using System.Collections;
using UnityEngine;

public class SpawnEggs : MonoBehaviour
{
    public GameObject eggObject;
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
                Instantiate(eggObject, this.transform);
            }

            yield return null;
        }
    }
}