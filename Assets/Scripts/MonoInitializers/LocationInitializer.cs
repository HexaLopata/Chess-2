using UnityEngine;

/// <summary>
/// Инициализирует текущую локацию
/// </summary>
public class LocationInitializer : MonoBehaviour
{
    [SerializeField] private LocationFactory _locationFactory;

    private void Start()
    {
        Core.CurrentLocation.MainFieldWhiteCell = _locationFactory.GetFirstMainCell();
        Core.CurrentLocation.MainFieldBlackCell = _locationFactory.GetSecondMainCell();
        Core.CurrentLocation.BattleFieldWhiteCell = _locationFactory.GetFirstBattleCell();
        Core.CurrentLocation.BattleFieldBlackCell = _locationFactory.GetSecondBattleCell();
        Core.CurrentLocation.AdditionalCells = _locationFactory.GetAdditionalCells();
    }
}