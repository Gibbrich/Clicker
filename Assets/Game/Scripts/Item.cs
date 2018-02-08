using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;
using Zenject;

namespace Game
{
    /* todo    - split into ItemModel and ItemController
     * @author - Dvurechenskiyi
     * @date   - 08.02.2018
     * @time   - 14:19
    */    
    public class Item : MonoBehaviour
    {
        #region Fields

        [Inject] private GameController gameController;
        [Inject] private GameSettings gameSettings;
        [Inject] private ItemType type;
        [Inject] private Spawner spawner;
        [Inject] private GameField field;

        private int posX;
        private int posY;
        private Color color;
        private SpriteRenderer spriteRenderer;

        #endregion
        
        #region Properties
        
        public int PosX
        {
            get { return posX; }
            set
            {
                posX = value; 
                gameObject.transform.SetX(posX);
            }
        }

        public int PosY
        {
            get { return posY; }
            set
            {
                posY = value; 
                gameObject.transform.SetY(posY);
            }
        }

        public Color Color
        {
            get { return color; }
            set
            {
                color = value; 
                spriteRenderer.color = color;
            }
        }

        public ItemType Type
        {
            get { return type; }
        }

        #endregion
        
        #region Unity callbacks

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            if (type == gameController.CorrectItem.Type && color == gameController.CorrectItem.Color)
            {
                field.GetNeighbours(this).ForEach(item => item.Color = color);
                spawner.ReleaseBulk(GetAllItems(color, type));
                gameController.Score += gameSettings.PointsSettings.CorrectClickReward;
                print("Correct item clicked");
            }
            else
            {
                GetAllItems(color, type).ForEach(item => item.Color = gameSettings.GetColor());
                gameController.Score += gameSettings.PointsSettings.IncorrectClickPenalty;
                print("Incorrect item clicked");
            }
        }

        #endregion
        
        #region Private methods
        
        private List<Item> GetAllItems(Color color, ItemType type)
        {
            Item[] items = FindObjectsOfType<Item>();
            return items
                    .Where(item => item.Type == type && item.Color == color)
                    .ToList();
        }        
        
        #endregion
        
        public class SquareFactory : Factory<ItemType, Item>
        {
        }
    
        public class CircleFactory : Factory<ItemType, Item>
        {
        }
    
        public class TriangleFactory : Factory<ItemType, Item>
        {
        }
    
        public enum ItemType
        {
            SQUARE,
            CIRCLE,
            TRIANGLE
        }
    }

    [Serializable]
    public class ItemSettings
    {
        #region Editor tweakable fields
        
        [SerializeField] private GameObject prefab;
        [SerializeField] private int squareObjectMaxCount = 400;
        [SerializeField] private int appearRateMaxCount = 10;    
        
        #endregion
        
        #region Properties
        
        public GameObject Prefab
        {
            get { return prefab; }
        }

        public int SquareObjectMaxCount
        {
            get { return squareObjectMaxCount; }
        }

        public int AppearRateMaxCount
        {
            get { return appearRateMaxCount; }
        }

        #endregion
    }
}
