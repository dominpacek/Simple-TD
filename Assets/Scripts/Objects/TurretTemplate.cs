using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Turret")]
public class TurretTemplate : ScriptableObject
{
    [Header("Appearance")]

    public string turretName;
    public string description;
    public Sprite sprite;
    public Sprite bulletSprite;

    [Header("Stats")]

    public float damage;
    public float range;
    public float fireRate;

    [Header("Upgrades")]
    public int cost;
    public int upgradeCost;
    public float damageUpgrade;
    public float rangeUpgrade;
    public float fireRateUpgrade;

    [Header("Effects")]
    public bool slowing = false;
    public float slowPercentage;
    public float slowDuration;

    [Header("Unity Variables")]

    public string[] enemyTags = {"Enemy"};
}
