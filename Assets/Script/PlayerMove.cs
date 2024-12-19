using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]//�ν����Ϳ����� ���� �����ϰ�
    private float smoothRotationTime;//target ������ ȸ���ϴµ� �ɸ��� �ð�
    [SerializeField]
    private float smoothMoveTime;//target �ӵ��� �ٲ�µ� �ɸ��� �ð�
    [SerializeField]
    private float moveSpeed;//�����̴� �ӵ�
    private float rotationVelocity;//The current velocity, this value is modified by the function every time you call it.
    private float speedVelocity;//The current velocity, this value is modified by the function every time you call it.
    private float currentSpeed;
    private float targetSpeed;

    //public float moveSpeed;// �̵� �ӵ�
    private Rigidbody rb;
    public float jumpSpeed;// ���� �ӵ�

    private int jumpcnt = 0;// ���� ī��Ʈ ���� ������ ���� ����

    private Transform cameraTrans;

    public float finish_time;
    public Text finish_text;

    public GameObject Finish_Panel;// ���� �ǳ�

    public GameObject Over_Panel;// ���� ���� �ǳ�
    //private float rotationVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTrans = Camera.main.transform;

        finish_text = GameObject.Find("finish_text").GetComponent<Text>();
        Finish_Panel.SetActive(false);
        Over_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        finish_time += Time.deltaTime;
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //GetAxisRaw("Horizontal") :������ ����Ű������ 1�� ��ȯ, �ƹ��͵� �ȴ����� 0, ���ʹ���Ű�� -1 ��ȯ
        //GetAxis("Horizontal"):-1�� 1 ������ �Ǽ����� ��ȯ
        //Vertical�� ���ʹ���Ű ������ 1,�ƹ��͵� �ȴ����� 0, �Ʒ��ʹ���Ű�� -1 ��ȯ

        Vector2 inputDir = input.normalized;
        //���� ����ȭ. ���� input=new Vector2(1,1) �̸� �������� �밢������ �����δ�.
        //������ ã���ش�

        if (inputDir != Vector2.zero)//�������� ������ �� �ٽ� ó�� ������ ���ư��°� ��������
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref rotationVelocity, smoothRotationTime);
        }
        //������ �����ִ� �ڵ�, �÷��̾ ������ �� �밢������ �����Ͻ� �� ������ �ٶ󺸰� ���ش�
        //Mathf.Atan2�� ������ return�ϱ⿡ �ٽ� ������ �ٲ��ִ� Mathf.Rad2Deg�� �����ش�
        //Vector.up�� y axis�� �ǹ��Ѵ�
        //SmoothDampAngle�� �̿��ؼ� �ε巴�� �÷��̾��� ������ �ٲ��ش�.

        targetSpeed = moveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothMoveTime);
        //���罺�ǵ忡�� Ÿ�ٽ��ǵ���� smoothMoveTime ���� ���Ѵ�
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcnt < 1)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                jumpcnt++;// ������ �� �� ����
            }
        }
    }// ���� ����

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Floor")// �ٴڿ� ����� ��
        { 
            jumpcnt = 0;// �ٽ� ������ �� �� �ֵ��� ���� ī��Ʈ�� �ʱ�ȭ������
        }

        //if (coll.gameObject.tag == "Enemy")
        //{
        //    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 3) * 3f, ForceMode.Impulse);// ���������� �з���
        //}
        if (coll.gameObject.tag == "REnemy")// �����ʿ��� ���ƿ��� ��
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 20) * 2f, ForceMode.Impulse);// ���������� �з���
        }
        if (coll.gameObject.tag == "LEnemy")// ���ʿ��� ���ƿ��� ��
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -20) * 2f, ForceMode.Impulse);// ���������� �з���
        }
        if (coll.gameObject.tag == "Fake")
        {
            coll.gameObject.SetActive(false);// ���� ������Ʈ�� �±װ� Fake��� ������Ʈ�� ���ֵ���
        }
        if (coll.gameObject.tag == "Stair")// ���� ������Ʈ�� �̰��� ��
        {
            jumpcnt = 0;// �ٽ� ������ �� �� �ֵ��� ���� ī��Ʈ�� �ʱ�ȭ������
        }
        if (coll.gameObject.tag == "Finish")
        {
            Time.timeScale = 0;// ������ ���� ���ϵ���
            Finish_Panel.SetActive(true);
            finish_text.text = (finish_time.ToString("F2") + "�� ���� ������ Ŭ�����ϼ̽��ϴ�!");
        }
        if (coll.gameObject.tag == "Death")// �÷��̾ �����Ͽ��� ��
        {
            Time.timeScale = 0;// ������ ���� ���ϵ���
            Over_Panel.SetActive(true);
        }
    }
}
