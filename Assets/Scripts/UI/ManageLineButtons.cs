using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class ManageLineButtons : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler, IPointerUpHandler,IPointerDownHandler
{

	[SerializeField]
	private PayoutCalculation payManager;
	[SerializeField]
	private GameObject _ConnectedLine;
	
	private bool isEnabled = false;
	[SerializeField]
	private int num;
	private Button btn;

    private void Start()
    {
		btn = this.GetComponent<Button>();

	}

    public void OnPointerEnter(PointerEventData eventData)
	{
        print(payManager.currrentLineIndex);
        if (num <= payManager.currrentLineIndex)
        {
            isEnabled = true;

            //btn.interactable = true;
        }
        else
        {
            isEnabled = false;
            //btn.interactable = false;

        }
        if (isEnabled)
			payManager.GeneratePayoutLinesBackend(num-1);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		if (isEnabled)
			payManager.ResetLines();
	}
	public void OnPointerDown(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
		{
			this.gameObject.GetComponent<Button>().Select();
			Debug.Log("run on pointer down");
			payManager.GeneratePayoutLinesBackend(num - 1);
		}

	}
	public void OnPointerUp(PointerEventData eventData)
	{
		if (Application.platform == RuntimePlatform.WebGLPlayer && Application.isMobilePlatform)
		{
			Debug.Log("run on pointer up");
			payManager.ResetLines();
			DOVirtual.DelayedCall(0.1f, () =>
			{
				this.gameObject.GetComponent<Button>().spriteState = default;
				EventSystem.current.SetSelectedGameObject(null);
			});
		}
	}


}
