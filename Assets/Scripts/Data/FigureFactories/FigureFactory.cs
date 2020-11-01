using UnityEngine;

public abstract class FigureFactory : MonoBehaviour
{
    [SerializeField] protected GameObject _mainFieldFigurePrefub;
    [SerializeField] protected GameObject _battleFieldFigurePrefub;

    abstract public Figure GetFigure();
}
