using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BitMapBlockObj : MonoBehaviour
{
    public Text blockText;
    public Image blockImage;

    public void Init(Block block)
    {
        blockText = GetComponentInChildren<Text>();
        blockImage = GetComponent<Image>();
        switch (block.blockStatusType)
        {
            case BlockStatusType.Free:
                blockImage.color = Color.green;
                blockText.text = "0";
                break;
            case BlockStatusType.Used:
                blockImage.color = Color.red;
                blockText.text = "1";
                break;
            default:
                break;
        }
    }
}
