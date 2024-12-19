using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 오브젝트 제자리에서 이동시키는 스크립트
public class OMove : MonoBehaviour
{
    public float obj_move = 0.05f;
    public GameObject target;
    Vector3 velo = Vector3.zero;
    Vector3 startP;
    public int point_number = 0;// 발판이 어디에 위치해 있는지 확인하는 용도의 변수

    public float Timer_obj;
    public int time_cnt = 0;
    public int Right_cnt;// 오른쪽
    public int Left_cnt;// 왼쪽

    // Start is called before the first frame update
    void Start()
    {
        startP = transform.position;// 처음 발판이 있던 자리를 저장
    }

    // Update is called once per frame
    void FixedUpdate()// 시간을 제대로 멈출려면 픽시드 업데이트 문에서 해야함!!
    {// 그냥 업데이트 문에 작성하면 타임.스케일이 0이어도 자기 혼자서 계속 돌아감
        Timer_obj += Time.deltaTime * 0.1f;
        move_obj();
    }

    void move_obj()
    {
        if (Left_cnt == 0 || Right_cnt == 1)// 왼쪽에서 오른쪽으로
        {
            this.transform.Translate(new Vector3(0, 0, Timer_obj));
            if (Timer_obj >= 0.3)
            {
                time_cnt = 1;
                Timer_obj = 0;
                Right_cnt = 0;
                Left_cnt = 1;
            }
        }
        if (Right_cnt == 0 || Left_cnt == 1)// 오른쪽에서 왼쪽으로
        {
            this.transform.Translate(new Vector3(0, 0, -Timer_obj));
            if (Timer_obj >= 0.3)
            {
                time_cnt = 0;
                Timer_obj = 0;
                Right_cnt = 1;
                Left_cnt = 0;
            }
        }
    }
}
