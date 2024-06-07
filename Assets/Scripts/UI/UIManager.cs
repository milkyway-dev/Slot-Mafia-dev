using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Info_Button;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private Button Left_Arrow;
    [SerializeField]
    private Button Right_Arrow;
    [SerializeField]
    private GameObject[] paytableList;
    [SerializeField]
    private int CurrentIndex = 0;
    [SerializeField]
    private Image Info_Image;
    [SerializeField]
    private TMP_Text[] SymbolsText;

    [Header("Win Popup")]
    [SerializeField]
    private GameObject WinPopup_Object;
    [SerializeField]
    private TMP_Text Win_Text;

    [SerializeField]
    private AudioController audioController;

    [Header("Light Animation")]
    [SerializeField] private GameObject lightOn;
    [SerializeField] private GameObject lightOff;

    [SerializeField]
    private Button GameExit_Button;

    [SerializeField]
    private SlotBehaviour slotManager;

    private void Start()
    {
        StartCoroutine(lightanimation());

        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });


        if (Info_Button) Info_Button.onClick.RemoveAllListeners();
        if (Info_Button) Info_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if(Left_Arrow) Left_Arrow.onClick.RemoveAllListeners();
        if(Left_Arrow) Left_Arrow.onClick.AddListener(delegate { slide(-1); });

        if(Right_Arrow) Right_Arrow.onClick.RemoveAllListeners();
        if(Right_Arrow) Right_Arrow.onClick.AddListener(delegate { slide(1); });

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(CallOnExitFunction);
    }

    internal void PopulateWin(double amount)
    {
        int initAmount = 0;
        if (WinPopup_Object) WinPopup_Object.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (Win_Text) Win_Text.text = initAmount.ToString();
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(false);
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
            slotManager.CheckBonusGame();
        });
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }

    private void slide(int i)
    {
        if (audioController) audioController.PlayButtonAudio();

        if (CurrentIndex < paytableList.Length-1 && i>0)
        {
            paytableList[CurrentIndex].SetActive(false);
            paytableList[CurrentIndex + 1].SetActive(true);
            CurrentIndex ++;
        }

        if (CurrentIndex >= 1 && i<0)
        {
            paytableList[CurrentIndex].SetActive(false);
            paytableList[CurrentIndex - 1].SetActive(true);
            CurrentIndex --;
        }
    }

    internal void InitialiseUIData(string SupportUrl, string AbtImgUrl, string TermsUrl, string PrivacyUrl, Paylines symbolsText)
    {
        PopulateSymbolsPayout(symbolsText);
    }


    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            if (i < SymbolsText.Length)
            {
                string text = null;
                if (paylines.symbols[i].multiplier._5x != 0)
                {
                    text += paylines.symbols[i].multiplier._5x;
                }
                if (paylines.symbols[i].multiplier._4x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._4x;
                }
                if (paylines.symbols[i].multiplier._3x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._3x;
                }
                if (paylines.symbols[i].multiplier._2x != 0)
                {
                    text += "\n" + paylines.symbols[i].multiplier._2x;
                }
                if (SymbolsText[i]) SymbolsText[i].text = text;
            }
        }
    }

    private void CallOnExitFunction()
    {
        slotManager.CallCloseSocket();
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
    }

    private IEnumerator lightanimation()
    {
        while (true)
        {
            if (lightOn.activeSelf)
            {
                lightOn.SetActive(false);
            }
            else
            {
                lightOn.SetActive(true);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
