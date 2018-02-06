using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game
{
    public class Item : MonoBehaviour
    {
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
