using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float Range;// 炮塔攻击范围
    public string EnemyTag = "Player";// 敌人判定标签
    public float Damage;// 炮塔的攻击力
    public Transform BulletPoint;// 子弹的生成位置
    public GameObject BulletPrefeb;// 子弹的预制体
    public float AttackSpeed;// 发射子弹的速率
    public float ExplosionRadius;// 子弹爆炸伤害范围
    public float ExplosionAttenuation;// 爆炸伤害衰减系数
    public float Speed;// 子弹的速度
    internal bool CanAttack = true;

    private Transform Target;// 攻击目标
    private PlayerStatus EnemyHP;
    private float CountDown;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0, 0.5f);
        CountDown = 1 / AttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // 倒计时发射子弹
        CountDown -= Time.deltaTime;
        if (CountDown <= 0 && CanAttack)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bulletGo = Instantiate(BulletPrefeb, BulletPoint.position, transform.rotation);
        EnemyBullet bullet = bulletGo.GetComponent<EnemyBullet>();
        if (bullet == null)
        {
            bullet = bulletGo.AddComponent<EnemyBullet>();
        }
        bullet.SetTarget(Target);
        bullet.SetDamage(Damage);
        bullet.SetEnemyTag(EnemyTag);
        bullet.SetFlyingSpeed(Speed);
        bullet.SetExplosionRadius(ExplosionRadius);
        bullet.SetExplosionAttenuation(ExplosionAttenuation);
        CountDown = 1 / AttackSpeed;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);
        float minDistance = Mathf.Infinity;
        Transform nearestEnemy = null;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;// 找到最近的敌人
            }
        }
        if (minDistance < Range)
        {
            Target = nearestEnemy;
            EnemyHP = Target.GetComponent<PlayerStatus>();
        }
        else
        {
            Target = null;
        }
    }
}
