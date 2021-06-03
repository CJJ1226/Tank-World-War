using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int lifeValue1 = 3;
    public int lifeValue2 = 3;
    public int playerScore = 0;
    public bool isDead1;
    public bool isDead2;
    public bool isDefeat;
    //引用
    public GameObject born;
    public Text playerScoreText;
    public Text PlayerLifeText;
    public GameObject isDefeatUI;
    //单例模式
    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((lifeValue1 <= 0 && lifeValue2 <= 0) || isDefeat) 
        {
            //游戏结束，返回主界面
            isDefeat = true;
            isDefeatUI.SetActive(true);
        }
        if (isDead1 && lifeValue1 > 0)
        {
            Recover();
        }
        else if (isDead2 && lifeValue2 > 0)
        {
            Recover();
        }
        playerScoreText.text = playerScore.ToString();
        PlayerLifeText.text = lifeValue1.ToString();
    }

    private void Recover()
    {
        if(isDead1 && lifeValue1 > 0)
        {
            lifeValue1--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer1 = true;
            isDead1 = false;
        }
        else if(isDead2 && lifeValue2 > 0)
        {
            lifeValue2--;
            GameObject go = Instantiate(born, new Vector3(2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer2 = true;
            isDead2 = false;
        }
    }
}
