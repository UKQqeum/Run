using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision coll)// ������ ������� ���� �̺�Ʈ
    {
        if (coll.gameObject.tag == "Player")// �÷��̾
        {
            if (this.gameObject.tag == "Enemy")// �ٴ� ���ʹ̿� ����� ��
            {
                this.gameObject.SetActive(false);
            }// �ٴ� ���� ����
        }
    }
}
