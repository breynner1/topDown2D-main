using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Grid : ScriptableObject
{
    private int width;
    private int height;
    private int cellSize;
    private Cell cellPrefab;
    public Cell[,] gridArray;
    public GameObject padre;

    public Grid(int width, int height, int cellSize, Cell cellPrefab)
    {
        
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.cellPrefab = cellPrefab;

        generateBoard();
    }

    private void generateBoard()
    {
        Cell cell;
        gridArray = new Cell[width, height];
        padre = new GameObject("Terrainpadre");
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var p = new Vector2(i, j) * cellSize;
                cell = Instantiate(cellPrefab,  p, Quaternion.identity);
                //cell.transform.Setpadre(padre.transform);
                cell.Init(this, (int)p.x, (int)p.y, true);

                cell.transform.SetParent(padre.transform);

                //if (Random.Range(0, 10) <= 2)
                //    cell.SetWalkable(false);
                //else
                //    cell.SetColor(Color.blue);

                gridArray[i, j] = cell;
            }
        }

        var center = new Vector2((float)height / 2 - 0.5f, (float)width / 2 - 0.5f);

        Camera.main.transform.position = new Vector3(center.y, center.x, -5);
    }

    internal int GetHeight()
    {
        return height;
    }

    internal int GetWidth()
    {
        return width;
    }

    public void setBusyCell(int initialX,int initialY, int newX, int newY)
    {
        gridArray[initialX, initialY].SetWalkable(true);
        gridArray[newX, newY].SetWalkable(false);
    }
    public void morir(int initialX, int initialY)
    {
        gridArray[initialX, initialY].SetWalkable(true);
    }

    public bool isWalkable(int x, int y)
    {
        return gridArray[x, y].isWalkable;
    }

    public Cell GetGridObject(int x, int y)
    {
        return gridArray[x, y];
    }

    internal float GetCellSize()
    {
        return cellSize;
    }
}
