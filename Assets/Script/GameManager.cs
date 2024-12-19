using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float time_limit = 0f;// ���� ���ۺ��� �ɸ��� �ð�
    public Text time_text;// ���� ���ۺ��� �ɸ��� �ð��� ������ �ؽ�Ʈ

    public GameObject ESC_Panel;// ESC �ǳ�

    // Start is called before the first frame update
    void Start()
    {
        time_text = GameObject.Find("time_text").GetComponent<Text>();
        ESC_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))// ESC Ű�� ������ ��
        {
            ESC_Panel.SetActive(true);
            Time.timeScale = 0;// ������ ���� ���ϵ���
        }

        time_limit += Time.deltaTime;// ���� ���� �� �ð��� ��� �����ֱ�
        time_text.text = time_limit.ToString("F2");// F2�� �־��־ �Ҽ��� �ڸ���
    }
    public void game_ESC()// ESC ��ư�� ������ ��
    {
        if (Input.GetKeyDown(KeyCode.Escape))// ESC Ű�� ������ ��
        {
            ESC_Panel.SetActive(true);
            Time.timeScale = 0;// ������ ���� ���ϵ���
        }
    }
    public void game_re()// ���� ����ϱ� ��ư
    {
        Time.timeScale = 1;// ���� �ٽ� ������ �� �ֵ���
        ESC_Panel.SetActive(false);
    }
    public void game_Exit()// ���� ���� ��ư
    {
        Application.Quit();
    }
    public void Restart()// Ÿ��Ʋ���� ���� ���� ��ư
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void go_Title()// Ÿ��Ʋ���� ���� ���� ��ư
    {
        SceneManager.LoadScene("Title");
    }
}
