using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision coll)// 닿으면 사라지는 발판 이벤트
    {
        if (coll.gameObject.tag == "Player")// 플레이어가
        {
            if (this.gameObject.tag == "Enemy")// 바닥 에너미에 닿았을 때
            {
                this.gameObject.SetActive(false);
            }// 바닥 발판 관련
        }
    }
}
