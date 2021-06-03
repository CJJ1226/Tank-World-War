using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //�ɱ�����ֵ
    const float defendTime = 3f;
    public float moveSpeed = 4;
    private int kinds;

    private Vector3 bulletEulerAngles;
    private int enemyLives = 0;
    private float h;
    private float v;

    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //�� �� �� ��
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab;

    //��ʱ��
    private float timeVal;
    private float defendTimeVal;
    private float changeDirectionTimeVal = 2;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        kinds = Random.Range(0, 8);
        sr.sprite = tankSprite[kinds];
        if (kinds < 2)  //����̹��
        {
            defendTimeVal = defendTime;
            moveSpeed -= 1;
        }
        else if (kinds > 3) //����̹��
        {
            enemyLives = 1;
            moveSpeed -= 2;
        }
    }
    // Update is called once per frame
    void Update()
    {
        changeDirectionTimeVal += Time.deltaTime;
        //�������
        if (timeVal > 4)
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
        Move();
    }
    //̹�˵Ĺ�������
    private void Attack()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }
    //̹�˵��ƶ�����
    private void Move()
    {
        if (changeDirectionTimeVal >= 2)
        {
            int judge = Random.Range(0, 8);
            if (judge == 0)
            {
                v = 1;
                h = 0;
            }
            else if (judge <= 2)
            {
                h = -1;
                v = 0;
            }
            else if (judge <= 4)
            {
                h = 1;
                v = 0;
            }
            else
            {
                v = -1;
                h = 0;
            }
            changeDirectionTimeVal = 0;
        }
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[kinds + 24];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[kinds + 8];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
        if (h != 0) //����������һ�𰴷���Ư��
        {
            return;
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[kinds + 16];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[kinds];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }
    //̹�˵���������
    private void Die()
    {
        if (defendTimeVal > 0)
            return;
        if (enemyLives > 0)
        {
            enemyLives--;
            return;
        }
        PlayerManager.Instance.playerScore++;
        //������ըЧ��
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
    //�������˺��ϰ��ı䷽��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                changeDirectionTimeVal = 2;
                break;
            case "Barrier":
                changeDirectionTimeVal = 2;
                break;
            default:
                break;
        }
    }
}
