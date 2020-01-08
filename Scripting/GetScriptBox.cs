using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//맨처음 texting 시작을 위해서 쓰는 스크립트
public class GetScriptBox : MonoBehaviour
{
    public GameObject TextBox;
    private GameObject ScriptBox;
    private GameObject ScriptBox_prefab;
    void Start()
    {
        //waittime = new WaitForSeconds(0.01f);
        //StartCoroutine(TextBoxStrech());

        string FirstScriptPath = "ScriptBox/" + ScriptData.instance.cur_text;
        ScriptBox = Resources.Load(FirstScriptPath) as GameObject;
        ScriptBox_prefab = Instantiate(ScriptBox, this.transform);
    }
    //IEnumerator TextBoxStrech()
    //{
    //    RectTransform rect_tran = (RectTransform)TextBox.transform;
    //    Vector2 textbos_scale = rect_tran.sizeDelta;

    //    while (rect_tran.sizeDelta.y < 1690)
    //    {
    //        rect_tran.sizeDelta = textbos_scale;
    //        textbos_scale.y += strech_speed;
    //        yield return waittime;
    //    }
        
    //}

}
