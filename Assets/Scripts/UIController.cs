using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreUI;
    [SerializeField] private Transform _losePanel;
    [SerializeField] private Transform _controlPanel;
    [SerializeField] private Button _replayButton;

    private void Start()
    {
        GameController.Instance.OnScoreUpdate += UpdateScoreUI;
        GameController.Instance.OnStageChange += state =>
        {
            ShowLosePanelIf(state == GameController.GameState.over);
            ShowControlPanelIf(state == GameController.GameState.run);
        };

        _replayButton.onClick.AddListener(() => GameController.Instance.State = GameController.GameState.run);
    }

    private void UpdateScoreUI(int score)
    {
        _scoreUI.text = score.ToString();
    }

    private void ShowLosePanelIf(bool canShow)
    {
        _losePanel.gameObject.SetActive(canShow);
    }
    
    private void ShowControlPanelIf(bool canShow)
    {
        _controlPanel.gameObject.SetActive(canShow);
    }
}
