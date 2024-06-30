using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss : MonoBehaviour
{
    TMP_Text enemyHpTmpText;

    float moveSpeed = 5;
    int enemyHp = 50;

    private void Awake()
    {
        enemyHpTmpText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        enemyHpTmpText.text = enemyHp.ToString();
        // 아마도 한 방향으로만 이동
        transform.Translate(new Vector3(0, 0, -moveSpeed) * Time.deltaTime);
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public int Attack()
    {
        return enemyHp;
    }

    public void SetHp(int hp)
    {
        this.enemyHp = hp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            enemyHp -= other.GetComponent<Bullet>().damage;
            Destroy(other.gameObject);

            if (enemyHp <= 0)
            {
                Death();
            }
        }
    }
}
