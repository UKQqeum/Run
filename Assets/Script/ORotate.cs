using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������Ʈ ���ڸ����� ȸ����Ű�� ��ũ��Ʈ
public class ORotate : MonoBehaviour
{
    public float obj_time = 20f;// ������Ʈ�� ���ư��� �ð�

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * Time.deltaTime * obj_time);
    }
}
