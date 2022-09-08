using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class SliceScr : MonoBehaviour
{
    public SpriteRenderer baseSlice;
    public TMP_Text multiplierText;
    public SpriteRenderer fadeSprite;
    public SpriteRenderer triangleSprite;
    public GameObject cornerParticle;
    public GameObject selectionObject;
    public SpriteRenderer glowSprite;
    public int wheelNo;
    public int sliceNo;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.ReadyToSpin)
        {
            fadeSprite.DOFade(0f, 0.2f);
            triangleSprite.DOFade(1f, 0.2f);
            cornerParticle.SetActive(true);
        }
        if ((state == GameState.FirstSpin) && wheelNo != GameManager.Instance.currentWheel)
        {
            cornerParticle.SetActive(false);
            if (!(GameManager.Instance.currentWheel > 0 && sliceNo == 6) && !(GameManager.Instance.currentWheel > 1 && sliceNo == 8))
            {
                fadeSprite.DOFade(0.9f, 0.2f);
                triangleSprite.DOFade(0.25f, 0.2f);
            }
        }
        //state == GameState.FirstSpin || state == GameState.SecondSpin || state == GameState.ThirdSpin
        if ((state == GameState.SecondWheel || state == GameState.ThirdWheel) && wheelNo != GameManager.Instance.currentWheel)
        {
            cornerParticle.SetActive(false);
            if ( !(GameManager.Instance.currentWheel > 0 && sliceNo == 6) && !(GameManager.Instance.currentWheel > 1 && sliceNo == 8))
            {
                fadeSprite.DOFade(0.9f, 0.2f);
                triangleSprite.DOFade(0.25f, 0.2f);
            }
        }
        if ((state == GameState.SecondSpin || state == GameState.ThirdSpin) && wheelNo == GameManager.Instance.currentWheel)
        {
            cornerParticle.SetActive(true);
            fadeSprite.DOFade(0f, 0.2f);
            triangleSprite.DOFade(1f, 0.2f);
        }
        if ((state == GameState.Win || state == GameState.Lose) )
        {
            fadeSprite.DOFade(0.9f, 0.2f);
            triangleSprite.DOFade(0.25f, 0.2f);
        }

                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
