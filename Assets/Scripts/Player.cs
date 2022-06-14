using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 5;
    public int startingGold = 50;

    [Header("GUI elements")]
    public GameObject healthDisplay;

    private static int currentHealth;
    private static int gold;

    private static AudioSource hitSound;

    void Awake()
    {
        hitSound = GetComponent<AudioSource>();
        gold = startingGold;
        currentHealth = maxHealth;
    }
    void Update()
    {
        float ratio = (float)currentHealth / maxHealth;

        healthDisplay.GetComponent<HealthDisplayManager>().updateHealthbar(ratio);
    }

    public static void Damage()
    {
        hitSound.Play();
        currentHealth -= 1;
        if (currentHealth <= 0)
        {
            Debug.Log("koniec gry");
            Time.timeScale = 0f;
        }
    }

    public static bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    public static void GainGold(int amount)
    {
        gold += amount;
    }

    public static int GetGold()
    {
        return gold;
    }

}
