using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public float FollowRange;
    public float FollowSpeed;
    public float HealingRange;
    public float HealingSpeed;
    public float HealingAmount;
    internal bool IsHealing;
    internal bool IsFollowing;
    private float CoolDown;
    // Start is called before the first frame update
    void Start()
    {
        CoolDown = 1 / HealingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CoolDown -= Time.deltaTime;
        if (Vector3.Distance(transform.position, Player.transform.position) >= FollowRange && IsFollowing)
        {
            Vector3 dir = Player.transform.position - transform.position;
            transform.Translate(dir.normalized * FollowSpeed * Time.deltaTime, Space.World);
        }

        if (Vector3.Distance(transform.position, Player.transform.position) <= HealingRange && CoolDown <= 0 && IsHealing)
        {
            PlayerStatus player = Player.GetComponent<PlayerStatus>();
            player.Damage(-HealingAmount);
            CoolDown = 1 / HealingSpeed;
        }
    }

    public void SetFollowing(bool isFollowing)
    {
        IsFollowing = isFollowing;
    }
}
