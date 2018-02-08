using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using Gamelogic.Extensions;
using UnityEngine;
using Zenject;

namespace Game
{  
    public class ItemController : MonoBehaviour
    {
        #region Fields

        [Inject] private GameController gameController;
        [Inject] private GameSettings gameSettings;
        [Inject] private Spawner spawner;
        [Inject] private GameField field;

        private Item item;
        private SpriteRenderer spriteRenderer;

        #endregion
        
        #region Public methods

        public void SetPosition(int x, int y)
        {
            item.PosX = x;
            gameObject.transform.SetX(x);

            item.PosY = y;
            gameObject.transform.SetY(y);
        }

        public void SetColor(Color color)
        {
            item.Color = color;
            spriteRenderer.color = color;
        }

        public ItemType GetItemType()
        {
            return item.Type;
        }

        public int GetPosX()
        {
            return item.PosX;
        }

        public int GetPosY()
        {
            return item.PosY;
        }

        public Color GetColor()
        {
            return item.Color;
        }
                
        #endregion
        
        #region Unity callbacks

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            if (item.Type == gameController.CorrectItem.Type && item.Color == gameController.CorrectItem.Color)
            {
                field.GetNeighbours(this)
                     .ForEach(itemController => itemController.SetColor(item.Color));
                spawner.ReleaseBulk(GetAllItems(item.Color, item.Type));
                gameController.Score += gameSettings.PointsSettings.CorrectClickReward;
            }
            else
            {
                GetAllItems(item.Color, item.Type)
                    .ForEach(itemController => itemController.SetColor(gameSettings.GetColor()));
                gameController.Score -= gameSettings.PointsSettings.IncorrectClickPenalty;
            }
        }

        #endregion
        
        #region Private methods
        
        private List<ItemController> GetAllItems(Color color, ItemType type)
        {
            var itemControllers = FindObjectsOfType<ItemController>();
            return itemControllers
                    .Where(itemController => itemController.GetItemType() == type && itemController.GetColor() == color)
                    .ToList();
        }
        
        [Inject]
        private void Init(ItemType type)
        {
            item = new Item(Color.clear, type);
        }
        
        #endregion
        
        public class SquareFactory : Factory<ItemType, ItemController>
        {
        }
    
        public class CircleFactory : Factory<ItemType, ItemController>
        {
        }
    
        public class TriangleFactory : Factory<ItemType, ItemController>
        {
        }
    }

    [Serializable]
    public class ItemSettings
    {
        #region Editor tweakable fields
        
        [SerializeField] private GameObject prefab;
        [SerializeField] private int itemMaxCount = 400;
        [SerializeField] private int appearRateMaxCount = 10;    
        
        #endregion
        
        #region Properties
        
        public GameObject Prefab
        {
            get { return prefab; }
        }

        public int ItemMaxCount
        {
            get { return itemMaxCount; }
        }

        public int AppearRateMaxCount
        {
            get { return appearRateMaxCount; }
        }

        #endregion
    }
}
