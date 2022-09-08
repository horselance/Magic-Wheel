using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextGradient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.SecondWheel || state == GameState.ThirdWheel)
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one * 5f, 0.65f).SetEase(Ease.Flash)
                .OnComplete(() => transform.localScale = Vector3.zero);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
