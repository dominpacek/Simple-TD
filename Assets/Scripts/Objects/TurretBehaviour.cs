using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    private TurretTemplate template;

    private string turretName;

    [Header("Stats")]
    public float damage;
    public float range;
    public float fireRate;

    [Header("Effects")]
    [SerializeField]
    private bool slowing;
    [SerializeField]
    private float slowPercentage, slowDuration;

    public GameObject bulletPrefab;

    // Debuff effects
    private float slowDebuffPercentage = 0f;
    private float slowDebuffDuration;


    private GameObject target;
    private float fireCountdown = 0f;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = template.sprite;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 targ = target.transform.position;
        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (slowDebuffDuration > 0)
        {
            slowDebuffDuration -= Time.deltaTime;
            if (slowDebuffDuration <= 0)
            {
                slowDebuffPercentage = 0f;
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.color = Color.white;
            }
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / (fireRate * (1 - slowDebuffPercentage));
        }

        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        List<GameObject> enemies = new List<GameObject>();
        foreach (string tag in template.enemyTags)
        {
            enemies.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {

            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }
    }

    void Shoot()
    {
        GameObject bulletObject = (GameObject)Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.setDamage(damage);
            if (slowing == true)
            {
                bullet.setSlow(slowPercentage, slowDuration);
            }
            bullet.SetTarget(target);
        }
        SpriteRenderer bulletSprite = bulletObject.GetComponent<SpriteRenderer>();
        bulletSprite.sprite = template.bulletSprite;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    public void setType(TurretTemplate type)
    {
        template = type;
        turretName = template.turretName;
        damage = template.damage;
        range = template.range;
        fireRate = template.fireRate;
        slowing = template.slowing;
        slowPercentage = template.slowPercentage;
        slowDuration = template.slowDuration;
    }

    public void Upgrade()
    {
        if (Player.SpendGold(template.upgradeCost))
        {
            damage += template.damageUpgrade;
            range += template.rangeUpgrade;
            fireRate += template.fireRateUpgrade;
        }
    }

    // this also allows for making the enemy go back, but it won't take turns
    public void setSlowDebuff(float percentage, float duration)
    {
        if (percentage > 1)
        {
            // percentage higher than 1 can be used to buff firerate
            // but not intending such use at this time
            Debug.LogError("Turret received slow effect value greater than 1;" +
                            "Sped up attack instead!");
        }
        if (duration <= 0)
        {
            Debug.LogError("Turret received slow duration was less than zero!");
        }
        slowDebuffPercentage = percentage;
        slowDebuffDuration = duration;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.green;
    }



    public string getName()
    {
        return turretName;
    }
    public float getDamage()
    {
        return damage;
    }
    public float getRange()
    {
        return range;
    }
    public float getFireRate()
    {
        return fireRate;
    }
    public int getUpgradeCost()
    {
        return template.upgradeCost;
    }
}
