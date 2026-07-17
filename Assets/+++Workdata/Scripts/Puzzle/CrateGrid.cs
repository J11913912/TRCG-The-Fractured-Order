using System;
using UnityEngine;
public class CrateGrid : MonoBehaviour
{
    //  public static Action<int, int> GetGrid;
    public static Action<int[,]> SetGrid;
    
    public int[,] grid;

    public int posX;
    public int PosX => posX;
    
    public int posY;
    public int PosY => posY;

    public int maxX;
    public int maxY;

    public virtual void Awake()
    {
        grid = new int[5, 7]
        {
            {1, 1, 1, 0, 0, 0, 1},
            {1, 0, 0, 0, 1, 1, 0},
            {1, 0, 1 ,1, 1, 1, 0},
            {1, 1, 1, 1, 1, 1, 0},
            {1, 0, 0, 0, 0, 0, 0}
            // start pos
        };

        posX = 0;
        posY = 4;
        
        maxX = grid.GetLength(1);
        maxY = grid.GetLength(0);
    }
    
    public int GetGrid(int x, int y)
    {
        if (x > maxX - 1 || x < 0 || y > maxY - 1 || y < 0)
        {
            return 100;
        }
        else
        {
            return grid[y, x];
        }
    }

    public void SetPositionX(int x)
    {
        posX = x;

        if (posX > maxX - 1)
        {
            posX = 6;
        }
        
        if (posX < 0)
        {
            posX = 0;
        }
    }
    
    public void SetPositionY(int y)
    {
        posY = y;
        
        if (posY > maxY - 1)
        {
            posY = 4;
        }
        
        if (posY < 0)
        {
            posY = 0;
        }
    }

    public virtual void RemoveBarrier()
    {
        grid[0, 4] = 1;
        grid[0, 5] = 1;
    }

    public virtual void LockCrate()
    {
        grid[0, 5] = 0;
    }
}