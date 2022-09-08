using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BetMinus : MonoBehaviour
{
    public SpriteRenderer minusSprite;
    public bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        BetManager.OnBetIndexChanged += OnBetIndexChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        GameManager gm = GameManager.Instance;
        BetManager bm = BetManager.Instance;
        if (state == GameState.ReadyToSpin && bm.betAmountIndex > 0)
        {
            minusSprite.DOFade(1f, 0.01f);
        }
        if (state == GameState.FirstSpin)
        {
            minusSprite.DOFade(0.25f, 0.01f);
        }
    }



    // Update is called once per frame
    void Update()
    {

    }

    public void OnBetIndexChanged(int index)
    {
        GameManager gm = GameManager.Instance;
        if (index == 0 || gm.money < 1)
        {
            minusSprite.DOFade(0.25f, 0.01f);
            isOn = false;
        }
        else
        {
            minusSprite.DOFade(1f, 0.01f);
            isOn = true;
        }
    }

    private void OnMouseUpAsButton()
    {
        GameState gameState = GameManager.Instance.state;
        GameManager gm = GameManager.Instance;
        BetManager bm = BetManager.Instance;
        if (isOn && bm.betAmountIndex > 0 && gameState == GameState.ReadyToSpin)
        {
            AudioManager.Instance.PlayAudio1(AudioManager.Instance.clickSound);
            bm.DecreaseBetAmount();
            if (!CanDecrease())
            {
                GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
            }
        }

    }

    private bool CanDecrease()
    {
        GameManager gm = GameManager.Instance;
        if (BetManager.Instance.betAmountIndex > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void OnMouseEnter()
    {
        GameState gameState = GameManager.Instance.state;
        if (gameState == GameState.ReadyToSpin && isOn)
        {
            GameManager.Instance.ChangeCursor(GameManager.Instance.handCursor);
        }
    }

    void OnMouseExit()
    {
        GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
    }
}
