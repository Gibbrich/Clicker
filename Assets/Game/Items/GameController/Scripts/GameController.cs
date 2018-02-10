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
        private GameState state;

        #endregion
    
        #region Properties

        public int Score
        {
            get { return score; }
            set
            {
                score = Mathf.Clamp(value, 0, gameSettings.PointsSettings.PointsTarget);
                uiController.UpdateScoreDisplay(score);
            }
        }

        public Item CorrectItem
        {
            get { return correctItem; }
        }

        public GameState State
        {
            get { return state; }
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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (state == GameState.PAUSE)
                {
                    state = GameState.PLAY;
                    clock.Unpause();
                    uiController.ClosePauseMenu();
                }
                else
                {
                    state = GameState.PAUSE;
                    clock.Pause();
                    uiController.OpenPauseMenu();
                }
            }
            
            if (state == GameState.PLAY)
            {
                clock.Update(Time.deltaTime);

                if (Time.time - correctItemShowTimeStamp >= gameSettings.TimingSettings.ItemDisplayTime)
                {
                    ChangeCorrectItem();
                    Score -= gameSettings.PointsSettings.TimeOutPenalty;
            
                }
            }
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
            state = GameState.PAUSE;
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
    
        #endregion
    }

    public enum GameState
    {
        PLAY,
        PAUSE
    }
}