using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitMapWindow : MonoBehaviour
{
    private BitMapBlockObj[] bitMapBlockObjs;
    private Button returnButton;

    public void Init()
    {
        bitMapBlockObjs = GetComponentsInChildren<BitMapBlockObj>();

        returnButton = GetComponentInChildren<Button>();

        returnButton.onClick.RemoveAllListeners();
        returnButton.onClick.AddListener(() =>
        {
            UIManager.Instance.PopPanel(UIPanelType.BitMapPanel);
        });
    }

    public void Show()
    {
        for (int i = 0; i < BlockManager.Instance.rowNum; i++)
        {
            for (int j = 0; j < BlockManager.Instance.lineNum; j++)
            {
                bitMapBlockObjs[i * BlockManager.Instance.lineNum + j].Init(BlockManager.Instance.blocks[i][j]);
            }
        }
    }
}
