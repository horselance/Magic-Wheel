using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CircleGradient : MonoBehaviour
{
    public SpriteRenderer gradientSprite;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.FirstSpin || state == GameState.SecondSpin || state == GameState.ThirdSpin)
        {
            int rotationSide = 1 - Random.Range(0, 2)*2;
            transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f));
            transform.DORotate(new Vector3(0f, 0f, Random.Range(2710f, 3230f) * rotationSide), 6.2f, RotateMode.FastBeyond360).SetEase(GameManager.Instance.easeCurve);
            gradientSprite.color = GameManager.Instance.colorTheme.circleGradient;
            gradientSprite.DOFade(0f, 6.2f).SetEase(GameManager.Instance.easeCurve);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
