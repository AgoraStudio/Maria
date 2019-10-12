using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float InitHealth = 100;// 初始生命值
    private float CurrentHealth;
    public Image HPBar;// 血条
    // Start is called before the first frame update
    void Start()
    {

        CurrentHealth = InitHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Damage(float amount)
    {
        CurrentHealth -= amount;
        HPBar.fillAmount = CurrentHealth / InitHealth;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        AutoAttack.SetTarget(gameObject.transform);
    }
}
