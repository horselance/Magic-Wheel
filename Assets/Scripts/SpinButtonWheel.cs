using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpinButtonWheel : MonoBehaviour
{
    public SpriteRenderer circleSprite;
    public SpriteRenderer arrowSprite;
    public SpriteRenderer edgeGlowSprite;
    public SpriteRenderer topGlowSprite;
    public GameObject glowObject1;
    public GameObject particlePrefab;
    public bool mouseOver;
    public AnimationCurve glowEase;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
        GameManager.OnGameStateChanged += OnGameStateChanged;
        topGlowSprite.DOFade(1f, 3.1415f).SetEase(glowEase).SetLoops(-1, LoopType.Restart).SetDelay(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameStateChanged(GameState state)
    {
        if (state == GameState.ReadyToSpin)
        {
            topGlowSprite.gameObject.SetActive(true);
            if (mouseOver)
            {
                GameManager.Instance.ChangeCursor(GameManager.Instance.handCursor);
            }
        }
        else
        {
            topGlowSprite.gameObject.SetActive(false);
        }
        if (state == GameState.FirstSpin)
        {
            glowObject1.SetActive(true);
            arrowSprite.color = new Color(1f, 1f, 1f, 0.5f);
        }
        if (state == GameState.WaitingServerResponse)
        {
            transform.DOKill();
            transform.localScale = Vector3.one;
            transform.DOScale(0.8f, 0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCirc);
            edgeGlowSprite.DOKill();
            edgeGlowSprite.DOFade(1f,0.2f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutCirc);
        }
    }


    public void SideSpinButton()
    {
        OnMouseUpAsButton();
    }
    

    public void CircleGlowColor()
    {
        circleSprite.color = GameManager.Instance.colorTheme.spinButtonGlow;
        glowObject1.SetActive(false);
        topGlowSprite.gameObject.SetActive(false);
        GameManager.Instance.ChangeCursor(GameManager.Instance.handCursor);
        arrowSprite.color = new Color(1f, 1f, 1f, 1f);
    }
    public void CircleResetColor()
    {
        circleSprite.color = GameManager.Instance.colorTheme.spinButtonNormal;
        glowObject1.SetActive(true);
        topGlowSprite.gameObject.SetActive(true);
        arrowSprite.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void OnMouseUpAsButton()
    {
        GameState gameState = GameManager.Instance.state;
        
        if (gameState == GameState.ReadyToSpin && GameManager.Instance.money >= 1f)
        {
            Debug.Log("Pressed Spin Button");
            Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
            GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
            AudioManager.Instance.PlayAudio1(AudioManager.Instance.spinButtonSound);
            GameManager.Instance.ChangeGameState(GameState.WaitingServerResponse);
        }
        
    }

    void OnMouseEnter()
    {
        GameState gameState = GameManager.Instance.state;
        if (gameState == GameState.ReadyToSpin )
        {
            CircleGlowColor();
        }
        mouseOver = true;
    }

    void OnMouseExit()
    {
        GameState gameState = GameManager.Instance.state;
        if (gameState == GameState.ReadyToSpin )
        {
            CircleResetColor();
        }
        GameManager.Instance.ChangeCursor(GameManager.Instance.defaultCursor);
        mouseOver = false;
    }
}
