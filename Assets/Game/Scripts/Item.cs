using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Item
    {
        #region Private fields
    
        private int posX;
        private int posY;
        private Color color;
        private ItemType type;

        #endregion
    
        #region Properties
    
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public ItemType Type
        {
            get { return type; }
        }

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        #endregion
    
        #region Public methods

        public Item(Color color, ItemType type)
        {
            this.color = color;
            this.type = type;
        }

        #endregion
    }

    [Serializable]
    public enum ItemType
    {
        SQUARE,
        CIRCLE,
        TRIANGLE
    }
}