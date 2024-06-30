using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public enum EnemyType { EnemyA, ItemA, ItemB, ItemC}
public enum EnemeyPosition { Left, Right, Center }

public class Data
{
    public EnemyType enemyType;
    public EnemeyPosition choiceRegion;
    public int spawnTime;
    public int enemyHp;

    public Data(EnemyType enemyType, EnemeyPosition choiceRegion, int spawnTime, int enemyHp)
    {
        this.choiceRegion = choiceRegion;
        this.enemyType = enemyType;
        this.spawnTime = spawnTime;
        this.enemyHp = enemyHp;
    }
}

public class Spawn : MonoBehaviour
{
    public GameObject[] enemys;

    private void Start()
    {
        var spawnDatas = LoadData(1);
        StartCoroutine(SpawnStart(spawnDatas));
    }

    List<Data> LoadData(int stageIndex)
    {
        var dataList = new List<Data>();

        TextAsset text = Resources.Load<TextAsset>($"stage{stageIndex}");
        string[] datas = text.text.Split("\n");
        int i = 0;
        while (i < datas.Length)
        {
            var tempData = datas[i].Split(",");
            var enemyType = (EnemyType)Enum.Parse(typeof(EnemyType), tempData[0]);
            var enemyPosition = (EnemeyPosition)Enum.Parse(typeof(EnemeyPosition), tempData[1]);
            var spawnTime = int.Parse(tempData[2]);
            var enemyHp = int.Parse(tempData[3]);

            Debug.Log($"{enemyType} / {enemyPosition} / {spawnTime}");
            dataList.Add(new Data(enemyType,enemyPosition, spawnTime, enemyHp));
            i++;
        }

        Debug.Log(dataList);
        return dataList;
    }

    IEnumerator SpawnStart(List<Data> spawnData)
    {
        int maxTime = int.MinValue;
        int nowSpawnCount = 0;

        for(int i = 0; i < spawnData.Count; i++)
        {
            if (spawnData[i].spawnTime > maxTime)
            {
                maxTime = spawnData[i].spawnTime;
            }
        }

        for(int i = 0; i <= maxTime; i++)
        {
            // 현재 시간에 해당하는 모든 적을 스폰
            while (nowSpawnCount < spawnData.Count && spawnData[nowSpawnCount].spawnTime == i)
            {
                SpawnEnemy(spawnData[nowSpawnCount].enemyType, spawnData[nowSpawnCount].choiceRegion, spawnData[nowSpawnCount].enemyHp);
                nowSpawnCount++;
            }
            yield return new WaitForSeconds(1);
        }
        yield return null;
    }

    void SpawnEnemy(EnemyType enemyType, EnemeyPosition choiceRegion,int enemyHp)
    {
        var spawnPosition = new Vector3(0,0,0);
        switch(choiceRegion)
        {
            case EnemeyPosition.Left:
                spawnPosition = new Vector3(-5, 1, 90);
                break;
            case EnemeyPosition.Right:
                spawnPosition = new Vector3(5, 1, 90);
                break;
            case EnemeyPosition.Center:
                if((int)enemyType == 4)
                {
                    spawnPosition = new Vector3(0f, 6, 90);
                } else
                {
                    spawnPosition = new Vector3(0f, 1, 90);
                }
                break;
        }

        // 생성
        var enemy = Instantiate(enemys[(int)enemyType], spawnPosition, Quaternion.identity);
        enemy.transform.parent = GameObject.Find("SpawnEnemy").transform;
        if(enemy.GetComponent<Enemy>() != null)
        {
            enemy.GetComponent<Enemy>().SetHp(enemyHp);
        } else if(enemy.GetComponent<Boss>() != null)
        {
            enemy.GetComponent<Boss>().SetHp(enemyHp);
        }
    }
}
