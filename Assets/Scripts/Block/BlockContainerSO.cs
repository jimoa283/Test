using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Block/BlockContainer")]
[System.Serializable]
public class BlockContainerSO : ScriptableObject
{
    public List<Block> blocks = new List<Block>();
}
