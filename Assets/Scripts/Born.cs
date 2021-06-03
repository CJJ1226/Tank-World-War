using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject enemyPrefab;

    public bool createPlayer1;
    public bool createPlayer2;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void BornTank()
    {
        if(createPlayer1)
        {
            Instantiate(player1Prefab, transform.position, Quaternion.identity);
        }
        else if (createPlayer2)
        {
            Instantiate(player2Prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
        
    }
}
