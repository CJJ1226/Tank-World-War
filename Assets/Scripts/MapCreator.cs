using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    //0.Heart 1.Wall 2.Barrier 3.River 4.Grass 5.Born 6.AirBarrier
    public GameObject[] item;
    public float createEnemyTime;
    public int randomObjectNum;

    //已经有东西的位置列表
    private List<Vector3> itemPositionList = new List<Vector3>();

    private void Awake()
    {
        //Heart
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //Wall
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -8; j <= -7; j++)
            {
                if (!(i == 0 && j == -8))
                    CreateItem(item[1], new Vector3(i, j, 0), Quaternion.identity);
            }
        }

        //初始化玩家
        GameObject player1 = Instantiate(item[5], new Vector3(-2, -8, 0), Quaternion.identity);
        player1.GetComponent<Born>().createPlayer1 = true;
        GameObject player2 = Instantiate(item[5], new Vector3(2, -8, 0), Quaternion.identity);
        player2.GetComponent<Born>().createPlayer2 = true;

        //初始化敌人
        Instantiate(item[5], new Vector3(10, 8, 0), Quaternion.identity);
        Instantiate(item[5], new Vector3(-10, 8, 0), Quaternion.identity);
        //定时生成敌人
        InvokeRepeating("CreateEnemy", createEnemyTime, createEnemyTime);

        //随机生成地图
        int randomObject;
        for (int i = 0; i < randomObjectNum; i++)
        {
            randomObject = Random.Range(1, 5);
            CreateItem(item[randomObject], CreateRandomPosition(), Quaternion.identity);
        }
    }
    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    
    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (HasPosition(createPosition)) 
                return createPosition;
        }
    }
    private bool HasPosition(Vector3 createPosition)
    {
        int length = itemPositionList.Count;
        for(int i = 0; i < length; i++)
        {
            if (createPosition == itemPositionList[i])
                return false;
        }
        return true;
    }
    
    private void CreateEnemy()
    {
        int num = Random.Range(0, 2);
        Vector3 enemyPosition = new Vector3(0, 8, 0);
        if (num == 0)
        {
            enemyPosition.x = 10;
        }
        else
        {
            enemyPosition.x = -10;
        }
        Instantiate(item[5], enemyPosition, Quaternion.identity);
    }
}
