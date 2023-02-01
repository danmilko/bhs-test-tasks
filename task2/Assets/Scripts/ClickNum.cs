using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickNum : MonoBehaviour
{
    public RandomPlacement parent;
    public int orderNum;
    public TextMeshProUGUI texter;
    private void Start()
    {
        texter.text = orderNum.ToString();
    }

    public void SetNum(int num)
    {
        orderNum = num;
        texter.text = orderNum.ToString();
    }

    public void SendNumInfo()
    {
        parent.CheckClick(orderNum);
    }
}
