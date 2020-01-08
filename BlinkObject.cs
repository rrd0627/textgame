using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkObject : MonoBehaviour
{
    WaitForSeconds waittime;
    Color _color;
    int plus_minus;

    private IEnumerator BlinkCor;

    // Start is called before the first frame update
    void Start()
    {
        _color = new Color(1, 1, 1, 1);
        plus_minus = -1;
        waittime = new WaitForSeconds(0.01f);
        BlinkCor = Blink();
        StartBlink();
    }
    IEnumerator Blink()
    {
        while(true)
        {
            _color.a += 0.03f * plus_minus;

            if (plus_minus == 1 && _color.a > 0.9f)
                plus_minus = -1;
            else if (plus_minus == -1 && _color.a < 0.1f)
                plus_minus = 1;

            this.GetComponent<Image>().color = _color;

            yield return waittime;
        }
    }
    public void StopBlink()
    {
        StopCoroutine(BlinkCor);
        _color = this.GetComponent<Image>().color;
        _color.a = 0;
        this.GetComponent<Image>().color = _color;
        plus_minus = 1;
    }
    public void StartBlink()
    {
        StartCoroutine(BlinkCor);
    }
}
