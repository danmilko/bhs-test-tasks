using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RandomPlacement : MonoBehaviour
{
    float placementRangeW;
    float placementRangeH;
    public int count = 4;
    public ClickNum clickObject;
    List<ClickNum> objs;
    int nextSelect = 1;
    public GameObject startButton;
    public TextMeshProUGUI result;
    void Start()
    {
        objs = new List<ClickNum>();
        placementRangeH = this.GetComponent<RectTransform>().rect.height;
        placementRangeW = this.GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < count; i++) 
        {
            ClickNum t = Instantiate(clickObject);
            t.SetNum(i + 1);
            objs.Add(t);
            t.transform.SetParent(this.transform);
            t.parent = this;
            t.gameObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].gameObject.SetActive(true);
            PlaceRandomOnCanvas(objs[i].gameObject); 
        }
        nextSelect = 1;
        startButton.SetActive(false);
        result.gameObject.SetActive(false);
    }

    void PlaceRandomOnCanvas(GameObject o)
    {
        RectTransform rt = o.GetComponent<RectTransform>();
        float height = rt.rect.height;
        float width = rt.rect.width;
        Quaternion q = Quaternion.Euler(0, 0, Random.Range(-90, 90));
        o.GetComponent<RectTransform>().SetPositionAndRotation
        (
            new Vector3(
                Random.Range(0 + width / 2, placementRangeW - width / 2),
                Random.Range(0 + height / 2, placementRangeH - height / 2)
                ),
            q
        );
    }

    public void CheckClick(int num)
    {
        if (nextSelect != num) 
        {
            FinishGame(false); 
        }
        else if (nextSelect == objs.Count)
        {
            FinishGame(true);
        }
        nextSelect++;
    }

    public void FinishGame(bool win)
    {
        startButton.SetActive(true);
        for (int i = 0; i < objs.Count; i++)
        {
            objs[i].gameObject.SetActive(false);
        }
        result.text = win ? "You won" : "You lost";
        result.gameObject.SetActive(true);
    }
}
