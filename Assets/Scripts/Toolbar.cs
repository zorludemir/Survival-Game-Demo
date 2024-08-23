using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public Transform indicator;
    public int tIndex;
    private void Start()
    {
        UpdateIndicator(0);
    }
    void UpdateIndicator(int index)
    {
        indicator.GetComponent<RectTransform>().transform.localPosition = new Vector2(-300 + index * 120, 0);
        tIndex = index;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateIndicator(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateIndicator(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateIndicator(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateIndicator(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            UpdateIndicator(4);
        }
    }
}
