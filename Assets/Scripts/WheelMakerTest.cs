using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelMakerTest : MonoBehaviour
{
    public GameObject wheelPiece8;
    public GameObject wheelPiece4;
    public GameObject wheelPiece8_2;
    public GameObject wheelPiece16;
    public List<GameObject> wheelHolders;
    public AnimationCurve easeCurve;
    public static WheelMakerTest instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i = 0; i < 7; i++)
        {
            GameObject tmpObj;
            if (GameManager.Instance.wheelBases[0].wheelSlices[i].sliceSize != 4)
            {
                tmpObj = Instantiate(wheelPiece8, Vector3.zero, Quaternion.Euler(0f, 0f, i * -45f));
            }
            else
            {
                tmpObj = Instantiate(wheelPiece4, Vector3.zero, Quaternion.Euler(0f, 0f, i * -45f));
            }

            tmpObj.transform.SetParent(wheelHolders[0].transform);
            tmpObj.transform.localScale = Vector3.one * 0.9f;
            SliceScr sliceScr = tmpObj.GetComponent<SliceScr>();
            sliceScr.baseSlice.color = GameManager.Instance.wheelBases[0].wheelSlices[i].sliceColor;
            float multiplier = GameManager.Instance.wheelBases[0].wheelSlices[i].multiplier;
            string sliceText = "x" + Mathf.FloorToInt(multiplier).ToString();
            if (multiplier % 1f != 0f)
            {
                sliceText += ",5";
            }
            if (multiplier < 0f)
            {
                sliceText = "NEXT";
            }
            sliceScr.multiplierText.text = sliceText;
            sliceScr.wheelNo = 0;
            sliceScr.sliceNo = GameManager.Instance.sliceList.Count;
            GameManager.Instance.sliceList.Add(sliceScr);
            GameManager.Instance.wheelSliceList.Add(GameManager.Instance.wheelBases[0].wheelSlices[i]);
            if (i < 6)
            {
                for (int j = 0; j < GameManager.Instance.wheelBases[0].wheelSlices[i].sliceSize * 32; j++)
                {
                    GameManager.Instance.winTable.Add(i);
                }
            }
        }

        for (int i = 0; i < 8; i++)
        {
            GameObject tmpObj;
            if (GameManager.Instance.wheelBases[2].wheelSlices[i].multiplier > -1f)
            {
                tmpObj = Instantiate(wheelPiece8_2, Vector3.zero, Quaternion.Euler(0f, 0f, i * -45f));
            }
            else
            {
                tmpObj = Instantiate(wheelPiece8_2, Vector3.zero, Quaternion.Euler(0f, 0f, i * -45f));
            }
            tmpObj.transform.SetParent(wheelHolders[1].transform);
            tmpObj.transform.localScale = Vector3.one * 0.9f;

            SliceScr sliceScr = tmpObj.GetComponent<SliceScr>();
            sliceScr.baseSlice.color = GameManager.Instance.wheelBases[1].wheelSlices[i].sliceColor;
            float multiplier = GameManager.Instance.wheelBases[1].wheelSlices[i].multiplier;
            string sliceText = "x" + Mathf.FloorToInt(multiplier).ToString();
            if (multiplier % 1f != 0f)
            {
                sliceText += ",5";
            }
            if (multiplier < 0f)
            {
                sliceText = "NEXT";
            }
            sliceScr.multiplierText.text = sliceText;

            sliceScr.wheelNo = 1;
            sliceScr.sliceNo = GameManager.Instance.sliceList.Count;
            GameManager.Instance.wheelSliceList.Add(GameManager.Instance.wheelBases[1].wheelSlices[i]);
            GameManager.Instance.sliceList.Add(sliceScr);
            if (i != 1)
            {
                for (int j = 0; j < 16; j++)
                {
                    GameManager.Instance.winTable.Add(i + 7);
                }
            }
        }

        for (int i = 0; i < 16; i++)
        {
            GameObject tmpObj;
            if (GameManager.Instance.wheelBases[2].wheelSlices[i].multiplier > -1f)
            {
                tmpObj = Instantiate(wheelPiece16, Vector3.zero, Quaternion.Euler(0f, 0f, i * -22.5f));
            }
            else
            {
                tmpObj = Instantiate(wheelPiece16, Vector3.zero, Quaternion.Euler(0f, 0f, i * -22.5f));
            }
            tmpObj.transform.SetParent(wheelHolders[2].transform);
            tmpObj.transform.localScale = Vector3.one * 0.9f;

            SliceScr sliceScr = tmpObj.GetComponent<SliceScr>();
            sliceScr.baseSlice.color = GameManager.Instance.wheelBases[2].wheelSlices[i].sliceColor;
            float multiplier = GameManager.Instance.wheelBases[2].wheelSlices[i].multiplier;
            string sliceText = "x" + Mathf.FloorToInt(multiplier).ToString();
            if (multiplier % 1f != 0f)
            {
                sliceText += ",5";
            }
            if (multiplier < 0f)
            {
                sliceText = "NEXT";
            }
            sliceScr.multiplierText.text = sliceText;

            sliceScr.wheelNo = 2;
            sliceScr.sliceNo = GameManager.Instance.sliceList.Count;
            GameManager.Instance.wheelSliceList.Add(GameManager.Instance.wheelBases[2].wheelSlices[i]);
            GameManager.Instance.sliceList.Add(sliceScr);
            for (int j = 0; j < 1; j++)
            {
                GameManager.Instance.winTable.Add(i + 15);
            }

        }
        
        

        //wheels[2].transform.DORotate(new Vector3(0f, 0f, 2800f), 6.2f, RotateMode.FastBeyond360).SetEase(easeCurve);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
