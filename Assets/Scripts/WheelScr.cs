using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class WheelScr : MonoBehaviour
{
    public int wheelNo;
    public GameObject borderObject;
    public SpriteRenderer borderSprite;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameState state)
    {
        if ((state == GameState.SecondSpin || state == GameState.ThirdSpin) && wheelNo == GameManager.Instance.currentWheel)
        {
            SizeBounce(10);
        }
        if ((state == GameState.FirstSpin || state == GameState.SecondSpin || state == GameState.ThirdSpin) && wheelNo != GameManager.Instance.currentWheel)
        {
            borderSprite.DOFade(0.1f, 0.01f);
        }
        if ((state == GameState.FirstSpin || state == GameState.SecondSpin || state == GameState.ThirdSpin) && wheelNo == GameManager.Instance.currentWheel)
        {
            borderSprite.DOFade(0.5f, 0.01f);
        }
        if ((state == GameState.SpinEnded))
        {
            SizeBounce((2 - wheelNo) * 300);
        }
        if ((state == GameState.ReadyToSpin))
        {
            borderSprite.DOFade(0.5f, 0.01f);
        }

        

    }

    public async void EndSpin()
    {
        await Task.Delay(200);
        if (wheelNo == 0 && GameManager.Instance.state == GameState.SpinEnded)
        {
            GameManager.Instance.ChangeGameState(GameState.ReadyToSpin);
        }
    }

    public async void SizeBounce(int delay)
    {
        await Task.Delay(delay);
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.wheelResizeSound);
        transform.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCirc);
        borderObject.transform.DOScale(Vector3.one * 1.1f, 0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCirc)
            .OnComplete(() => EndSpin());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
