using UnityEngine;
using TMPro;

public class TurretStatDisplayManager : MonoBehaviour
{
    public TextMeshProUGUI turretNameUI;
    public TextMeshProUGUI damageUI;
    public TextMeshProUGUI rangeUI;
    public TextMeshProUGUI fireRateUI;
    public GameObject upgradeButton;
    public TextMeshProUGUI upgradeUI;

    public Color notSelectedColor, placingColor, titleColor, statsColor;

    void Awake()
    {
        clearTurretStats();
    }

    void Start(){
        upgradeUI = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void displayTemplateStats(TurretTemplate turret, string suffix)
    {
        setDisplayedStats(turret.turretName, turret.damage, turret.range, 
                            turret.fireRate, placingColor, placingColor);
        turretNameUI.text += "\n" + suffix;
    }

    public void displayTurretStats(TurretBehaviour turret)
    {
        if (turret == null)
        {
            clearTurretStats();
        }
        else
        {
            setDisplayedStats(turret.getName(), turret.getDamage(), turret.getRange(), 
                turret.getFireRate(), titleColor, statsColor);
            upgradeButton.SetActive(true);
            int cost = turret.getUpgradeCost();
            upgradeUI.text = ($"upgrade ({cost}g)");
        }
    }

    private void setDisplayedStats(string name, float damage, float range,
                                float fireRate, Color titleColor, Color statsColor)
    {
        turretNameUI.text = name;
        damageUI.text = "dmg: " + damage.ToString("0.#");
        rangeUI.text = "rng: " + range.ToString("0.#");
        fireRateUI.text = "spd: " + fireRate.ToString("0.#");

        turretNameUI.color = titleColor;
        damageUI.color = statsColor;
        rangeUI.color = statsColor;
        fireRateUI.color = statsColor;
    }

    public void clearTurretStats()
    {
        turretNameUI.text = "no turret selected";
        damageUI.text = "";
        rangeUI.text = "";
        fireRateUI.text = "";
        
        turretNameUI.color = notSelectedColor;
        damageUI.color = notSelectedColor;
        rangeUI.color = notSelectedColor;
        fireRateUI.color = notSelectedColor;

        upgradeButton.SetActive(false);
    }

    public void hideUpgradeButton(){
        upgradeButton.SetActive(false);
    }

}
