using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;

    [Header("Stats")]
    public float speed = 6f;
    public float damage;

    [Header("Effects")]
    public bool slowing = false;
    public float slowPercentage = 0.5f;
    public float slowDuration = 5f;

    public bool AOE = false;
    

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 dir = target.transform.position - transform.position;
        float distanceStep = speed * Time.deltaTime;


        if (dir.magnitude <= distanceStep)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceStep, Space.World);
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
    }

    private void HitTarget()
    {
        Enemy enemy = target.gameObject.GetComponent<Enemy>();
        enemy.Damage(damage);
        if (slowing == true){
            enemy.Slow(slowPercentage, slowDuration);
        }
        Destroy(gameObject);
    }

    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    public void setSlow(float slowPercentage, float slowDuration){
        this.slowing = true;
        this.slowPercentage = slowPercentage;
        this.slowDuration = slowDuration;
    }

}
