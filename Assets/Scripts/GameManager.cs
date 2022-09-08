using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
//using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public int currentWheel;
    public int currentSlice;
    public float _money;
    
    
    public GameObject particlePrefab;
    public int historyCount;
    public Texture2D defaultCursor;
    public Texture2D handCursor;
    public static event System.Action<GameState> OnGameStateChanged;
    
    public GameSettings gameSettings;
    public ColorTheme colorTheme;
    public WheelBase[] wheelBases;
    public List<SliceScr> sliceList;
    public List<WheelSlice> wheelSliceList;
    public List<GameObject> wheels;
    public List<int> winTable;
    
    public AnimationCurve easeCurve;
    public float money
    {
        get { return _money; }
        set
        {
            _money = value;
            UI.Instance.moneyText.text = "DEM " + _money.ToString("F0");
        }
    }
    

    private void Awake()
    {
        Instance = this;
        winTable = new List<int>();
        sliceList = new List<SliceScr>();
    }

    void Start()
    {
        money = gameSettings.startMoney;
        BetManager.Instance.betAmount = gameSettings.startBetAmount;
        BetManager.Instance.betAmountIndex = 1;
        ChangeGameState(GameState.ReadyToSpin);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeGameState(GameState newState)
    {
        GameState oldState =  state;
        state = newState;
        
        switch (newState)
        {
            case GameState.ReadyToSpin:
                OnReadyToSpin();
                break;
            case GameState.WaitingServerResponse:
                OnWaitServerResponse();
                break;
            case GameState.FirstSpin:
                OnFirstSpin();
                break;
            case GameState.SecondSpin:
                OnSecondSpin();
                break;
            case GameState.ThirdSpin:
                OnThirdSpin();
                break;
            case GameState.Win:
                OnWin();
                break;
            case GameState.Lose:
                OnLose();
                break;
            case GameState.SpinEnded:
                OnSpinEnded();
                break;

        }

        OnGameStateChanged?.Invoke(newState);
    }

    private void OnReadyToSpin()
    {
        if (BetManager.Instance.betAmount > money)
        {
            BetManager.Instance.MaxBetAmount();
        }
    }

    private async void OnSpinEnded()
    {
        await Task.Delay(150);
        sliceList[currentSlice].selectionObject.SetActive(false);
        sliceList[6].selectionObject.SetActive(false);
        sliceList[8].selectionObject.SetActive(false);
    }

    private void OnWaitServerResponse()
    {
        Debug.Log("Waiting Server Response");
        OnServerResponseReceived(ServerResponse());
    }

    private void OnFirstSpin()
    {
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.spinSound);
        AudioManager.Instance.PlayAudio2(AudioManager.Instance.spin2Sound);
        AudioManager.Instance.PlayAudioDelayed(AudioManager.Instance.spinLastSound, 3500);
    }
    private void OnSecondSpin()
    {
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.spinSound);
        AudioManager.Instance.PlayAudio2(AudioManager.Instance.spin2Sound);
        AudioManager.Instance.PlayAudioDelayed(AudioManager.Instance.spinLastSound, 3500);
    }
    private void OnThirdSpin()
    {
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.spinSound);
        AudioManager.Instance.PlayAudio2(AudioManager.Instance.spin2Sound);
        AudioManager.Instance.PlayAudioDelayed(AudioManager.Instance.spinLastSound, 3500);
    }

    private void OnServerResponseReceived(int response)
    {
        SpinWheel(response, 0);
    }

    private int ServerResponse()
    {
        int output;
        output = winTable[Random.Range(0, 512)];
        //output = 1;
        money -= BetManager.Instance.betAmount;
        
        return output;
    }


    public async void NextWheel(int sliceNo, int wheelNo, int targetWheel)
    {
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.nextWheelSound);
        await Task.Delay(200);
        wheels[wheelNo - 1].GetComponent<WheelScr>().borderSprite.DOFade(0.1f, 0.01f);
        Instantiate(particlePrefab, Vector3.zero, Quaternion.identity);
        currentWheel = wheelNo;
        if (wheelNo == 1)
        {
            sliceList[6].selectionObject.SetActive(true);
            sliceList[6].glowSprite.DOFade(0.8f, 0.3f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.InOutCirc)
                .OnComplete(() => SpinWheel(sliceNo, wheelNo));
            ChangeGameState(GameState.SecondWheel);
        }
        else
        {
            sliceList[8].selectionObject.SetActive(true);
            sliceList[8].glowSprite.DOFade(0.8f, 0.3f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.InOutCirc)
                .OnComplete(() => SpinWheel(sliceNo, wheelNo));
            ChangeGameState(GameState.ThirdWheel);
        }
    }

    public  void CalculatePayout(int sliceNo, int wheelOfSlice)
    {
        //await Task.Delay(200);
        float multiplier = wheelSliceList[sliceNo].multiplier;
        if (multiplier > 0f)
        {
            money += multiplier * BetManager.Instance.betAmount;
            ChangeGameState(GameState.Win);
        }
        else
        {
            ChangeGameState(GameState.Lose);
        }
        if (historyCount < 4)
        {
            
            if (historyCount > 0)
            {
                for (int i = historyCount; i > 0; i--)
                {
                    UI.Instance.historyMultiplierTexts[i].text = UI.Instance.historyMultiplierTexts[i - 1].text;
                    UI.Instance.historyTexts[i].text = UI.Instance.historyTexts[i - 1].text;
                }
            }
            UI.Instance.historyBars[historyCount].SetActive(true);
            UI.Instance.historyMultiplierTexts[0].text = "x" + multiplier.ToString();
            UI.Instance.historyTexts[0].text = "DEM " + (multiplier * BetManager.Instance.betAmount).ToString("");
            historyCount++;
        }
        else
        {
            for (int i = 3; i > 0; i--)
            {
                UI.Instance.historyMultiplierTexts[i].text = UI.Instance.historyMultiplierTexts[i - 1].text;
                UI.Instance.historyTexts[i].text = UI.Instance.historyTexts[i - 1].text;
            }
            UI.Instance.historyMultiplierTexts[0].text = "x" +  multiplier.ToString();
            UI.Instance.historyTexts[0].text = "DEM " + (multiplier * BetManager.Instance.betAmount).ToString("");
        }
    }

    public void OnWin()
    {
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.winSound);
        wheels[currentWheel].GetComponent<WheelScr>().borderSprite.DOFade(0.1f, 0.01f);
        sliceList[currentSlice].selectionObject.SetActive(true);
        sliceList[currentSlice].glowSprite.DOFade(0.8f, 0.3f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.InOutCirc)
            .OnComplete(() => ChangeGameState(GameState.SpinEnded));
    }

    public void OnLose()
    {
        AudioManager.Instance.PlayAudio1(AudioManager.Instance.loseSound);
        wheels[currentWheel].GetComponent<WheelScr>().borderSprite.DOFade(0.1f, 0.01f);
        sliceList[currentSlice].selectionObject.SetActive(true);
        sliceList[currentSlice].glowSprite.DOFade(0.8f, 0.3f).SetLoops(6, LoopType.Yoyo).SetEase(Ease.InOutCirc)
            .OnComplete(() => ChangeGameState(GameState.SpinEnded));
    }

    public async void SpinWheel(int sliceNo, int spinNo)
    {
        int wheelOfSlice = 0;
        currentWheel = spinNo;
        currentSlice = sliceNo;
        if (spinNo == 0)
        {
            ChangeGameState(GameState.FirstSpin);
        }
        else if (spinNo == 1)
        {
            ChangeGameState(GameState.SecondSpin);
        }
        else
        {
            ChangeGameState(GameState.ThirdSpin);
        }

        if (sliceNo > 6)
        {
            wheelOfSlice = 1;
        }
        if (sliceNo > 14)
        {
            wheelOfSlice = 2;
        }


        if (wheelOfSlice > spinNo)
        {
            await Task.Delay(200);

            Vector3 targetAngle = new Vector3(0f, 0f, Calculate.GetNextSliceAngle(spinNo));
            wheels[spinNo].transform.DORotate(targetAngle, 6.2f, RotateMode.FastBeyond360).SetEase(easeCurve)
                .OnComplete(() => NextWheel(sliceNo, spinNo + 1, wheelOfSlice));
        }
        else
        {
            await Task.Delay(200);
           
            Vector3 targetAngle = new Vector3(0f, 0f, Calculate.GetSliceAngle(sliceNo));
            wheels[spinNo].transform.DORotate(targetAngle, 6.2f, RotateMode.FastBeyond360).SetEase(easeCurve)
                .OnComplete(() => CalculatePayout(sliceNo, wheelOfSlice));
        }
    }

    public void ChangeCursor(Texture2D cursorTexture)
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

}
public enum GameState
{
    ReadyToSpin,
    WaitingServerResponse,
    FirstSpin,
    SecondWheel,
    SecondSpin,
    ThirdWheel,
    ThirdSpin,
    Win,
    Lose,
    SpinEnded,
}

[System.Serializable]
public class GameSettings
{
    public float startMoney;
    public float maximumBetAmount;
    public float startBetAmount;
}

[System.Serializable]
public class WheelSlice
{
    public Color sliceColor;
    public Color textColor = Color.white;
    public float multiplier;
    public float sliceSize;
}
[System.Serializable]
public class WheelBase
{
    public List<WheelSlice> wheelSlices;
}

[System.Serializable]
public class ColorTheme
{
    public Color spinButtonNormal = Color.white;
    public Color spinButtonGlow = Color.white;
    public Color spinButtonDisabled = Color.white;
    public Color circleGradient = Color.white;
}
