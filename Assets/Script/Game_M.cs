using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_M : MonoBehaviour
{
    public void Title_change()// Ÿ��Ʋ���� ���� ���� ��ư
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Exit()// ���� ���� ��ư
    {
        Application.Quit();
    }
}
