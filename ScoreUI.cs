using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreUI : MonoBehaviour
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
        //string.Format이용해서 소숫점과 형변환을 한번에 해결 
        myText.text = string.Format("{0:F0}",GameManager.score);
    }
}
