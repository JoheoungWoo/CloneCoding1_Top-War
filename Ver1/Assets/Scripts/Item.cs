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
        // �Ƹ��� �� �������θ� �̵�
        transform.Translate(new Vector3(0, 0, -moveSpeed) * Time.deltaTime);
    }
}
