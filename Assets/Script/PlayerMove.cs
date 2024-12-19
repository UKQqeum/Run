using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]//인스펙터에서만 참조 가능하게
    private float smoothRotationTime;//target 각도로 회전하는데 걸리는 시간
    [SerializeField]
    private float smoothMoveTime;//target 속도로 바뀌는데 걸리는 시간
    [SerializeField]
    private float moveSpeed;//움직이는 속도
    private float rotationVelocity;//The current velocity, this value is modified by the function every time you call it.
    private float speedVelocity;//The current velocity, this value is modified by the function every time you call it.
    private float currentSpeed;
    private float targetSpeed;

    //public float moveSpeed;// 이동 속도
    private Rigidbody rb;
    public float jumpSpeed;// 점프 속도

    private int jumpcnt = 0;// 점프 카운트 연속 점프를 막기 위함

    private Transform cameraTrans;

    public float finish_time;
    public Text finish_text;

    public GameObject Finish_Panel;// 도착 판넬

    public GameObject Over_Panel;// 게임 오버 판넬
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
        //GetAxisRaw("Horizontal") :오른쪽 방향키누르면 1을 반환, 아무것도 안누르면 0, 왼쪽방향키는 -1 반환
        //GetAxis("Horizontal"):-1과 1 사이의 실수값을 반환
        //Vertical은 위쪽방향키 누를시 1,아무것도 안누르면 0, 아래쪽방향키는 -1 반환

        Vector2 inputDir = input.normalized;
        //벡터 정규화. 만약 input=new Vector2(1,1) 이면 오른쪽위 대각선으로 움직인다.
        //방향을 찾아준다

        if (inputDir != Vector2.zero)//움직임을 멈췄을 때 다시 처음 각도로 돌아가는걸 막기위함
        {
            float rotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraTrans.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, rotation, ref rotationVelocity, smoothRotationTime);
        }
        //각도를 구해주는 코드, 플레이어가 오른쪽 위 대각선으로 움직일시 그 방향을 바라보게 해준다
        //Mathf.Atan2는 라디안을 return하기에 다시 각도로 바꿔주는 Mathf.Rad2Deg를 곱해준다
        //Vector.up은 y axis를 의미한다
        //SmoothDampAngle을 이용해서 부드럽게 플레이어의 각도를 바꿔준다.

        targetSpeed = moveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedVelocity, smoothMoveTime);
        //현재스피드에서 타겟스피드까지 smoothMoveTime 동안 변한다
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcnt < 1)
            {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                jumpcnt++;// 점프를 한 번 했음
            }
        }
    }// 점프 관련

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Floor")// 바닥에 닿았을 때
        { 
            jumpcnt = 0;// 다시 점프를 할 수 있도록 점프 카운트를 초기화시켜줌
        }

        //if (coll.gameObject.tag == "Enemy")
        //{
        //    GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 3) * 3f, ForceMode.Impulse);// 오른쪽으로 밀려남
        //}
        if (coll.gameObject.tag == "REnemy")// 오른쪽에서 날아오는 공
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 20) * 2f, ForceMode.Impulse);// 오른쪽으로 밀려남
        }
        if (coll.gameObject.tag == "LEnemy")// 왼쪽에서 날아오는 공
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -20) * 2f, ForceMode.Impulse);// 오른쪽으로 밀려남
        }
        if (coll.gameObject.tag == "Fake")
        {
            coll.gameObject.SetActive(false);// 닿은 오브젝트의 태그가 Fake라면 오브젝트를 없애도록
        }
        if (coll.gameObject.tag == "Stair")// 닿은 오브젝트가 이것일 때
        {
            jumpcnt = 0;// 다시 점프를 할 수 있도록 점프 카운트를 초기화시켜줌
        }
        if (coll.gameObject.tag == "Finish")
        {
            Time.timeScale = 0;// 조작을 하지 못하도록
            Finish_Panel.SetActive(true);
            finish_text.text = (finish_time.ToString("F2") + "초 만에 게임을 클리어하셨습니다!");
        }
        if (coll.gameObject.tag == "Death")// 플레이어가 낙하하였을 때
        {
            Time.timeScale = 0;// 조작을 하지 못하도록
            Over_Panel.SetActive(true);
        }
    }
}
