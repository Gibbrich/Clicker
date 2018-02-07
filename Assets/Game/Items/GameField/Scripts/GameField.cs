using System;
using System.Collections.Generic;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameField : MonoBehaviour
    {
        #region Private fields

        [Inject] private GameSettings gameSettings;
        private Item[,] field;

        #endregion
        
        #region Unity callbacks

        private void Start()
        {
            field = new Item[gameSettings.FieldSettings.Width, gameSettings.FieldSettings.Height];
        }

        #endregion
        
        #region Public methods

        /* todo    - consider to make a property. Add/delete free/placed coordinates for performance optimization
         * @author - Dvurechenskiyi
         * @date   - 07.02.2018
         * @time   - 17:48
        */        
        public List<Tuple<int, int>> GetFreeFields()
        {
            List<Tuple<int, int>> list = new List<Tuple<int, int>>();
            
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (!field[i,j])
                    {
                        list.Add(Tuple.Create(i, j));
                    }
                }
            }

            return list;
        }

        public void SetItem(int width, int height, Item item)
        {
            field[width, height] = item;
        }
        
        #endregion
    }

    [Serializable]
    public class GameFieldSettings
    {
        #region Editor tweakable fields
        
        [SerializeField] private int width = 32;
        [SerializeField] private int height = 32;            
        
        #endregion
        
        #region Properties
        
        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        #endregion
    }
}