using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public float Range;// 攻击范围
    public string EnemyTag = "Enemy";// 敌人判定标签
    public float Damage;// 攻击力
    public GameObject BulletPrefeb;// 子弹的预制体
    public float AttackSpeed;// 发射子弹的速率
    public float Speed;// 子弹的速度
    public GameObject TargetIcon;// 锁定标记

    public static Transform Target;// 攻击目标

    private EnemyHealth EnemyHP;
    private float CountDown;

    // Start is called before the first frame update
    void Start()
    {
        CountDown = 1 / AttackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown -= Time.deltaTime;
        if (Target != null && Vector3.Distance(transform.position, Target.position) <= Range)
        {
            // 倒计时结束发射子弹
            if (CountDown <= 0)
            {
                Shoot();
            }
            TargetIcon.transform.position = Target.position;
            TargetIcon.SetActive(true);
        }
        else
        {
            TargetIcon.SetActive(false);
        }
    }

    public static void SetTarget(Transform target)
    {
        Target = target;
    }

    private void Shoot()
    {
        GameObject bulletGo = Instantiate(BulletPrefeb, transform.position, new Quaternion());
        PlayerBullet bullet = bulletGo.GetComponent<PlayerBullet>();
        if (bullet == null)
        {
            bullet = bulletGo.AddComponent<PlayerBullet>();
        }
        bullet.SetTarget(Target);
        bullet.SetDamage(Damage);
        bullet.SetEnemyTag(EnemyTag);
        bullet.SetFlyingSpeed(Speed);
        CountDown = 1 / AttackSpeed;
    }
}
