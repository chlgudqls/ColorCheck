using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Color highRate;
    public Color midRate;
    public Color lowRate;
    public Image fill;
    Slider mySlider;
    void Start()
    {
        mySlider = GetComponent<Slider>();
    }

    void LateUpdate()
    {
        float remain = GameManager.maxTime - GameManager.playTime;
        //기존은 비율이 증가 증가하는값을 고정값에 빼서 감소시켜야됨
        float rate = remain / GameManager.maxTime;
        mySlider.value = rate;

        if (rate > 0.7f)
        {
            fill.color = highRate;
        }
        else if(rate > 0.4f)
        {
            fill.color = midRate;
        }
        else
        {
            fill.color = lowRate;
        }
    }
}
