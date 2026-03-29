using System.Collections;
using UnityEngine;

public class SpawnEggs : MonoBehaviour
{
    public GameObject eggObject;

    void Start()
    {
        StartCoroutine(spawnEggs());
    }

    IEnumerator spawnEggs()
    {
        while (true)
        {
            if (GameController.gameStarted)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0, 10));
                Instantiate(eggObject, this.transform);
            }

            yield return null;
        }
    }
}