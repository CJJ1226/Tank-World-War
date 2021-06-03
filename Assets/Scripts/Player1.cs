using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    //��������ֵ
    const float defendTime = 3f;
    //�ɱ�����ֵ
    public float moveSpeed = 3;
    public bool isPlayer1;

    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float defendTimeVal = defendTime;

    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite; //�� �� �� ��
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
        //����CD
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
    //̹�˵Ĺ�������
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
    //̹�˵��ƶ�����
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
        if (h != 0) //����������һ�𰴷���Ư��
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
    //̹�˵���������
    private void Die()
    {
        if (defendTimeVal > 0)
            return;

        if(isPlayer1)
            PlayerManager.Instance.isDead1 = true;
        else
            PlayerManager.Instance.isDead2 = true;

        //������ըЧ��
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
}
