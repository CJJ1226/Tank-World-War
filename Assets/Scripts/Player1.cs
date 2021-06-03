using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    //常量属性值
    const float defendTime = 3f;
    //可变属性值
    public float moveSpeed = 3;
    public bool isPlayer1;

    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float defendTimeVal = defendTime;

    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //上 右 下 左
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //攻击CD
        if (timeVal > 0.5f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
        if (defendTimeVal > 0)
            defendTimeVal -= Time.deltaTime;
        else
            defendEffectPrefab.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (PlayerManager.Instance.isDefeat)
            Destroy(gameObject);
        Move();
    }
    //坦克的攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPlayer1) 
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0) && !isPlayer1)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            timeVal = 0;
        }
    }
    //坦克的移动方法
    private void Move()
    {
        float h, v;
        if (isPlayer1)
        {
            h = Input.GetAxisRaw("Horizontal1");
            v = Input.GetAxisRaw("Vertical1");
        }
        else
        {
            h = Input.GetAxisRaw("Horizontal2");
            v = Input.GetAxisRaw("Vertical2");
        }
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)  //left
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0) //right
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if (h != 0) //避免两个键一起按发生漂移
        {
            return;
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)  //down
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0) //up
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }
    //坦克的死亡方法
    private void Die()
    {
        if (defendTimeVal > 0)
            return;

        if(isPlayer1)
            PlayerManager.Instance.isDead1 = true;
        else
            PlayerManager.Instance.isDead2 = true;

        //产生爆炸效果
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }
}
