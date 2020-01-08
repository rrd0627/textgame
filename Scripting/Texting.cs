using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//글씨가 나오도록 하는 스크립트
public class Texting : MonoBehaviour
{
    public int[] Choice;
    public GameObject childButton;
    public Image childImage;
    private string text_buff;
    private int index;
    public int scriptindex;
    private bool Texting_done;
    private bool Choice_done;
    WaitForSeconds waittime;
    WaitForSeconds waittime_short;
    private Vector2 Choice_pos;
    // Start is called before the first frame update
    void Start()
    {
        ScriptData.instance.cur_text = int.Parse(this.gameObject.name.Substring(0,this.gameObject.name.Length-7));
        ScriptData.instance.SaveData();
        Texting_done = false;
        Choice_done = false;
        ScriptData.instance.IsTouched = false;
        Choice_pos = new Vector2(0, -965);
        foreach (Transform child in ScriptData.instance.ChoiceBox.transform)
        {
            Destroy(child.gameObject);
        }
        ScriptData.instance.ChoiceBox.SetActive(false);
        if (scriptindex > 0)
            text_buff = ScriptData.instance.GetScript(scriptindex);
        else
            text_buff = GetComponent<Text>().text;
        GetComponent<Text>().text = "";
        if (ScriptData.instance.ColorFlip)
            GetComponent<Text>().color = ScriptData.instance.FlipColor;
        else
            GetComponent<Text>().color = ScriptData.instance.OriginColor;
        GetComponent<Text>().font = ScriptData.instance.font;

        GetComponent<Text>().fontSize = ScriptData.instance.Fontsize;
        GetComponent<Text>().lineSpacing = ScriptData.instance.FontDist;

        ScriptData.instance.Alltext.Add(GetComponent<Text>());
        ScriptData.instance.ReadText.Add(GetComponent<Text>());

        waittime = new WaitForSeconds(0.03f);
        waittime_short = new WaitForSeconds(0.01f);
        index = 0;
        StartCoroutine(Scripting());
    }

    IEnumerator Scripting()
    {
        while (index < text_buff.Length)
        {
            if (ScriptData.instance.IsTouched || !ScriptData.instance.TypingEffect)
            {
                ScriptData.instance.IsTouched = false;
                GetComponent<Text>().text = text_buff;
                break;
            }                
            GetComponent<Text>().text += text_buff[index++];
            
            yield return waittime;
        }
        yield return waittime;
        Texting_done = true;

        if(Choice.Length==0)
        {
            if (childButton != null)
            {
                childButton.SetActive(true);
            }
            if (childImage!=null)
            {
                childImage.enabled = true;
            }
        }
        GetComponent<ContentSizeFitter>().enabled = false;
        yield return waittime;
        GetComponent<ContentSizeFitter>().enabled = true;
    }
    private void Update()
    {
        if (Texting_done&&!Choice_done)
        {
            if (ScriptData.instance.scroll_bar.IsActive())
            {
                if(ScriptData.instance.scroll_bar.value < 0)
                {
                    Choice_done = true;
                    string address;
                    GameObject Choice_Box;
                    GameObject Choice_Box_prefab;
                    if (Choice.Length > 0)
                    {
                        ScriptData.instance.ChoiceBox.SetActive(true);
                        for (int i = 0; i < Choice.Length; i++)
                        {
                            address = "Choice/" + Choice[i];
                            Choice_Box = Resources.Load(address) as GameObject;
                            Choice_Box_prefab = Instantiate(Choice_Box, ScriptData.instance.ChoiceBox.transform);
                        }
                        StartCoroutine(SizeFit());
                    }
                }
            }
            else
            {
                if (ScriptData.instance.TextGroup.transform.localPosition.y > 1||Input.GetMouseButton(0))
                {
                    Choice_done = true;
                    string address;
                    GameObject Choice_Box;
                    GameObject Choice_Box_prefab;
                    if (Choice.Length > 0)
                    {                        
                        ScriptData.instance.ChoiceBox.SetActive(true);
                        for (int i = 0; i < Choice.Length; i++)
                        {
                            address = "Choice/" + Choice[i];
                            Choice_Box = Resources.Load(address) as GameObject;
                            Choice_Box_prefab = Instantiate(Choice_Box, ScriptData.instance.ChoiceBox.transform);
                        }
                        StartCoroutine(SizeFit());
                    }
                }
            }
        }
        if(Choice_done)
        {
             ScriptData.instance.ChoiceBox.transform.localPosition = Vector2.Lerp(ScriptData.instance.ChoiceBox.transform.localPosition, Choice_pos, Time.deltaTime*10);
        }
    }
    IEnumerator SizeFit()
    {
        ScriptData.instance.ChoiceBox.GetComponent<ContentSizeFitter>().enabled = false;        
        yield return waittime;
        ScriptData.instance.ChoiceBox.GetComponent<ContentSizeFitter>().enabled = true;
        yield return waittime_short;

        //Vector2 Goal_Pos = new Vector2(0, 0);

        //while (ScriptData.instance.ChoiceBox.transform.localPosition.y<=-960.001) //얼마나 올라오는지
        //{
        //    ScriptData.instance.ChoiceBox.transform.Translate(Vector2.up*Time.deltaTime*10);
        //}            
    }
}
