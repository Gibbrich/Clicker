using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIController : MonoBehaviour
{
    #region Editor tweakable fields

    [SerializeField] private UIInput nameInputField;
    [SerializeField] private UIButton playButton;
    [SerializeField] private UILabel recordLabel;
    
    #endregion
    
    #region Unity callbacks

    private void Start()
    {
        ChangePlayButtonEnabledState();
        SetRecordLabelValue();
    }

    #endregion
    
    #region Public methods
    
    public void OnPlayButtonClick()
    {
        PreferenceManager.SetPlayerNameAndPoints(nameInputField.value, PreferenceManager.GetPlayerRecord());
        SceneManager.LoadScene("Level1");
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnNameInputFieldTextChange()
    {
        ChangePlayButtonEnabledState();
    }
    
    #endregion
    
    #region Private methods

    private void ChangePlayButtonEnabledState()
    {
        playButton.isEnabled = !string.IsNullOrEmpty(nameInputField.value);
    }

    private void SetRecordLabelValue()
    {
        if (string.IsNullOrEmpty(PreferenceManager.GetPlayerName()))
        {
            recordLabel.gameObject.SetActive(false);
        }
        else
        {
            recordLabel.gameObject.SetActive(true);
            
            StringBuilder sb = new StringBuilder();
            sb.Append("Player name: ").AppendLine(PreferenceManager.GetPlayerName());
            sb.Append("Record: ").Append(PreferenceManager.GetPlayerRecord());

            recordLabel.text = sb.ToString();
        }
    }
    
    #endregion
}