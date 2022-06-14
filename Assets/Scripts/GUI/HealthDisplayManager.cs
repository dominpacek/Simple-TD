using UnityEngine;
using UnityEngine.UI;

public class HealthDisplayManager : MonoBehaviour
{
    public Image healthbarFill;

    void Awake()
    {
        updateHealthbar(1f);
    }

    public void updateHealthbar(float healthPercentage)
    {
        if (healthPercentage < 0)
        {
            healthbarFill.transform.localScale = new Vector3(0, 1, 1);
        }
        else if (healthPercentage > 1)
        {
            Debug.LogError("Health over 100%?");
        }
        else
        {
            healthbarFill.transform.localScale = new Vector3(healthPercentage, 1, 1);
        }
    }
}
