using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    private float rotSensitive = 3f;//ī�޶� ȸ�� ����
    public float dis = 5f;//ī�޶�� �÷��̾������ �Ÿ�
    private float RotationMin = -80f;//ī�޶� ȸ������ �ּ�
    private float RotationMax = 100f;//ī�޶� ȸ������ �ִ�
    private float smoothTime = 0.12f;//ī�޶� ȸ���ϴµ� �ɸ��� �ð�
    //�� 5���� value�� �������� ���ⲯ �˾Ƽ� ����������
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public float wheelI = 0f;// ���콺 �� ���ȴ���

    void LateUpdate()//Player�� �����̰� �� �� ī�޶� ���󰡾� �ϹǷ� LateUpdate
    {
        Rotate();// ī�޶� ȸ��
        Mouse_cnt();// ���콺 �� �� / �ƿ�
    }
    void Rotate()
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//���콺 �¿�������� �Է¹޾Ƽ� ī�޶��� Y���� ȸ����Ų��
        Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//���콺 ���Ͽ������� �Է¹޾Ƽ� ī�޶��� X���� ȸ����Ų��
        //Xaxis�� ���콺�� �Ʒ��� ������(�������� �Է� �޾�����) ���� �������� ī�޶� �Ʒ��� ȸ���Ѵ� 

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
        //X��ȸ���� �Ѱ�ġ�� �����ʰ� �������ش�.

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        this.transform.eulerAngles = targetRotation;
        //SmoothDamp�� ���� �ε巯�� ī�޶� ȸ��

        transform.position = target.position - transform.forward * dis;
        //ī�޶��� ��ġ�� �÷��̾�� ������ ����ŭ �������ְ� ��� ����ȴ�.
    }

    void Mouse_cnt()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (dis < 10)
            {
                wheelI = 1f;
                dis = dis + wheelI;// ���� ���� �ø��� ī�޶� �Ÿ��� ª����
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (dis > 0)
            {
                wheelI = -1f;
                dis = dis + wheelI;// ���� �Ʒ��� ���� ī�޶� �Ÿ��� ª������ 1��Ī�� ��
            }
        }
    }
}