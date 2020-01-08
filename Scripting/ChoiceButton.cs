using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//버튼 누르는 스크립트
public class ChoiceButton : MonoBehaviour
{
    private WaitForSeconds waittime;


    private void Start()
    {      
        waittime = new WaitForSeconds(0.01f);
    }
   
    public void ColorFlip(bool flip)
    {
        ScriptData.instance.ColorFlip = flip;

        if(ScriptData.instance.ColorFlip)
        {
            for (int i = 0; i < ScriptData.instance.Alltext.Count; i++)
                ScriptData.instance.Alltext[i].color = ScriptData.instance.FlipColor;
            for (int i = 0; i < ScriptData.instance.AllImage.Count; i++)
                ScriptData.instance.AllImage[i].color = Color.white;
            ScriptData.instance.ChoiceBox.GetComponent<Image>().sprite = Resources.Load<Sprite>("Choice/ChoiceWhite") as Sprite;
        }
        else
        {
            for (int i = 0; i < ScriptData.instance.Alltext.Count; i++)
                ScriptData.instance.Alltext[i].color = ScriptData.instance.OriginColor;
            for (int i = 0; i < ScriptData.instance.AllImage.Count; i++)
                ScriptData.instance.AllImage[i].color = Color.black;
            ScriptData.instance.ChoiceBox.GetComponent<Image>().sprite = Resources.Load<Sprite>("Choice/ChoiceBlack") as Sprite;
        }
    }

    public void EndingCondition(int EndingIndex) //엔딩 버튼 눌렀을때 팝업
    {
        ScriptData.instance.Ending_panel.SetActive(true);

        string address_picture = "Ending/" + EndingIndex;        

        switch (EndingIndex)
        {
            case 0:
                ScriptData.instance.Ending_picture.sprite = Resources.Load<Sprite>(address_picture) as Sprite;
                ScriptData.instance.Ending_Title.text = "Kogi";
                ScriptData.instance.Ending_MainText.text = "It is hard. isn't it?";
                break;
            case 1:
                ScriptData.instance.Ending_picture.sprite = Resources.Load<Sprite>(address_picture) as Sprite;
                ScriptData.instance.Ending_Title.text = "MINJAE";
                ScriptData.instance.Ending_MainText.text = "is it work well?";
                break;
            case 2:
                ScriptData.instance.Ending_picture.sprite = Resources.Load<Sprite>(address_picture) as Sprite;
                ScriptData.instance.Ending_Title.text = "Kogi";
                ScriptData.instance.Ending_MainText.text = "It is hard. isn't it?";
                break;
        }
    }
    public void EndingBack()
    {
        ScriptData.instance.Ending_panel.SetActive(false);
    }
    public void MainMenu()
    {
        StopAllCoroutines();

        ScriptData.instance.Menu_Items.SetActive(true);
        ScriptData.instance.MenuText.text = "아이템";
        ScriptData.instance.option.SetActive(false);
        ScriptData.instance.Extra.SetActive(false);

        StartCoroutine(SlideToMenuCor());
    }
    public void MenuToMain()
    {
        StopAllCoroutines();

        ScriptData.instance.Menu_Items.SetActive(false);
        ScriptData.instance.option.SetActive(false);
        ScriptData.instance.Extra.SetActive(false);

        StartCoroutine(SlideToMainCor());
    }
    public void SlideOption()
    {
        StopAllCoroutines();

        ScriptData.instance.option.SetActive(true);
        ScriptData.instance.MenuText.text = "Setting";
        ScriptData.instance.Menu_Items.SetActive(false);
        ScriptData.instance.Extra.SetActive(false);

        StartCoroutine(SlideToMenuCor());
    }
    public void ExtraButton()
    {
        ScriptData.instance.Extra.SetActive(true);
        ScriptData.instance.MenuText.text = "Extra";
        ScriptData.instance.Menu_Items.SetActive(false);
        ScriptData.instance.option.SetActive(false);
    }
    public void ExtraToMenu()
    {
        ScriptData.instance.Extra.SetActive(false);
        ScriptData.instance.MenuText.text = "Setting";
        ScriptData.instance.Menu_Items.SetActive(false);
        ScriptData.instance.option.SetActive(true);
    }
    IEnumerator SlideToMenuCor()
    {
        Vector2 pos_text = new Vector2(-1080, 0);
        Vector2 menu_text = new Vector2(0, 0);

        while (ScriptData.instance.main_text.transform.localPosition.x > -1079.9 || ScriptData.instance.menu_text.transform.localPosition.x > 0.1)
        {
            ScriptData.instance.main_text.transform.localPosition = Vector2.Lerp(ScriptData.instance.main_text.transform.localPosition, pos_text, 10 * Time.deltaTime);
            ScriptData.instance.menu_text.transform.localPosition = Vector2.Lerp(ScriptData.instance.menu_text.transform.localPosition, menu_text, 10 * Time.deltaTime);

            yield return waittime;
        }
    }
    IEnumerator SlideToMainCor()
    {        
        Vector2 pos_text = new Vector2(0, 0);
        Vector2 menu_text = new Vector2(1080, 0);

        while (ScriptData.instance.main_text.transform.localPosition.x < -0.1 || ScriptData.instance.menu_text.transform.localPosition.x < 1079.9)
        {
            ScriptData.instance.main_text.transform.localPosition = Vector2.Lerp(ScriptData.instance.main_text.transform.localPosition, pos_text, 10 * Time.deltaTime);
            ScriptData.instance.menu_text.transform.localPosition = Vector2.Lerp(ScriptData.instance.menu_text.transform.localPosition, menu_text, 10 * Time.deltaTime);
            yield return waittime;
        }       
    }
   
    public void GoToButton(Scrollbar scrollbar)
    {
        if(scrollbar.value>0.1f)
        {
            ScriptData.instance.DownButton.GetComponent<Image>().enabled = true;
            ScriptData.instance.DownButton.GetComponent<Button>().enabled = true;
        }            
        else
        {
            ScriptData.instance.DownButton.GetComponent<Image>().enabled = false;
            ScriptData.instance.DownButton.GetComponent<Button>().enabled = false;
        }            
    }
    public void GoToBottom()
    {
        ScriptData.instance.scroll_bar.value = 0;
    }
    public void SetTextSize(Slider TextSizeSlider)
    {
        for (int i = 0; i < ScriptData.instance.ReadText.Count; i++)
        {
            ScriptData.instance.ReadText[i].fontSize = (int)(TextSizeSlider.value * 35) + 25;   //25~60   35
        }        
        ScriptData.instance.Fontsize = (int)(TextSizeSlider.value * 35) + 25;
    }
    public void SetTextDist(Slider TextDistSlider)
    {
        for (int i = 0; i < ScriptData.instance.ReadText.Count; i++)
        {
            ScriptData.instance.ReadText[i].lineSpacing = TextDistSlider.value+1;   //1~2
        }
        ScriptData.instance.FontDist = TextDistSlider.value + 1;
    }

    public void SetTypeEffect(bool seteffect)
    {
        ScriptData.instance.TypingEffect = seteffect;
    }

    
    public void SetVibrate(bool setvibe)
    {
        ScriptData.instance.Vibrate = setvibe;
        if(setvibe)
        {
            //Handheld.Vibrate();
            //진동
        }
    }
    public void TextGoFirst()
    {
        ScriptData.instance.Fontsize = 48;
        ScriptData.instance.FontDist = 1.5f;
        ScriptData.instance.FontDist_Slider.value = ScriptData.instance.FontDist - 1;
        ScriptData.instance.FontSize_Slider.value = (ScriptData.instance.Fontsize - 25) / 35f;
    }
    public void DataInitalize()
    {
        ScriptData.instance.SetDefaultSetting();
        ScriptData.instance.SaveData();
        ScriptData.instance.GoToFirstScript();
    }
    
    public void IsQuit()
    {
        Application.Quit();
    }
    public void IsQuitNo(GameObject QuitPanel)
    {
        QuitPanel.SetActive(false);
    }

    public void Gothic()
    {
        ScriptData.instance.font = Resources.Load<Font>("Font/NanumGothic");

        for(int i=0;i<ScriptData.instance.Alltext.Count;i++)
        {
            ScriptData.instance.Alltext[i].font = ScriptData.instance.font;            
        }
        for(int i = 0; i < ScriptData.instance.ReadText.Count; i++)
        {
            ScriptData.instance.ReadText[i].font = ScriptData.instance.font;
        }
        ScriptData.instance.is_gothic = true;
}
    public void Myeongjo()
    {
        ScriptData.instance.font = Resources.Load<Font>("Font/NanumMyeongjo");

        for (int i = 0; i < ScriptData.instance.Alltext.Count; i++)
        {
            ScriptData.instance.Alltext[i].font = ScriptData.instance.font;
        }
        for (int i = 0; i < ScriptData.instance.ReadText.Count; i++)
        {
            ScriptData.instance.ReadText[i].font = ScriptData.instance.font;
        }
        ScriptData.instance.is_gothic = false;
    }    
}
