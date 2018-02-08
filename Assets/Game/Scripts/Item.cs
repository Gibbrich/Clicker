using System;
using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Item : MonoBehaviour
    {
        #region Fields

        private SpriteRenderer spriteRenderer;
        
        #endregion
        
        #region Unity callbacks

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        #endregion
        
        #region Public methods

        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void SetPosition(int posX, int posY)
        {
            gameObject.transform.SetX(posX);
            gameObject.transform.SetY(posY);
        }
        
        #endregion
        
        public class SquareFactory : Factory<Item>
        {
        }
    
        public class CircleFactory : Factory<Item>
        {
        }
    
        public class TriangleFactory : Factory<Item>
        {
        }
    
        public enum ObjectType
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
