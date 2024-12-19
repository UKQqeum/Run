using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float time_limit = 0f;// 게임 시작부터 걸리는 시간
    public Text time_text;// 게임 시작부터 걸리는 시간을 보여줄 텍스트

    public GameObject ESC_Panel;// ESC 판넬

    // Start is called before the first frame update
    void Start()
    {
        time_text = GameObject.Find("time_text").GetComponent<Text>();
        ESC_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))// ESC 키를 눌렀을 때
        {
            ESC_Panel.SetActive(true);
            Time.timeScale = 0;// 조작을 하지 못하도록
        }

        time_limit += Time.deltaTime;// 게임 시작 시 시간을 계속 더해주기
        time_text.text = time_limit.ToString("F2");// F2를 넣어주어서 소수점 자르기
    }
    public void game_ESC()// ESC 버튼을 눌렀을 때
    {
        if (Input.GetKeyDown(KeyCode.Escape))// ESC 키를 눌렀을 때
        {
            ESC_Panel.SetActive(true);
            Time.timeScale = 0;// 조작을 하지 못하도록
        }
    }
    public void game_re()// 게임 계속하기 버튼
    {
        Time.timeScale = 1;// 게임 다시 조작할 수 있도록
        ESC_Panel.SetActive(false);
    }
    public void game_Exit()// 게임 종료 버튼
    {
        Application.Quit();
    }
    public void Restart()// 타이틀에서 게임 시작 버튼
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void go_Title()// 타이틀에서 게임 시작 버튼
    {
        SceneManager.LoadScene("Title");
    }
}
