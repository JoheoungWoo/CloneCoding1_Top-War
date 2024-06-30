using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    float moveSpeed = 10;

    // Update is called once per frame
    void Update()
    {
        // 아마도 한 방향으로만 이동
        transform.Translate(new Vector3(0, 0, -moveSpeed) * Time.deltaTime);
    }
}
