using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    TMP_Text enemyHpTmpText;

    float moveSpeed = 10;
    int enemyHp = 50;

    private void Awake()
    {
        enemyHpTmpText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        enemyHpTmpText.text = enemyHp.ToString();
        // �Ƹ��� �� �������θ� �̵�
        transform.Translate(new Vector3(0,0,-moveSpeed) * Time.deltaTime);
    }

    void Death()
    {
        Destroy(gameObject);
    }


    public void SetHp(int hp)
    {
        this.enemyHp = hp;
    }

    public int Attack()
    {
        return enemyHp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerBullet"))
        {
            enemyHp -= other.GetComponent<Bullet>().damage;
            Destroy(other.gameObject);

            if(enemyHp <= 0)
            {
                Death();
            }
        }
    }
}
