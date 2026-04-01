using UnityEngine;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject start;
    public GameObject shop;
    public GameObject end;
    public GameObject hud;

    void Start()
    {
        // Initialize
        Time.timeScale = 0;
        start.SetActive(true);
        shop.SetActive(false);
        end.SetActive(false);
        hud.SetActive(false);
    }

    public void startGame()
    {
        Time.timeScale=1;
        start.SetActive(false);
        hud.SetActive(true);
    }

    public void endGame()
    {
        Time.timeScale=0;
        end.SetActive(true);
        hud.SetActive(false);
        end.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = $"Time's Up\nBetter Luck Next Time\n\nFinal Score: {GetComponent<PlayerController>().score}\nFinal Level: {LevelController.level}";
    }

    public void openShop()
    {
        Time.timeScale=0;
        shop.SetActive(true);
        hud.SetActive(false);
        StartCoroutine(shop.GetComponent<ShopController>().loadShop());
    }

    public void closeShop()
    {
        Time.timeScale=1;
        shop.SetActive(false);
        hud.SetActive(true);
    }
}