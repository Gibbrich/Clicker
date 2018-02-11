using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using Zenject;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField] private UILabel playerNameLabel;
    [SerializeField] private UILabel timerLabel;
    [SerializeField] private UILabel scoreLabel;
    [SerializeField] private UIItem correctItemUIController;
    [SerializeField] private MenuPanelController menuPanel;

	[Inject] private GameController gameController;

    #endregion
    
    #region Public methods

    public void UpdateTimerDisplay(int time)
    {
        timerLabel.text = time.ToString();
    }

    public void UpdateDisplayedItem(ItemType type, Color color)
    {
        correctItemUIController.ChangeDisplay(type, color);
    }

    public void UpdateScoreDisplay(int score)
    {
        scoreLabel.text = score.ToString();
    }

    public void UpdatePlayerNameDisplay()
    {
        playerNameLabel.text = PreferenceManager.GetPlayerName();
    }

    public void OpenMenuPanel(bool isPause)
    {
        correctItemUIController.gameObject.SetActive(false);
        menuPanel.Open(isPause);
    }

    public void CloseMenuPanel()
    {
        correctItemUIController.gameObject.SetActive(true);
        menuPanel.Close();
    }

	public void OnResumeButtonClick()
	{
		gameController.UnpauseGame();
	}

	public void OnExitButtonClick()
	{
		Application.Quit ();
	}

	public void OnMainMenuButtonClick()
	{
		SceneManager.LoadScene ("MainMenu");
	}

    
    #endregion
}