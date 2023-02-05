using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class LevelConstructor : MonoBehaviour
{
    
    enum BlockType
    {
        ground, wall, moveBlock, empty
    }
    
    [Serializable]
    struct BlockData
    {
        public Vector2 pos;
        public BlockType type;

    }

    [SerializeField] private GameObject[] blocks;
    [Header("Params")]
    [SerializeField] private Vector2 levelSize;
    [SerializeField] private List<BlockData> exeptionList;
    [SerializeField] private List<BlockData> topBlocks;
    private IBoardable[,] blockGrid;
    private IBoardable[,] topBlockGrid;

    private void Awake() => Bake();

    public void Bake()
    {
        ClearGrid();
        blockGrid = new IBoardable[(int) levelSize.x,(int)levelSize.y];
        topBlockGrid = new IBoardable[(int) levelSize.x,(int)levelSize.y];

        var currentType = BlockType.empty;
        for (int y = 0; y < levelSize.y; y++)
        {
            for (int x = 0; x < levelSize.x; x++)
            {
                var currentPos = new Vector2(x, y);
                if (x == 0 || y == 0 || x == levelSize.x - 1 || y == levelSize.y - 1) currentType = BlockType.wall;
                else currentType = BlockType.ground;
                foreach (var exeption in exeptionList)
                {
                    if (exeption.pos == currentPos) currentType = exeption.type;
                }

                if (currentType != BlockType.empty)
                {
                    var obj = Instantiate(blocks[(int)currentType], new Vector3(x, -0.5f, y) + transform.position,
                        Quaternion.identity, transform);
                    obj.name = $"({x},{y})";
                    var boardable = obj.GetComponent<IBoardable>();
                    blockGrid[x, y] = boardable ?? throw new NullReferenceException($"IBoardable isn't Set to {blocks[(int)currentType].name}");
                    boardable.SetMaster(this, currentPos);
                }

                foreach (var block in topBlocks)
                {
                    if (block.pos == currentPos)
                    {
                        var obj = Instantiate(blocks[(int)block.type], new Vector3(x, .5f, y), quaternion.identity,
                            transform);
                        obj.name = $"({x},{y}) TOP";
                        var boardable = obj.GetComponent<IBoardable>();
                        blockGrid[x, y] = boardable ?? throw new NullReferenceException($"IBoardable isn't Set to {blocks[(int)currentType].name}");
                        boardable.SetMaster(this, currentPos);
                        boardable.SetTopState(true);
                    }
                }
            }
        }
    }

    void ClearGrid()
    {
        var childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        blockGrid = null;
    }
    
    public IBoardable GetNeighboor(Vector2 boardPos, Enums.Side side)
    {
        switch (side)
        {
            case Enums.Side.up :
                if (boardPos.y+1 == levelSize.y) return null;
                return  blockGrid[(int)boardPos.x,(int)boardPos.y+1];
            case Enums.Side.left :
                if (boardPos.x == 0) return null;
                return  blockGrid[(int)boardPos.x-1,(int)boardPos.y];
            case Enums.Side.right :
                if (boardPos.x+1 == levelSize.x) return null;
                return  blockGrid[(int)boardPos.x+1,(int)boardPos.y];
            case Enums.Side.down :
                if (boardPos.y == 0) return null;
                return  blockGrid[(int)boardPos.x,(int)boardPos.y-1];
            default: return null;
        }
    }

    public IBoardable GetPosition(Vector2 position)
    {
        return blockGrid[(int)position.x, (int)position.y];
    }

    public bool TryMove(Vector2 boardPos, Enums.Side side, IBoardable sender, bool top = false)
    {
        var neighbour = GetNeighboor(boardPos, side);
        if (neighbour == null)
        {
            blockGrid[(int)boardPos.x, (int)boardPos.y] = null;
            switch (side)
            {
                case Enums.Side.up : blockGrid[(int)boardPos.x, (int)boardPos.y+1] = sender;
                    sender.SetPosition(boardPos+Vector2.up);
                    break;
                case Enums.Side.left : blockGrid[(int)boardPos.x-1, (int)boardPos.y] = sender;
                    sender.SetPosition(boardPos+Vector2.left);
                    break;
                case Enums.Side.right : blockGrid[(int)boardPos.x+1, (int)boardPos.y] = sender;
                    sender.SetPosition(boardPos+Vector2.right);
                    break;
                case Enums.Side.down : blockGrid[(int)boardPos.x, (int)boardPos.y-1] = sender;
                    sender.SetPosition(boardPos+Vector2.down);
                    break;
            }
            
            return true;
        }
        return false;
    }

}

