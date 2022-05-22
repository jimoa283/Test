using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : Singleton<BlockManager>
{
    //private BlockContainerSO blockContainer;
    public Block[][] blocks;
    public int lineNum=25;
    public int rowNum=11;

    public BlockManager()
    {
        //blockContainer = Resources.Load<BlockContainerSO>("SO/BlockContainer");

        blocks = new Block[11][];
        for (int i = 0; i < rowNum; i++)
        {
            blocks[i] = new Block[lineNum];
            for (int j = 0; j < lineNum; j++)
            {
                blocks[i][j] = new Block()
                {
                    blockStatusType = BlockStatusType.Free,
                    blockIndex = i * lineNum + j,
                    nextblockIndex = -1
                };

                //blockContainer.blocks.Add(blocks[i][j]);
            }
        }

    }

    public int RequestBlocks(int size)
    {
        if (size <= 0)
            return -1;

        Block currentBlock=null;
        int firstIndex = -1;

        List<Block> temp = new List<Block>();

        for(int i=0;i<rowNum;i++)
        {
            for (int j = 0; j < lineNum; j++)
            {
                if(blocks[i][j].blockStatusType==BlockStatusType.Free)
                {
                    if(currentBlock==null)
                    {
                        firstIndex = blocks[i][j].blockIndex;
                    }
                    else
                    {
                        currentBlock.nextblockIndex = blocks[i][j].blockIndex;                        
                    }
                    currentBlock = blocks[i][j];
                    currentBlock.blockStatusType = BlockStatusType.Used;
                    temp.Add(currentBlock);

                    if (temp.Count == size)
                        return firstIndex;              
                }
            }
        }


        if(temp.Count!=size)
        {
            temp.ForEach(x =>
            {
                x.blockStatusType = BlockStatusType.Free;
                x.nextblockIndex = -1;
            });

            return -1;
        }

        return firstIndex;
    }

    public void ReleaseBlock(int startBlockIndex)
    {
        int currentIndex = startBlockIndex;
        Block currentBlock;

        while(currentIndex!=-1)
        {          
            currentBlock = blocks[currentIndex / lineNum][currentIndex % lineNum];
            //Debug.Log(currentIndex / lineNum);
            //Debug.Log(currentIndex % lineNum);
            currentIndex = currentBlock.nextblockIndex;
            currentBlock.blockStatusType = BlockStatusType.Free;
            currentBlock.nextblockIndex = -1;
            //Debug.Log(currentIndex);
        }
    }
}

[System.Serializable]
public class Block
{
    public int blockIndex;                          //盘块号
    public BlockStatusType blockStatusType;         //状态（空闲，被占用）
    public int nextblockIndex;                      //下一个盘块号
}

public enum BlockStatusType
{
    Free=0,
    Used=1
}
