using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public class ShopController : MonoBehaviour
{
    [Header("Controller")]
    public PlayerController pc;
    private GameObject[] buttons = new GameObject[3];
    private int selectedIndex=-1;
    public Upgrade[] totalUpgrades;
    public Upgrade[] upgradeOptions = new Upgrade[3];
    private Upgrade selectedUpgrade;

    [Header("UI")]
    public TextMeshProUGUI moneyText;
    public Color defaultColor = Color.white;
    public Color selectedColor;
    public MenuController menuCont=null;

    void Awake()
    {
        // Initialize
        pc = FindAnyObjectByType<PlayerController>();
        for (int i=0; i<buttons.Length; i++)
        {
            buttons[i] = transform.GetChild(3).GetChild(i).gameObject;
        }
        for (int i=0; i<totalUpgrades.Length; i++)
        {
            totalUpgrades[i].Reset();
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
            if (i==index && selectedIndex!=index)
            {
                button.GetComponent<Image>().color = selectedColor;
            }
        }

        // Update selected upgrade
        if (selectedIndex!=index)
        {
            selectedIndex = index;
        } else {
            selectedIndex = -1;
        }
    }

    public void buyUpgrade()
    {
        if (selectedIndex>=0)
        {
            // Subtract cost and apply stat changes
            Upgrade upgrade = upgradeOptions[selectedIndex];
            if (pc.money>=upgrade.price)
            {
                StartCoroutine(applyUpgrade(upgrade));
                upgrade.price+=10;
            }
        } else if (selectedIndex<0) {
            // Continue with no upgrade
            menuCont.closeShop();
        }
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
            Transform button = buttons[i].transform;
            button.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgrade.name;
            button.GetChild(1).GetComponent<Image>().sprite = upgrade.sprite;
            button.GetChild(2).GetComponent<TextMeshProUGUI>().text = $"${upgrade.price}";

            // Reset button colors
            buttons[i].GetComponent<Image>().color = defaultColor;
        }

        // Show players their money
        while (pc.score>0)
        {
            pc.money++;
            pc.score--;
            moneyText.text = $"$$$: {pc.money}";
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return null;
    }

    IEnumerator applyUpgrade(Upgrade upgrade)
    {
        // Animation of money decreasing
        int final = pc.money-upgrade.price;
        while (pc.money>final)
        {
            pc.money--;
            moneyText.text = $"$$$: {pc.money}";
            yield return new WaitForSecondsRealtime(0.01f);
        }

        yield return new WaitForSecondsRealtime(.01f);

        // Go through stats and add to appropriate locations
        pc.speed += upgrade.speed;
        Transform basket = pc.transform.GetChild(1);
        basket.localScale = new Vector3(basket.localScale.x+upgrade.basketSizeX, basket.localScale.y+upgrade.basketSizeY, basket.localScale.z);
        basket.localPosition = new Vector3(basket.localPosition.x, basket.localPosition.y+(upgrade.basketSizeY/2), basket.localPosition.z);
        EggController.alterSpeed(upgrade.eggDropSpeed);
        SpawnEggs.changeMax(upgrade.eggSpawnRate);

        // Close shop
        menuCont.closeShop();
    }
}