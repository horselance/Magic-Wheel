using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowHead : MonoBehaviour
{
    public SpriteRenderer arrowSpite;
    public Transform[] targetTransforms;
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
            arrowSpite.sortingOrder = 5;
            transform.position = targetTransforms[0].position;
            arrowSpite.DOFade(1f, 0.01f);
        }
        if (state == GameState.SecondWheel)
        {
            arrowSpite.sortingOrder = 3;
            transform.position = targetTransforms[1].position;
        }
        if (state == GameState.ThirdWheel)
        {
            arrowSpite.sortingOrder = 1;
            transform.position = targetTransforms[2].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
