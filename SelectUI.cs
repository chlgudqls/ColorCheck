using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour
{
    public KeyCode mappingKey;
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        if (GameManager.isGameOver)
            return;

        if (Input.GetKeyDown(mappingKey))
            button.onClick.Invoke();
    }
}
