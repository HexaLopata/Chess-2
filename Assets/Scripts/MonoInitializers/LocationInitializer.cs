﻿using UnityEngine;

public class LocationInitializer : MonoBehaviour
{
    [SerializeField] private LocationFactory _locationFactory;

    private void Start()
    {
        Core.currentLocation.WhiteCell = _locationFactory.GetFirstCell();
        Core.currentLocation.BlackCell = _locationFactory.GetSecondCell();
        Core.currentLocation.AdditionalCells = _locationFactory.GetAdditionalCells();
    }
}