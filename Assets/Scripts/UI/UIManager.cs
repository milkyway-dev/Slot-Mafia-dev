using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using UnityEngine.EventSystems;


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
    private int CurrentIndex=0;
    [SerializeField]
    private Image Info_Image;

    [Header("Card Bonus Game")]
    [SerializeField]
    private Button DoubleBet_Button;
    [SerializeField]
    private GameObject BonusPanel;
    [SerializeField]
    private GameObject CardGame_Panel;
    [SerializeField]
    private GameObject CoffinGame_Panel;

    [Header("Light Animation")]
    [SerializeField] private GameObject lightOn;
    [SerializeField] private GameObject lightOff;

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
      

        if (DoubleBet_Button) DoubleBet_Button.onClick.RemoveAllListeners();
        if (DoubleBet_Button) DoubleBet_Button.onClick.AddListener(delegate { OpenBonusGame(true); });
    }

    private void OpenBonusGame(bool type)
    {
        if (type)
        {
            if (CardGame_Panel) CardGame_Panel.SetActive(true);
            if (CoffinGame_Panel) CoffinGame_Panel.SetActive(false);
        }
        else
        {
            if (CardGame_Panel) CardGame_Panel.SetActive(false);
            if (CoffinGame_Panel) CoffinGame_Panel.SetActive(true);
        }
        if (BonusPanel) BonusPanel.SetActive(true);
    }

    private void OpenPopup(GameObject Popup)
    {
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }

    private void slide(int i) {

        if (CurrentIndex < paytableList.Length-1 && i>0) {
        paytableList[CurrentIndex].SetActive(false);
        paytableList[CurrentIndex + 1].SetActive(true);
            CurrentIndex ++;
        }

        if (CurrentIndex >= 1 && i<0) {
        paytableList[CurrentIndex].SetActive(false);
        paytableList[CurrentIndex - 1].SetActive(true);
            CurrentIndex --;
        }

       
    }

    IEnumerator lightanimation() {

        while (true) {
            if (lightOn.activeSelf)
                lightOn.SetActive(false);
            else
                lightOn.SetActive(true);

            yield return new WaitForSeconds(0.1f);

        }


    }


}
