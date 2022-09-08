using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BetPlus : MonoBehaviour
{
    public SpriteRenderer plusSprite;
    public bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
        BetManager.OnBetIndexChanged += OnBetIndexChanged;
    }

    private bool CanIncrease()
    {
        GameManager gm = GameManager.Instance;
        BetManager bm = BetManager.Instance;
        int index = bm.betAmountIndex;
        if (index + 1 < bm.betAmountTable.Count)
        {
            Debug.Log("inside");
            if (bm.betAmountTable.Count == index + 1 || gm.money < bm.betAmountTable[index + 1])
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public void ButtonEnable()
    {
        plusSprite.DOFade(1f, 0.01f);
        isOn = true;
    }

    public void ButtonDisable()
    {
        isOn = false;
        plusSprite.DOFade(0.25f, 0.01f);
    }

    private void OnGameStateChanged(GameState state)
    {
        GameManager gm = GameManager.Instance;
        BetManager bm = BetManager.Instance;
        if (state == GameState.ReadyToSpin && bm.betAmount < bm.betAmountTable[bm.betAmountTable.Count - 1] && CanIncrease())
        {
            ButtonEnable();
        }
        if (state == GameState.FirstSpin)
        {
            ButtonDisable();
        }
    }

    public void OnBetIndexChanged(int index)
    {
        GameManager gm = GameManager.Instance;
        BetManager bm = BetManager.Instance;
        if (index + 1 < bm.betAmountTable.Count)
        {
            //if (gm.money < gm.betAmountTable[index + 1])
            //{
            //    plusSprite.DOFade(0.25f, 0.01f);
            //    isOn = false;
            //}
            Debug.Log("inside");
            if (bm.betAmountTable.Count == index + 1 || gm.money < bm.betAmountTable[index + 1])
            {
                ButtonDisable();
            }
            else
            {
                ButtonEnable();
            }
        }
        else 
        {
            ButtonDisable();
        }
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        GameState gameState = GameManager.Instance.state;
        GameManager gm = GameManager.Instance;
        BetManager bm = BetManager.Instance;
        if (isOn && bm.betAmount < bm.betAmountTable[bm.betAmountTable.Count - 1] && gameState == GameState.ReadyToSpin)
        {
            AudioManager.Instance.PlayAudio1(AudioManager.Instance.clickSound);
            bm.IncreaseBetAmount();
            if (!CanIncrease())
            {
                GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
            }
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
