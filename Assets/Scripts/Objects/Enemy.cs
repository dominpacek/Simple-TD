using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    // stats
    [SerializeField]
    private float speed;
    private float health;
    [SerializeField]
    private int goldDropValue;
    [SerializeField]
    private float currentHealth;

    // particle effect of money gain on enemy death
    public GameObject coinParticle;

    [SerializeField]
    private Traits.Trait enemyType = Traits.Trait.None;

    // Pathing
    private Vector3 target;
    private int currentPosition = 0;

    // Debuff effects
    private float slowPercentage = 1f;
    private float slowDuration;

    private Animator animator;
    private bool dead;

    public string turretTag = "Turret";

    void Start()
    {
        applyTypeModifiers();

        currentHealth = health;
        target = PathGenerator.waypoints[0];

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (dead)
        {
            // don't do anything while waiting for death animation to finish
            return;
        }
        Vector2 dir = target - transform.position;
        if (slowDuration > 0)
        {
            slowDuration -= Time.deltaTime;
            if (slowDuration <= 0)
            {
                slowPercentage = 1f;
                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                sprite.color = Color.white;
            }
        }

        transform.Translate(dir.normalized * speed * Time.deltaTime * slowPercentage, Space.World);

        if (Vector2.Distance(transform.position, target) <= 0.05f)
        {
            GetNextTarget();
        }

        if (currentHealth <= 0)
        {
            gameObject.tag = "Untagged";
            GameObject particle = Instantiate(coinParticle, transform.position, Quaternion.identity);
            Destroy(particle, 1.5f);
            animator.Play("Death");
            dead = true;
            Destroy(gameObject, 0.5f);
            EnemySpawner.enemiesAlive--;
            Player.GainGold(goldDropValue);
            GetComponent<AudioSource>().Play();
        }

        if (enemyType == Traits.Trait.Slimy)
        {
            List<GameObject> turrets = new List<GameObject>();
            turrets.AddRange(GameObject.FindGameObjectsWithTag(turretTag));

            foreach (GameObject turret in turrets)
            {

                float distanceToTurret = Vector2.Distance(transform.position, turret.transform.position);
                if (distanceToTurret < Traits.slowingRange)
                {
                    turret.GetComponent<TurretBehaviour>().setSlowDebuff(Traits.slowPercentage, Traits.slowDuration);
                }
            }
        }

    }


    void GetNextTarget()
    {
        currentPosition++;
        if (currentPosition >= PathGenerator.waypoints.Count)
        {
            // enemy reached endpoint and player should be dealt damage
            Player.Damage();
            Destroy(gameObject);
            EnemySpawner.enemiesAlive--;
            return;
        }
        target = PathGenerator.waypoints[currentPosition];
    }

    // deals damage to the enemy lowering current health
    public void Damage(float damage)
    {
        Color c = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = c;
        currentHealth -= damage;
        animator.Play("Hit");
    }

    // this also allows for making the enemy go back, but it won't take turns
    public void Slow(float percentage, float duration)
    {
        if (percentage > 1)
        {
            // percentage higher than 1 can be used to buff speed
            // but not intending such use at this time
            Debug.LogError("Enemy received slow effect value greater than 1;" +
                            "Sped up movement instead!");
        }
        if (duration <= 0)
        {
            Debug.LogError("Enemy received slow duration was less than zero!");
        }
        slowPercentage = percentage;
        slowDuration = duration;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.blue;
    }

    public void setType(Traits.Trait trait)
    {
        this.enemyType = trait;
    }

    public void setValues(float speed, int health, int gold)
    {
        this.speed = speed;
        this.health = health;
        this.goldDropValue = gold;
    }


    private void applyTypeModifiers()
    {
        int additionalGold = (int)(goldDropValue * 0.4f);
        switch (enemyType)
        {

            case Traits.Trait.Speedy:
                speed *= Traits.speedyBoost;
                break;

            case Traits.Trait.Tough:
                health *= Traits.toughBoost;
                break;

            case Traits.Trait.Slimy:
                break;

            default:
                additionalGold = 0;
                break;
        }
        goldDropValue += additionalGold;
    }

}
