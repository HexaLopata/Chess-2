using UnityEngine;

public class LocationInitializer : MonoBehaviour
{
    [SerializeField] private LocationFactory _locationFactory;

    private void Start()
    {
        Core.CurrentLocation.WhiteCell = _locationFactory.GetFirstCell();
        Core.CurrentLocation.BlackCell = _locationFactory.GetSecondCell();
        Core.CurrentLocation.AdditionalCells = _locationFactory.GetAdditionalCells();
    }
}