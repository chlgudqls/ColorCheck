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

    //�ᱹ�� ���������̶�°�
    //UI�� �׻� ���� �����ڿ�
    void LateUpdate()   
    {
        //string.Format�̿��ؼ� �Ҽ����� ����ȯ�� �ѹ��� �ذ� 
        myText.text = string.Format("{0:F0}",GameManager.score);
    }
}
