using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Choice : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public Image Blink_object;
    public Image ChoiceImageOn;

    private GameObject Namepanel;
    private GameObject ScriptBox;
    private GameObject ScriptBox_prefab;

    private void Start()
    {
        BatteryControl(0);
    }
    public void Press_Button(int scriptBox_index)
    {
        ScriptData.instance.ChoiceBox.GetComponent<RectTransform>().localPosition = new Vector2(0, -1500);

        foreach (Transform child in ScriptData.instance.TextGroup.transform)
        {
            ScriptData.instance.ReadText.Remove(child.GetComponent<Text>());
            ScriptData.instance.Alltext.Remove(child.GetComponent<Text>());
            Destroy(child.gameObject);
        }
        string address = "ScriptBox/" + scriptBox_index;
        ScriptBox = Resources.Load(address) as GameObject;
        ScriptBox_prefab = Instantiate(ScriptBox, ScriptData.instance.TextGroup.transform);
        
        if (Blink_object != null)
            Blink_object.enabled = false;
    }
    private void Update()
    {
        if(GetComponent<Text>()!=null)
        {
            if (!ScriptData.instance.ColorFlip)
                GetComponent<Text>().color = ScriptData.instance.ChoiceColor;
            else
                GetComponent<Text>().color = ScriptData.instance.FlipColor;
        }
        if(ChoiceImageOn!=null)
        {
            //if(GetComponent<Button>().is)
        }

    }




    public void BatteryControl(int BatteryChangeAmount)
    {
        if (ScriptData.instance.Battery_Remain + BatteryChangeAmount > ScriptData.instance.Battery_Capacity)
            ScriptData.instance.Battery_Remain = ScriptData.instance.Battery_Capacity;
        ScriptData.instance.Battery_Remain += BatteryChangeAmount;
        ScriptData.instance.Battery_Text.text = ScriptData.instance.Battery_Remain + " / " + ScriptData.instance.Battery_Capacity;
        ScriptData.instance.Battery_Slider.value = (float)ScriptData.instance.Battery_Remain / ScriptData.instance.Battery_Capacity;

        if (ScriptData.instance.Battery_Slider.value > 0.7)ScriptData.instance.Battery_Text.color = Color.green;
        else if (ScriptData.instance.Battery_Slider.value > 0.3) ScriptData.instance.Battery_Text.color = Color.yellow;
        else if (ScriptData.instance.Battery_Slider.value > 0.0) ScriptData.instance.Battery_Text.color = Color.red;

        Color silderColor;
        float r,g;
        if (ScriptData.instance.Battery_Slider.value > 0.5f)
        {
            g = 1;
            r = 1f/(ScriptData.instance.Battery_Slider.value)-1f;   //value 1일때 0 value 0.5일때 1
            silderColor = new Color(r, g, 0);
            ScriptData.instance.Battery_FillSlider.color = silderColor;
        }
        else
        {
            r = 1;
            g = ScriptData.instance.Battery_Slider.value*2;  //value 0.5일때 1            value 0일때 0
            silderColor = new Color(r, g, 0);
            ScriptData.instance.Battery_FillSlider.color = silderColor;
        }
            

    }
    public void GetItem(int ItemIndex)
    {
        string address = "Item/" + ItemIndex;
        ScriptBox = Resources.Load(address) as GameObject;
        ScriptBox_prefab = Instantiate(ScriptBox, ScriptData.instance.ItemGroup.transform);

    }
    public void Press_End()
    {
        ScriptData.instance.Press_End(this.transform);
    }
    public void SetName(GameObject namepanel)
    {
        Namepanel = Instantiate(namepanel,this.transform.parent.parent);
        Namepanel.SetActive(true);
        GetComponent<Button>().enabled = false;
    }
    public void StrToName(Text text)
    {
        if(text.text.Length==0||text.text.Length>10)
        {
            GetComponentInChildren<Text>().text = "1~10글자 내로 입력해주세요";
        }
        else
        {
            ScriptData.instance.Name = text.text;
            Press_Button(3);
            Destroy(Namepanel);
        }     
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(ChoiceImageOn!=null)
            ChoiceImageOn.GetComponent<Image>().enabled = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (ChoiceImageOn != null)
            ChoiceImageOn.GetComponent<Image>().enabled = false;
    }
}
