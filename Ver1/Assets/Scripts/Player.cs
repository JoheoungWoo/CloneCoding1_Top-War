using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    //프리팹
    public GameObject bulletPrefab;

    //텍스트
    TMP_Text playerHpTmpText;

    //스테이터스들
    float moveSpeed = 10;
    float bulletMoveSpeed = 10f;
    float attackDelay = 1f;
    float nowDelay = 0f;

    int hp = 3;

    

    private void Awake()
    {
        playerHpTmpText = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        //hp 갱신
        playerHpTmpText.text = hp.ToString();


        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(horizontal, vertical, 0) * Time.deltaTime * moveSpeed);

        nowDelay += Time.deltaTime;
        if (nowDelay >= attackDelay)
        {
            Attack();
            nowDelay = 0f;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Attack()
    {
        var bullet = Instantiate(bulletPrefab, transform.position + new Vector3(0,0,3),Quaternion.identity);
        bullet.transform.SetParent(GameObject.Find("Bullets").transform);
        bullet.GetComponent<Bullet>().SetDamage(hp);
        bullet.GetComponent<Bullet>().SetSpeed(bulletMoveSpeed);
    }

    private bool IsEnemy(Collider other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            return true;
        }
        return false;
    }

    private bool IsBoss(Collider other)
    {
        if (other.GetComponent<Boss>() != null)
        {
            return true;
        }
        return false;
    }


    private bool IsItem(Collider other)
    {
        if (other.GetComponent<Item>() != null)
        {
            return true;
        }
        return false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (IsEnemy(other))
        {
            int damage = other.GetComponent<Enemy>().Attack();
            Debug.Log(damage);
            if (damage > 0)
            {
                hp -= damage;
                Destroy(other.gameObject);
                if (hp <= 0)
                {
                    Die();
                }
            }
        } else if(IsBoss(other))
        {

            int damage = other.GetComponent<Boss>().Attack();
            Debug.Log(damage);
            if (damage > 0)
            {
                hp -= damage;
                Destroy(other.gameObject);
                if (hp <= 0)
                {
                    Die();
                }
            }
        }

        if(IsItem(other))
        {
            switch(other.tag)
            {
                case "ItemA":
                    hp += 7;
                    break;
                case "ItemB":
                    hp *= 2;
                    break;
                case "ItemC":
                    bulletMoveSpeed *= 1.3f;
                    attackDelay -= attackDelay * 0.5f;
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
        }
    }

}
