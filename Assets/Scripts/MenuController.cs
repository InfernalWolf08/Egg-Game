using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class MenuController : MonoBehaviour
{
    [Header("Menus")]
    public GameObject start;
    public GameObject shop;
    public GameObject end;
    public GameObject hud;

    [Header("Other Stuff")]
    public AudioController audio;

    void Start()
    {
        // Initialize
        Time.timeScale = 0;
        start.SetActive(true);
        shop.SetActive(false);
        end.SetActive(false);
        hud.SetActive(false);
        audio.changeBG(audio.start);
    }

    public void startGame()
    {
        Time.timeScale=1;
        audio.changeBG(audio.game);
        start.SetActive(false);
        hud.SetActive(true);
    }

    public void endGame()
    {
        Time.timeScale=0;
        audio.changeBG(audio.end);
        end.SetActive(true);
        hud.SetActive(false);
        int score = GetComponent<PlayerController>().score+GetComponent<PlayerController>().money;
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        } else if (PlayerPrefs.GetInt("Highscore")<score) {
            PlayerPrefs.SetInt("Highscore", score);
        }
        end.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = $"Time's Up\nBetter Luck Next Time\n\nFinal Score: {GetComponent<PlayerController>().score}\nHighscore: {PlayerPrefs.GetInt("Highscore")}\nFinal Level: {LevelController.level}";
    }

    public void openShop()
    {
        Time.timeScale=0;
        audio.changeBG(audio.shop);
        shop.SetActive(true);
        hud.SetActive(false);
        StartCoroutine(shop.GetComponent<ShopController>().loadShop());
    }

    public void closeShop()
    {
        Time.timeScale=1;
        audio.changeBG(audio.game);
        shop.SetActive(false);
        hud.SetActive(true);
        FindAnyObjectByType<Timer>().timeLeft+=10*LevelController.level;
    }

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}