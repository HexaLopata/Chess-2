using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Exit);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
