using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WinGradient : MonoBehaviour
{
    public float delay;
    //public SpriteRenderer gradientSprite;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.Win)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one * 2.5f, 0.5f).SetEase(Ease.Flash).SetDelay(delay)
                .OnComplete(() => SecondBurst());
            //gradientSprite.color = GameManager.Instance.colorTheme.circleGradient;
            //gradientSprite.DOFade(0f, 6.2f).SetEase(GameManager.Instance.easeCurve);
        }
    }

    private void SecondBurst()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one * 2.5f, 0.5f).SetEase(Ease.Flash).SetDelay(0.5f)
                    .OnComplete(() => transform.localScale = Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
