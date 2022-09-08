using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MaxButton : MonoBehaviour
{
    public SpriteRenderer buttonSpite;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        GameManager gm = GameManager.Instance;
        if (state == GameState.ReadyToSpin)
        {
            buttonSpite.DOFade(1f, 0.01f);
        }
        if (state == GameState.FirstSpin)
        {
            buttonSpite.DOFade(0.25f, 0.01f);
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
        if (gameState == GameState.ReadyToSpin)
        {
            AudioManager.Instance.PlayAudio1(AudioManager.Instance.clickSound);
            BetManager.Instance.MaxBetAmount();
        }

    }

    void OnMouseEnter()
    {
        GameState gameState = GameManager.Instance.state;
        if (gameState == GameState.ReadyToSpin)
        {
            GameManager.Instance.ChangeCursor(GameManager.Instance.handCursor);
        }
    }

    void OnMouseExit()
    {
        GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
    }
}
