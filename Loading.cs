using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    public Slider progressBar;

    public Image panel;

    public Text ready_text;

    int flag;

    Color _color;
    Color text_color;
    WaitForSeconds waittime;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(720, 1280, true);
        Application.targetFrameRate = 60;
    }
    private void Start()
    {
        _color = new Color(0, 0, 0, 0);
        text_color = ready_text.color;
        ready_text.text = "Loading ...";
        flag = 1;
        waittime = new WaitForSeconds(0.01f);
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation oper = new AsyncOperation();

        oper = SceneManager.LoadSceneAsync("Main");

        oper.allowSceneActivation = false;

        float timer = 0.0f;
        while (!oper.isDone)
        {
            yield return null;

            timer += Time.deltaTime;

            if (oper.progress >= 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);

                if (progressBar.value == 1.0f)
                {
                    ready_text.text = "시작하려면 아무곳이나 누르세요";

                    while (true)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            break;
                        }
                        yield return waittime;

                        text_color.a -= flag * Time.deltaTime;
                        ready_text.color = text_color;
                        if (text_color.a < 0 || text_color.a > 1)
                        {
                            flag *= -1;
                            text_color.a -= flag * Time.deltaTime;
                        }
                    }
                    while (_color.a < 1)
                    {
                        _color.a += 3f * Time.deltaTime;
                        panel.color = _color;
                        yield return new WaitForSeconds(Time.deltaTime);
                    }
                    oper.allowSceneActivation = true;
                }
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, oper.progress, timer);
                if (progressBar.value >= oper.progress)
                {
                    timer = 0f;
                }
            }
        }
    }
}