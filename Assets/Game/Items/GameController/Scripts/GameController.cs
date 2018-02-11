using System;
using Gamelogic.Extensions;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        #region Private fields

        [Inject] private GameSettings gameSettings;
        [Inject] private GameField gameField;
        [Inject] private Spawner spawner;
        [Inject] private UIController uiController;
    
        private Clock clock;

        private Item correctItem;
        private float correctItemShowTimeStamp = 0;

        private int score;
        private StateMachine<GameState> stateMachine;

        #endregion
    
        #region Properties

        public int Score
        {
            get { return score; }
            set
            {
                score = Mathf.Clamp(value, 0, gameSettings.PointsSettings.PointsTarget);
                uiController.UpdateScoreDisplay(score);

                if (score >= gameSettings.PointsSettings.PointsTarget)
                {
                    stateMachine.CurrentState = GameState.FINISHED;
                }
            }
        }

        public Item CorrectItem
        {
            get { return correctItem; }
        }

        public GameState State
        {
            get { return stateMachine.CurrentState; }
        }

        #endregion
    
        #region Unity callbacks

        private void Start()
        {
            clock = new Clock();
            clock.AddTime(gameSettings.TimingSettings.GameDuration);
            clock.OnSecondsChanged += OnSecondsChanged;
            clock.OnClockExpired += OnClockExpired;
            clock.Unpause();
        
            ChangeCorrectItem();
            uiController.UpdateTimerDisplay(clock.TimeInSeconds);
            uiController.UpdateScoreDisplay(score);
            uiController.UpdatePlayerNameDisplay();
            
            stateMachine = new StateMachine<GameState>();
            stateMachine.AddState(GameState.PLAY, PlayStateOnStart, PlayStateOnUpdate);
            stateMachine.AddState(GameState.PAUSE, PauseStateOnStart, PauseStateOnUpdate);
            stateMachine.AddState(GameState.FINISHED, FinishedStateOnStart);
            stateMachine.CurrentState = GameState.PLAY;
        }

        private void Update()
        {           
            stateMachine.Update();
        }

        private void OnDestroy()
        {
            clock.OnSecondsChanged -= OnSecondsChanged;
            clock.OnClockExpired -= OnClockExpired;
        }

        #endregion
    
        #region Public methods
    
        public void ChangeCorrectItem()
        {
            var itemTypes = Utils.GetEnumValues<ItemType>();
            ItemType itemType = Utils.GetRandomItem(itemTypes);

            correctItem = new Item(gameSettings.GetColor(), itemType);
            uiController.UpdateDisplayedItem(correctItem.Type, correctItem.Color);
        
            correctItemShowTimeStamp = Time.time;
        }
        
        public void UnpauseGame()
        {
            stateMachine.CurrentState = GameState.PLAY;
        }
    
        #endregion
    
        #region Private methods

        private void OnSecondsChanged()
        {
            uiController.UpdateTimerDisplay(clock.TimeInSeconds);
        
            PlaceItem(spawner.PlaceSquare, gameSettings.SquareSettings.AppearRateMaxCount);
            PlaceItem(spawner.PlaceCircle, gameSettings.CircleSettings.AppearRateMaxCount);
            PlaceItem(spawner.PlaceTriangle, gameSettings.TriangleSettings.AppearRateMaxCount);
        }
    
        private void OnClockExpired()
        {
            stateMachine.CurrentState = GameState.FINISHED;
        }

        private void PlaceItem(Action placeItemAction, int maxCount)
        {
            for (int i = 0; i < maxCount; i++)
            {
                if (gameField.HasFreeFields())
                {
                    placeItemAction();                
                }
            }
        }

        private void PlayStateOnStart()
        {
            clock.Unpause();
            uiController.CloseMenuPanel();
        }

        private void PlayStateOnUpdate()
        {
            clock.Update(Time.deltaTime);

            if (Time.time - correctItemShowTimeStamp >= gameSettings.TimingSettings.ItemDisplayTime)
            {
                ChangeCorrectItem();
                Score -= gameSettings.PointsSettings.TimeOutPenalty;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                stateMachine.CurrentState = GameState.PAUSE;
            }
        }
        
        private void PauseStateOnStart()
        {
            clock.Pause();
            uiController.OpenMenuPanel(true);
        }

        private void PauseStateOnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnpauseGame();
            }
        }

        private void FinishedStateOnStart()
        {
            uiController.OpenMenuPanel(false);

            if (score > PreferenceManager.GetPlayerRecord())
            {
                PreferenceManager.SetPlayerNameAndPoints(PreferenceManager.GetPlayerName(), score);                
            }
        }
    
        #endregion
    }

    public enum GameState
    {
        PLAY,
        PAUSE,
		FINISHED
    }
}