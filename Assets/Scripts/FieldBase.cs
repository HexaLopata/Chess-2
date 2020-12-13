using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс для клечтатого поля
/// </summary>
public abstract class FieldBase : MonoBehaviour
{
    public CellBase[,] Cells { get; protected set; }

    protected GameObject _whiteCell;
    protected GameObject _blackCell;
    protected List<GameObject> _additionalCells;
    public int Width { get; protected set; }
    public int Height { get; protected set; }
    protected float _cellWidth;
    protected float _cellHeight;

    /// <summary>
    /// Использовать в потомках вместо Awake
    /// </summary>
    protected abstract void AdditionalAwakeInit();
    /// <summary>
    /// Использовать в потомках вместо Start
    /// </summary>
    protected abstract void AdditionalStartInit();

    private void Awake()
    {
        AdditionalAwakeInit();
        if (_whiteCell == null || _blackCell == null || _additionalCells == null)
            throw new Exception("Клетки не были инициализированы в подклассе, задайте их в методе AdditionalAwakeInit");
        if (Width == 0 || Height == 0)
            throw new Exception("Размеры поля не были заданы, задайте их в методе AdditionalAwakeInit");
        Cells = new CellBase[Width, Height];
    }

    private void Start()
    {
        InitAllCells();
        AdditionalStartInit();
    }

    /// <summary>
    /// Расставляет клетки в зависимости от ширины поля
    /// </summary>
    private void InitAllCells()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                GameObject cell;
                if ((x % 2 == 1 && y % 2 == 0) || (x % 2 == 0 && y % 2 == 1))
                    cell = Instantiate(_whiteCell, transform);
                else
                    cell = Instantiate(_blackCell, transform);
                if (_cellHeight == 0 || _cellWidth == 0)
                {
                    _cellWidth = cell.GetComponent<RectTransform>().rect.width;
                    _cellHeight = cell.GetComponent<RectTransform>().rect.height;
                }
                cell.transform.localPosition = new Vector2((x - Width / 2) * _cellWidth,
                        (y - Height / 2) * _cellHeight);
                Cells[x, y] = cell.GetComponent<CellBase>();
                cell.GetComponent<CellBase>().OnBoardPosition = new Vector2Int(x, y);
            }
        }

        if (Width % 2 == 1)
        {
            GetComponent<RectTransform>().localPosition = new Vector2(GetComponent<RectTransform>().localPosition.x - _cellWidth / 2, GetComponent<RectTransform>().localPosition.y);
        }
        if (Height % 2 == 1)
        {
            GetComponent<RectTransform>().localPosition = new Vector2(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y - _cellHeight / 2);
        }
    }

    /// <summary>
    /// Вспомогательный метод для определения, существует ли клетка в заданном направлении
    /// </summary>
    /// <param name="xOffset">Смещение от стартового x</param>
    /// <param name="yOffset">Смещение от стартового y</param>
    /// <param name="localX">Стартовый x</param>
    /// <param name="localY">Стартовый y</param>
    /// <returns></returns>
    public bool IsCellExists(int localX, int localY, int xOffset, int yOffset)
    {
        bool result;
        if (xOffset > 0)
            result = Cells.GetLength(0) > localX + xOffset;
        else if (xOffset < 0)
            result = localX > 0;
        else
            result = true;

        if (yOffset > 0)
            result = result && Cells.GetLength(1) > localY + yOffset;
        else if (yOffset < 0)
            result = result && localY > 0;
        return result;
    }

    /// <summary>
    /// Вспомогательный метод для определения, существует ли клетка в заданной точке
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool IsCellExists(int x, int y)
    {
        return (x >= 0) && (x < Cells.GetLength(0)) && (y >= 0) && (y < Cells.GetLength(1));
    }
}