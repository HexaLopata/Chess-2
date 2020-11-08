using UnityEngine;
/// <summary>
/// Этот класс создан лишь потому, что на elementary os в редакторе unity нельзя задать цвет фона, в будущем его можно использовать в более широких целях 
/// </summary>
public class CameraBackGroundColorSetter : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Camera>().backgroundColor = new Color(0.282352941f, 0.282352941f, 0.282352941f); // rgb: 72, 72, 72 
    }
}