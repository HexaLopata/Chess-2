using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class FieldBase : MonoBehaviour
{
        public Cell[,] Cells { get; protected set; }
        
        protected GameObject _whiteCell;
        protected GameObject _blackCell;
        protected List<GameObject> _additionalCells;
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        private float _cellWidth;
        private float _cellHeight;
        
        protected abstract void AdditionalAwakeInit();
        protected abstract void AdditionalStartInit();
        
        private void Awake()
        {
                AdditionalAwakeInit();
                if (_whiteCell == null || _blackCell == null || _additionalCells == null)
                        throw new Exception("Клетки не были инициализированы в подклассе, задайте их в методе AdditionalAwakeInit");
                if(Width == 0 || Height == 0)
                        throw new Exception("Размеры поля не были заданы, задайте их в методе AdditionalAwakeInit");
                Cells = new Cell[Width, Height];
        }

        private void Start()
        { 
                InitAllCells();
                AdditionalStartInit();
        }
        
        private void InitAllCells()
        {
                for (int x = 0; x < Width; x++)
                {
                        for (int y = 0; y < Height; y++)
                        { 
                                GameObject cell;
                                if ( (x % 2 == 1 && y % 2 == 0) || (x % 2 == 0 && y % 2 == 1) )
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
                                Cells[x, y] = cell.GetComponent<Cell>();
                                cell.GetComponent<Cell>().OnBoardPosition = new Vector2Int(x, y);
                        }
                }
        }
}