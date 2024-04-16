using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class PayoutCalculation : MonoBehaviour
{
    [SerializeField]
    private int x_Distance;
    [SerializeField]
    private int y_Distance;

    [SerializeField]
    private Transform LineContainer;
    [SerializeField]
    private GameObject[] Lines_Object;

    internal int LineCount;

    internal int currrentLineIndex;

    internal List<int> DontDestroy = new List<int>();

    GameObject TempObj = null;

    private void Awake()
    {
        LineCount = Lines_Object.Length;
        currrentLineIndex = Lines_Object.Length;
    }

    internal void GeneratePayoutLinesBackend(int lineIndex=-1, bool isStatic = false)
    {
        ResetLines();
        if (lineIndex >= 0) {
            if (Lines_Object[lineIndex]) Lines_Object[lineIndex].SetActive(true);
            //currrentLineIndex = lineIndex;
            return;
        }

        for (int i = 0; i < currrentLineIndex; i++)
        {
            Lines_Object[i].SetActive(true);


        }
        if (isStatic)
        {
            TempObj = Lines_Object[lineIndex];
        }
    }

    internal void ResetStaticLine()
    {
        if(TempObj!=null)
        {
            TempObj.SetActive(false);
            TempObj = null;
        }
    }

    internal void ResetLines()
    {
        for (int i = 0; i < Lines_Object.Length; i++)
        {
            if (DontDestroy.IndexOf(i) >= 0)
                continue;
            else
            Lines_Object[i].SetActive(false);
        }
    

    }


}
