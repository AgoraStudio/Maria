using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Transform m_Target;
    private float Speed;// 子弹的速度
    private float ExplosionRadius;
    private float Damage;
    private Vector3 TargetPosition;
    private string EnemyTag;

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }

    public void SetDamage(float amount)
    {
        Damage = amount;
    }

    public void SetEnemyTag(string tag)
    {
        EnemyTag = tag;
    }

    public void SetFlyingSpeed(float speed)
    {
        this.Speed = speed;
    }

    public void SetExplosionRadius(float radius)
    {
        ExplosionRadius = radius;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (m_Target != null)
        {
            TargetPosition = m_Target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 dir = TargetPosition - transform.position;
            if (Vector3.Distance(TargetPosition, transform.position) > Speed * Time.deltaTime)
            {
                transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
            }
            else
            {
                HitTarget();
            }
        }
    }
    private void HitTarget()
    {
        // 是否aoe
        if (ExplosionRadius > 0)
        {
            // 圆的范围内碰到的所有collider
            Collider[] colliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
            foreach (var item in colliders)
            {
                if (item.tag == EnemyTag)
                {
                    // 会衰减的溅射伤害
                    EnemyDamage(item.transform, (1 - Vector3.Distance(item.transform.position, transform.position) / ExplosionRadius) * Damage);
                }
            }
        }
        else
        {
            // 敌人减血
            EnemyDamage(m_Target, Damage);
        }
        // 销毁自己
        Destroy(gameObject);
    }
    private void EnemyDamage(Transform enemy, float damage)
    {
        EnemyHealth enemyHP = enemy.GetComponent<EnemyHealth>();
        if (enemyHP != null)
        {
            enemyHP.Damage(damage);
        }
    }
}
