using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������Ʈ ���ڸ����� �̵���Ű�� ��ũ��Ʈ
public class OMove : MonoBehaviour
{
    public float obj_move = 0.05f;
    public GameObject target;
    Vector3 velo = Vector3.zero;
    Vector3 startP;
    public int point_number = 0;// ������ ��� ��ġ�� �ִ��� Ȯ���ϴ� �뵵�� ����

    public float Timer_obj;
    public int time_cnt = 0;
    public int Right_cnt;// ������
    public int Left_cnt;// ����

    // Start is called before the first frame update
    void Start()
    {
        startP = transform.position;// ó�� ������ �ִ� �ڸ��� ����
    }

    // Update is called once per frame
    void FixedUpdate()// �ð��� ����� ������� �Ƚõ� ������Ʈ ������ �ؾ���!!
    {// �׳� ������Ʈ ���� �ۼ��ϸ� Ÿ��.�������� 0�̾ �ڱ� ȥ�ڼ� ��� ���ư�
        Timer_obj += Time.deltaTime * 0.1f;
        move_obj();
    }

    void move_obj()
    {
        if (Left_cnt == 0 || Right_cnt == 1)// ���ʿ��� ����������
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
        if (Right_cnt == 0 || Left_cnt == 1)// �����ʿ��� ��������
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
