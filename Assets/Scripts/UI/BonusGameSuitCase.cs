using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusGameSuitCase : MonoBehaviour
{
    [SerializeField] private Button suitcase;
    [SerializeField] private Sprite empty_case;
    [SerializeField] private Sprite filled_case_cash;
    [SerializeField] private Sprite filled_case_gold;
    [SerializeField] private Color32 text_color;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image case_image_bttom;
    [SerializeField] private ImageAnimation imageAnimation;

    [SerializeField]
    internal bool isOpen;
    [SerializeField]
    internal bool isEmpty;

    void Start()
    {
        if (suitcase) suitcase.onClick.RemoveAllListeners();
        if (suitcase) suitcase.onClick.AddListener(OpenCase);
        //if(isOpen) suitcase.interactable = false;
        //if (text_image) text_image.gameObject.SetActive(false);
    }

    void OpenCase() {
        if (isOpen)
            return;

        if (isEmpty)
            case_image_bttom.sprite = empty_case;
        else
            case_image_bttom.sprite = filled_case_cash;
        imageAnimation.StartAnimation();

        StartCoroutine(setCase());





    }

    IEnumerator setCase() {

        yield return new WaitUntil(() => !imageAnimation.isplaying);
        yield return new WaitForSeconds(0.3f);
        //text_image.sprite = text;

        text.gameObject.SetActive(true);
        text.outlineColor = text_color;
        //text.color = text_color;
        isOpen = true;
    }

}
