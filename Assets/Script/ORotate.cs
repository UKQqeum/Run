using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 오브젝트 제자리에서 회전시키는 스크립트
public class ORotate : MonoBehaviour
{
    public float obj_time = 20f;// 오브젝트가 돌아가는 시간

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * Time.deltaTime * obj_time);
    }
}
