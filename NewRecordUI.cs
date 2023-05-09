using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRecordUI : MonoBehaviour
{
    Text mytext;
    void Start()
    {
        if (!PlayerPrefs.HasKey("Score"))
            return;

        mytext = GetComponent<Text>();
        mytext.text = "최고점수" + string.Format("{0:F0}", PlayerPrefs.GetFloat("Score"));
    }
}
