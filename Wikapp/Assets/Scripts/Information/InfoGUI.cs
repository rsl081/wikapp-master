using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using TMPro;

public class InfoGUI : MonoBehaviour
{
    [SerializeField]GameObject nameTmp; 
    [SerializeField]GameObject ageTmp;
    [SerializeField]public TMP_Dropdown monthDropDown;
    [SerializeField]public TMP_Dropdown dayDropDown;
    [SerializeField]public TMP_Dropdown yearDropDown;
    [SerializeField]public TMP_Dropdown genderDropDown;
    List<string> montList = new List<string> { 
        "Buwan...",
        "Enero", "Pebrero", "Marso",
        "Abril", "Mayo", "Hunyo",
        "Hulyo", "Agosto", "Setyembre",
        "Oktubre", "Nobyembre", "Disyembre"
    };

    List<string> dayList = new List<string>() {
        "Araw...",
        "1", "2", "3", "4", "5", "6", "7",
        "8", "9", "10", "11", "12", "13", "14",
        "15", "16", "17", "18", "19", "20", "21",
        "22", "23", "24", "25", "26", "27", "28",
        "29", "30", "31"
    };

    List<string> yearList = new List<string> { 
        "Taon...",
        "2022", "2021",
        "2020", "2019", "2018",
        "2017", "2016", "2015",
        "2014", "2013", "2012",
        "2011", "2010", "2009",
        "2008", "2007", "2006",
        "2005", "2004", "2003",
        "2002", "2001", "2000",
        "1999","1998", "1997", 
        "1996","1995", "1994",
        "1993","1992", "1991",
        "1990","1989", "1988",
        "1987","1986", "1985",
        "1984","1983", "1982",
        "1981","1980", "1979",
        "1978","1977", "1976",
        "1975","1974", "1973",
        "1972","1971", "1970",
        "1969","1968", "1967", 
        "1966"
    };
    // List<string> genderList = new List<string> { 
    //     "Kasarian...",
    //     "Lalake", "Babae",
    // };
    [SerializeField]TMP_FontAsset nougatFont;
    Transition transition;

    private void Start() {
    
        init();

        

    }

    private void Update() {
        if(nameTmp.GetComponent<TMP_InputField>().isFocused)
        {
            nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Palayaw...";
            nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().color = Color.white;
            nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().alpha = 0.50f;
        
        }
        if(ageTmp.GetComponent<TMP_InputField>().isFocused)
        {
            ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Edad...";
            ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().color = Color.white;
            ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().alpha = 0.50f;
        
        }
    }

    void init(){

        transition = FindObjectOfType<Transition>();

        populateToDropDownMonth();
        populateToDropDownDay();
        populateToDropDownYear();
        //populateToDropDownGender();

    }


    public void submit() {
        string name = nameTmp.GetComponent<TMP_InputField>().text.Trim();
        string age = ageTmp.GetComponent<TMP_InputField>().text.Trim();
        string month = monthDropDown.options[monthDropDown.value].text;
        string day = dayDropDown.options[dayDropDown.value].text;
        string year = yearDropDown.options[yearDropDown.value].text;
        string gender = genderDropDown.options[genderDropDown.value].text;

        Info.Instance.currentPlayer = new Player();

        if(CheckField(name, age, month, day, year, gender)){
            Info.Instance.setCredentials(
                name, age, month, 
                day, year, gender
            );

    
            string player = JsonUtility.ToJson(Info.Instance.currentPlayer);
            Info.Instance.setPlayer(player);
            transition.SubmitInfo();
            
        }
    }




    bool CheckField(string name, string age, string month, string day,
                    string year, string gender){
        if(name == "" || age == "" || month == "Buwan..." || day == "Araw..."||
            year == "Year..."){
            if(name == ""){
                nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().font = nougatFont;
                nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Kinakailangan";
                nameTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
            }
            if(age == ""){
                ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().font = nougatFont;
                ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().fontStyle = FontStyles.Normal;
                ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().text = "Kinakailangan";
                ageTmp.GetComponent<TMP_InputField>().placeholder.GetComponent<TextMeshProUGUI>().color = Color.red;
            }

            if(month == "Buwan..."){
            
                monthDropDown.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();
            }
            if(day == "Araw..."){

                dayDropDown.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();
            
            }
            if(year == "Taon..."){
                
                yearDropDown.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();

            }
            // if(gender == "Kasarian..."){

            //     genderDropDown.transform.DOPunchPosition(transform.localPosition + 
            //                                                 new Vector3(0f,-5f,0), 0.5f).Play();
            // }

            return false;
        }
        return true;
    }

    public void populateToDropDownMonth()
    {
        monthDropDown.ClearOptions();
        monthDropDown.AddOptions(montList);
    }
    public void populateToDropDownDay()
    {
        dayDropDown.ClearOptions();
        dayDropDown.AddOptions(dayList);
    }
    public void populateToDropDownYear()
    {
        yearDropDown.ClearOptions();
        yearDropDown.AddOptions(yearList);
    }
    // public void populateToDropDownGender()
    // {
    //     genderDropDown.ClearOptions();
    //     genderDropDown.AddOptions(genderList);
    // }
}
