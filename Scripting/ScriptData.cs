using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//데이터 메니져
public class ScriptData : MonoBehaviour
{
    static public ScriptData instance;

    public string Name;

    public List<Text> Alltext;
    public List<Image> AllImage;

    public List<Text> ReadText;

    [HideInInspector]
    public Font font;
    public Toggle gothic;
    public Toggle myeongjo;
    [HideInInspector]
    public bool is_gothic;

    public GameObject ChoiceBox;

    public int Fontsize;
    public float FontDist;

    public bool TypingEffect;
    public bool Vibrate;

    public GameObject Second_text;

    public Color OriginColor;
    public Color FlipColor;
    public Color ChoiceColor;

    public Text MenuText;
    //second Text
    public GameObject main_text;
    public GameObject menu_text;
    public GameObject option;
    public GameObject Extra;

    public GameObject TextGroup;

    public GameObject DownButton;

    public Scrollbar scroll_bar;

    public Slider Battery_Slider;
    public Image Battery_FillSlider;
    public Text Battery_Text;
    [HideInInspector]
    public int Battery_Capacity;
    [HideInInspector]
    public int Battery_Remain;

    public GameObject ItemGroup;

    public bool IsTouched=false;

    public GameObject Menu_Items;

    //thrid Option
    public bool ColorFlip;

    public Slider FontSize_Slider;
    public Slider FontDist_Slider;

    //fourth Extra
    public GameObject Ending_panel;
    public Text Ending_Title;
    public Text Ending_MainText;
    public Image Ending_picture;

    public int cur_text;

    public GameObject Is_Quit;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }

        if (ES3.KeyExists("UserName") == false)
        {
            SetDefaultSetting();
            SaveData();
        }
        else
        {
            LoadData();
        }

    }  //--------------인스턴스화를 위함 ----


    private GameObject temp;
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Is_Quit.SetActive(true);
            }
        }
    }

    public void Press_End(Transform lastOne)
    {
        Transform _parent = lastOne.parent.parent;
        foreach (Transform child in _parent)
        {
            Destroy(child.gameObject);
        }
    }
    public void SetDefaultSetting()
    {
        cur_text = 1;
        Battery_Capacity = 40;
        Battery_Remain = 40;
        Fontsize = 48;
        FontDist = 1.5f;
        TypingEffect = true;

        FontDist_Slider.value = FontDist -1;
        FontSize_Slider.value = (Fontsize - 25) / 35f;

        OriginColor = new Color(0xE5 / 255f, 0xe9 / 255f, 0xe5 / 255f);
        FlipColor = new Color(0x0 / 255f, 0x0 / 255f, 0x0 / 255f);
        ChoiceColor = new Color(0xf2 / 255f, 0xfb / 255f, 0xf2 / 255f);

        font = Resources.Load<Font>("Font/NanumGothic");
        is_gothic = true;
    }
    public string GetScript(int index)
    {
        string ret="";
        switch(index)
        {
            case 3:
                ret = "\n내 이름은... <" + ScriptData.instance.Name + "> 이었지 \n\n'나는 왜 이곳에 있는걸까?'\n'내가 깨어난 이유는 뭐지?'\n적응하기 위해 나는 몸을 움직여보기 시작했다. 몸속에 윤활유가 돌아가듯 점점 몸의 움직임은 자연스러워지기 시작했다. 나는 이제 뭘 해야 할까?";
                break;
        }
        return ret;
    }

    public void TouchScreen()
    {
        IsTouched = true;
    }
    public void GoToFirstScript()
    {
        ChoiceBox.GetComponent<RectTransform>().localPosition = new Vector2(0, -1500);

        foreach (Transform child in TextGroup.transform)
        {
            ReadText.Remove(child.GetComponent<Text>());
            Alltext.Remove(child.GetComponent<Text>());
            Destroy(child.gameObject);
        }
        string address = "ScriptBox/1";
        GameObject ScriptBox = Resources.Load(address) as GameObject;
        GameObject ScriptBox_prefab = Instantiate(ScriptBox, TextGroup.transform);
    }

    public void SaveData()
    {
        ES3.Save<string>("UserName", Name);

        ES3.Save<int>("Battery_Capacity", Battery_Capacity);
        ES3.Save<int>("Battery_Remain", Battery_Remain);

        ES3.Save<int>("Fontsize", Fontsize);
        ES3.Save<float>("FontDist", FontDist);
        ES3.Save<bool>("TypingEffect", TypingEffect);

        ES3.Save<int>("cur_text", cur_text);
        ES3.Save<Font>("font", font);
        ES3.Save<bool>("is_gothic", is_gothic);
    }
    public void LoadData()
    {
        if(ES3.KeyExists("UserName") == false)
            ES3.Save<string>("UserName", Name);
        Name = ES3.Load<string>("UserName");

        if (ES3.KeyExists("Battery_Capacity") == false)
            ES3.Save<int>("Battery_Capacity", Battery_Capacity);
        Battery_Capacity = ES3.Load<int>("Battery_Capacity");
        
        if (ES3.KeyExists("Battery_Remain") == false)
            ES3.Save<int>("Battery_Remain", Battery_Remain);
        Battery_Remain = ES3.Load<int>("Battery_Remain");

        if (ES3.KeyExists("Fontsize") == false)
            ES3.Save<int>("Fontsize", Fontsize);
        Fontsize = ES3.Load<int>("Fontsize");

        if (ES3.KeyExists("FontDist") == false)
            ES3.Save<float>("FontDist", FontDist);
        FontDist = ES3.Load<float>("FontDist");

        if (ES3.KeyExists("TypingEffect") == false)
            ES3.Save<bool>("TypingEffect", TypingEffect);
        TypingEffect = ES3.Load<bool>("TypingEffect");

        if (ES3.KeyExists("cur_text") == false)
            ES3.Save<int>("cur_text", cur_text);
        cur_text = ES3.Load<int>("cur_text");

        if (ES3.KeyExists("font") == false)
            ES3.Save<Font>("font", font);
        font = ES3.Load<Font>("font");

        if (ES3.KeyExists("is_gothic") == false)
            ES3.Save<bool>("is_gothic", is_gothic);
        is_gothic = ES3.Load<bool>("is_gothic");

        if (is_gothic)
            gothic.isOn = true;
        else
            myeongjo.isOn = true;

        FontDist_Slider.value = FontDist - 1;
        FontSize_Slider.value = (Fontsize - 25) / 35f;

        OriginColor = new Color(0xE5 / 255f, 0xe9 / 255f, 0xe5 / 255f);
        FlipColor = new Color(0x0 / 255f, 0x0 / 255f, 0x0 / 255f);
        ChoiceColor = new Color(0xf2 / 255f, 0xfb / 255f, 0xf2 / 255f);
    }
 }
