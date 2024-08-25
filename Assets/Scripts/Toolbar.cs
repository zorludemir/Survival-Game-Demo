using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public Transform indicator;
    public int tIndex;

    public GameObject[] tools;
    [SerializeField] private Vector3[] toolPositions;
    [SerializeField] private Quaternion[] toolRotations;

    private void Start()
    {
        toolPositions = new Vector3[tools.Length];
        toolRotations = new Quaternion[tools.Length];

        for (int i = 0; i < tools.Length; i++)
        {
            toolPositions[i] = tools[i].transform.localPosition;
            toolRotations[i] = tools[i].transform.localRotation;
        }

        UpdateIndicator(0);
    }


    void UpdateIndicator(int index)
    {

        indicator.GetComponent<RectTransform>().transform.localPosition = new Vector2(-300 + index * 120, 0);
        tIndex = index;

        foreach (var tool in tools)
        {
            tool.SetActive(false);
        }
        tools[index].transform.localPosition = toolPositions[index];
        tools[index].transform.localRotation = toolRotations[index];
        tools[index].SetActive(true);
        Animator newAnimator = tools[index].GetComponent<Animator>();
        if (newAnimator != null)
        {
            newAnimator.SetTrigger("Equip");
        }
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
