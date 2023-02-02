using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private bool isInit;
    [SerializeField] private bool debug;
    [SerializeField] private Vector2 gridSize;
    private Tile[,] grid;
    [Space]
    [SerializeField] float tileSize = 1;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        grid = new Tile[(int)gridSize.x, (int)gridSize.y];
        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                grid[x, y] = new()
                {
                    worldPos = transform.position + new Vector3(x*tileSize, 0 , y*tileSize)
                };
            }
        }

        isInit = true;
    }

    private void OnDrawGizmos()
    {
        if (isInit && debug)
        {
            foreach (var tile in grid)
            {
                Gizmos.DrawSphere(tile.worldPos, .1f);
            }
        }
    }

    public Tile GetNeighboor(Vector2 boardPos, Enums.Side side)
    {
        switch (side)
        {
            case Enums.Side.up :
                if (boardPos.y+1 == gridSize.y) return null;
                return  grid[(int)boardPos.x,(int)boardPos.y+1];
            case Enums.Side.left :
                if (boardPos.x == 0) return null;
                return  grid[(int)boardPos.x-1,(int)boardPos.y];
            case Enums.Side.right :
                if (boardPos.x+1 == gridSize.x) return null;
                return  grid[(int)boardPos.x+1,(int)boardPos.y];
            case Enums.Side.down :
                if (boardPos.y == 0) return null;
                return  grid[(int)boardPos.x,(int)boardPos.y-1];
            default: return null;
        }
    }
}

public class Tile
{
    public GameObject objectOnTile;
    public Vector3 worldPos;
} 
