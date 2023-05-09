using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    Text myText;
    void Start()
    {
        myText = GetComponent<Text>();
    }

    //결국엔 프레임차이라는거
    //UI는 항상 값이 끝난뒤에
    void LateUpdate()
    {
        myText.text = GameManager.combo + "Combo!";
    }
}
