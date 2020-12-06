using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Фабрика набора клеток для локации
/// </summary>
public class LocationFactory : MonoBehaviour
{
    [SerializeField] private GameObject _mainFieldWhiteCell;
    [SerializeField] private GameObject _mainFieldBlackCell;
    [SerializeField] private GameObject _battleFieldWhiteCell;
    [SerializeField] private GameObject _battleFieldBlackCell;
    
    [SerializeField] private List<GameObject> _additionalCells = new List<GameObject>();

    public GameObject GetFirstMainCell()
    {
        return _mainFieldWhiteCell;
    }

    public GameObject GetSecondMainCell()
    {
        return _mainFieldBlackCell;
    }
    
    public GameObject GetFirstBattleCell()
    {
        return _battleFieldWhiteCell;
    }

    public GameObject GetSecondBattleCell()
    {
        return _battleFieldBlackCell;
    }

    public List<GameObject> GetAdditionalCells()
    {
        return _additionalCells;
    }
}
