using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_M : MonoBehaviour
{
    public void Title_change()// 타이틀에서 게임 시작 버튼
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()// 게임 종료 버튼
    {
        Application.Quit();
    }
}
