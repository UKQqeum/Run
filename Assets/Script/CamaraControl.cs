using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public float Yaxis;
    public float Xaxis;

    public Transform target;//Player

    private float rotSensitive = 3f;//카메라 회전 감도
    public float dis = 5f;//카메라와 플레이어사이의 거리
    private float RotationMin = -80f;//카메라 회전각도 최소
    private float RotationMax = 100f;//카메라 회전각도 최대
    private float smoothTime = 0.12f;//카메라가 회전하는데 걸리는 시간
    //위 5개의 value는 개발자의 취향껏 알아서 설정해주자
    private Vector3 targetRotation;
    private Vector3 currentVel;

    public float wheelI = 0f;// 마우스 휠 돌렸는지

    void LateUpdate()//Player가 움직이고 그 후 카메라가 따라가야 하므로 LateUpdate
    {
        Rotate();// 카메라 회전
        Mouse_cnt();// 마우스 줌 인 / 아웃
    }
    void Rotate()
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;//마우스 좌우움직임을 입력받아서 카메라의 Y축을 회전시킨다
        Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;//마우스 상하움직임을 입력받아서 카메라의 X축을 회전시킨다
        //Xaxis는 마우스를 아래로 했을때(음수값이 입력 받아질때) 값이 더해져야 카메라가 아래로 회전한다 

        Xaxis = Mathf.Clamp(Xaxis, RotationMin, RotationMax);
        //X축회전이 한계치를 넘지않게 제한해준다.

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        this.transform.eulerAngles = targetRotation;
        //SmoothDamp를 통해 부드러운 카메라 회전

        transform.position = target.position - transform.forward * dis;
        //카메라의 위치는 플레이어보다 설정한 값만큼 떨어져있게 계속 변경된다.
    }

    void Mouse_cnt()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (dis < 10)
            {
                wheelI = 1f;
                dis = dis + wheelI;// 휠을 위로 올리면 카메라 거리가 짧아짐
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (dis > 0)
            {
                wheelI = -1f;
                dis = dis + wheelI;// 휠을 아래로 당기면 카메라 거리가 짧아져서 1인칭이 됨
            }
        }
    }
}