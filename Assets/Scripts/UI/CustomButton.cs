using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private Button btn;
    [SerializeField] private Image image;
    [SerializeField] private Sprite sprite_active;
    [SerializeField] private Sprite sprite_idle;


    public void OnPointerDown(PointerEventData eventData)
    {
        if (btn.interactable) {

            image.sprite = sprite_active;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (btn.interactable) {
           image.sprite = sprite_idle;
        }

    }



}
