using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    [Header("Controller")]
    public PlayerController pc;
    private GameObject[] buttons = new GameObject[3];
    private int selectedIndex;
    public Upgrade[] totalUpgrades;
    public Upgrade[] upgradeOptions = new Upgrade[3];
    private Upgrade selectedUpgrade;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public Color defaultColor = Color.white;
    public Color selectedColor;

    void Awake()
    {
        // Initialize
        pc = FindAnyObjectByType<PlayerController>();
        for (int i=0; i<buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(3).GetChild(i).gameObject;
        }
    }

    // Functions
    public void selectUpgrade(int index)
    {
        // Set selected color
        for (int i=0; i<buttons.Length; i++)
        {
            // Store button as GameObject
            GameObject button = buttons[i];

            // Reset each button
            button.GetComponent<Image>().color = defaultColor;

            // Set the clicked one to selected color
            if (i==index)
            {
                button.GetComponent<Image>().color = selectedColor;
            }
        }

        // Update selected upgrade
        selectedIndex = index;
    }

    public void applyUpgrade()
    {
        // Iterate through stats and add to appropriate locations
        print($"Button Index: {selectedIndex}");
    }

    // Coroutines
    public IEnumerator loadShop()
    {
        // Set the upgrade options
        for (int i=0; i<upgradeOptions.Length; i++)
        {
            Upgrade upgrade = totalUpgrades[UnityEngine.Random.Range(0, totalUpgrades.Length)];
            upgradeOptions[i] = upgrade;
        
            // Change image and name
            // buttons[i]
        }

        // Show players their money
        while (pc.score>0)
        {
            pc.money++;
            pc.score--;
            moneyText.text = $"$$$: {pc.money}";
            yield return new WaitForSecondsRealtime(0.075f);
        }

        yield return null;
    }
}