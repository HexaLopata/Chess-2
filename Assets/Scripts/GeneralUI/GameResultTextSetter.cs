using UnityEngine;

/// <summary>
/// Выставляет результат игры в переданный текст
/// </summary>
public class GameResultTextSetter : MonoBehaviour
{
    [SerializeField] private Chess2Text _text1;
    [SerializeField] private Chess2Text _text2;
    
    void Start()
    {
        string resultText = string.Empty;
        switch (Core.GameResult)
        {
              case GameResult.BlackWon:
                  resultText = "Black Wins";
                  break;
              case GameResult.WhiteWon:
                  resultText = "White Wins";
                  break;
              case GameResult.Draw:
                  resultText = "Draw";
                  break;
        }

        _text1.Text = resultText;
        _text2.Text = resultText;
    }
}
