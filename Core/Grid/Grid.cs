using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    int _width;
    int _height;
    float _cellSize;

    int[,] gridArray;

    public Grid(int width, int height, float cellSize)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;

        gridArray = new int[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                Utils.CreateMarker(GetWorldPosition(x, z) 
                    + new Vector3(cellSize,0,cellSize) * 0.5f);
                Debug.DrawLine(GetWorldPosition(x, z),
                    GetWorldPosition(x, z + 1),Color.white, 
                    100f);
                Debug.DrawLine(GetWorldPosition(x, z),
                    GetWorldPosition(x+1, z),Color.white, 
                    100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height),
            GetWorldPosition(width, height),Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0),
            GetWorldPosition(width, height),Color.white, 100f);
    }

    private Vector3 GetWorldPosition(int x, int z) 
    {
        return new Vector3(x,0,z) * _cellSize;
    }

    void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPosition.x / _cellSize);
        z = Mathf.FloorToInt(worldPosition.z / _cellSize);
    }

    private void SetValue(int x, int y, int value) 
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            gridArray[x,y] = value;

        }
    }

    public void SetValue(Vector3 worldPositon, int value) 
    {
        int x, z;
        GetXZ(worldPositon, out x, out z);
    }


}
