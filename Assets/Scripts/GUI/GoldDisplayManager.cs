using UnityEngine;
using TMPro;

public class GoldDisplayManager : MonoBehaviour
{

    public TextMeshProUGUI goldCounter;
   
    void Start()
    {
        goldCounter = GetComponent<TextMeshProUGUI>();
        goldCounter.text = ($"gold: {Player.GetGold()}");
    }


    void Update()
    {
        goldCounter.text = ($"gold: {Player.GetGold()}");
    }
}
