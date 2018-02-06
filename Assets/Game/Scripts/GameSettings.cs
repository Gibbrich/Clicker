using System;
using UnityEngine;
using Zenject;

namespace Game
{
    [CreateAssetMenu(menuName = "Game/Game settings")]
    public class GameSettings : ScriptableObjectInstaller<GameSettings>
    {
        #region Editor tweakable fields

        [Header("General settings")] 
        [SerializeField] private GameController gameControllerPrefab;
        [SerializeField] private Spawner spawnerPrefab;
        
        [Space]
        [SerializeField] private GameFieldSettings gameFieldSettings;
        [SerializeField] private PointsSettings pointsSettings;
        [SerializeField] private TimingSettings timingSettings;
        
        [Header("Items")]
        [SerializeField] private ItemSettings squareSettings;
        [SerializeField] private ItemSettings circleSettings;
        [SerializeField] private ItemSettings triangleSettings;

        [Space]
        [SerializeField] private Color[] colors;

        #endregion
        
        #region Properties
        
        public GameFieldSettings FieldSettings
        {
            get { return gameFieldSettings; }
        }

        public PointsSettings PointsSettings
        {
            get { return pointsSettings; }
        }

        public TimingSettings TimingSettings
        {
            get { return timingSettings; }
        }

        #endregion
    
        #region Public methods

        public Color GetColor()
        {
            return Utils.GetRandomItem(colors);
        }
    
        #endregion

        #region Private methods

        public override void InstallBindings()
        {
            Container.BindFactory<Item, Item.SquareFactory>()
                     .WithId(Item.ObjectType.SQUARE)
                     .FromComponentInNewPrefab(squareSettings.Prefab)
                     .WithGameObjectName("Square")
                     .UnderTransformGroup("GameItems");

            Container.BindFactory<Item, Item.CircleFactory>()
                     .WithId(Item.ObjectType.CIRCLE)
                     .FromComponentInNewPrefab(circleSettings.Prefab)
                     .WithGameObjectName("Circle")
                     .UnderTransformGroup("GameItems");

            Container.BindFactory<Item, Item.TriangleFactory>()
                     .WithId(Item.ObjectType.TRIANGLE)
                     .FromComponentInNewPrefab(triangleSettings.Prefab)
                     .WithGameObjectName("Triangle")
                     .UnderTransformGroup("GameItems");

            Container.Bind<GameController>()
                     .FromComponentInNewPrefab(gameControllerPrefab)
                     .WithGameObjectName("GameController")
                     .AsSingle()
                     .NonLazy();

            Container.Bind<Spawner>()
                     .FromComponentInNewPrefab(spawnerPrefab)
                     .WithGameObjectName("Spawner")
                     .AsSingle()
                     .NonLazy();

            Container.BindInstance(this)
                     .AsSingle();
        }

        #endregion
    }

    [Serializable]
    public class TimingSettings
    {
        #region Editor tweakable fields
        
        [SerializeField] private float itemDisplayTime = 5f;
        [SerializeField] private float gameDuration = 60f;
        
        #endregion
        
        #region Properties
        
        public float ItemDisplayTime
        {
            get { return itemDisplayTime; }
        }

        public float GameDuration
        {
            get { return gameDuration; }
        }
        
        #endregion
    }

    [Serializable]
    public class PointsSettings
    {
        #region Editor tweakable fields
        
        [SerializeField] private int pointsTarget = 2000;
        [SerializeField] private int correctClickReward = 200;
        [SerializeField] private int incorrectClickPenalty = 50;
        [SerializeField] private int timeOutPenalty = 50;
        
        #endregion
        
        #region Properties
        
        public int PointsTarget
        {
            get { return pointsTarget; }
        }

        public int CorrectClickReward
        {
            get { return correctClickReward; }
        }

        public int IncorrectClickPenalty
        {
            get { return incorrectClickPenalty; }
        }

        public int TimeOutPenalty
        {
            get { return timeOutPenalty; }
        }        
        
        #endregion        
    }
}