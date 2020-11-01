using System.Collections.Generic;
using UnityEngine;

public class LocationFactory : MonoBehaviour
{
    [SerializeField] private GameObject _whiteCell;
    [SerializeField] private GameObject _blackCell;
    [SerializeField] private List<GameObject> _additionalCells = new List<GameObject>();

    public GameObject GetFirstCell()
    {
        return _whiteCell;
    }

    public GameObject GetSecondCell()
    {
        return _blackCell;
    }

    public List<GameObject> GetAdditionalCells()
    {
        return _additionalCells;
    }
}
