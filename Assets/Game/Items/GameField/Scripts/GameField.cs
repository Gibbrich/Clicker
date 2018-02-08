using System;
using System.Collections.Generic;
using Gamelogic.Extensions;
using ModestTree.Util;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game
{
    public class GameField : MonoBehaviour
    {
        #region Private fields

        [Inject] private GameSettings gameSettings;
        private List<ValuePair<int, int>> freeFields;

        private Item[,] matrix;

        #endregion

        #region Unity callbacks

        private void Start()
        {
            int capacity = gameSettings.FieldSettings.Width * gameSettings.FieldSettings.Height;
            freeFields = new List<ValuePair<int, int>>(capacity);

            for (int i = 0; i < gameSettings.FieldSettings.Width; i++)
            {
                for (int j = 0; j < gameSettings.FieldSettings.Height; j++)
                {
                    freeFields.Add(new ValuePair<int, int>(i, j));
                }
            }

            matrix = new Item[gameSettings.FieldSettings.Width, gameSettings.FieldSettings.Height];
        }

        #endregion

        #region Public methods

        public ValuePair<int, int> GetRandomFreeField()
        {
            var freeField = Utils.GetRandomItem(freeFields);
            /* todo    - after 30 sec spawn sometimes items in 0;0. Fix it
             * @author - Артур
             * @date   - 08.02.2018
             * @time   - 23:31
            */            
            freeFields.Remove(freeField);
            return freeField;
            
        }

        public int[] GetRandomFreeFieldNew()
        {
            /* todo    - finish
             * @author - Артур
             * @date   - 08.02.2018
             * @time   - 23:53
            */            
            List<int[]> freeFieldsList = new List<int[]>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == null)
                    {
                        freeFieldsList.Add(new []{i, j});
                    }
                }
            }

            var freeField = Utils.GetRandomItem(freeFieldsList);
            return freeField;
        }

        public void AddFreeField(int x, int y)
        {
            freeFields.Add(new ValuePair<int, int>(x, y));
        }

        /// <summary>
        /// Returns neighbours by grid offset 1 in all directions
        /// </summary>
        /// <param name="itemController"></param>
        /// <returns></returns>
        public List<ItemController> GetNeighbours(ItemController itemController)
        {
            /* todo    - implement
             * @author - Dvurechenskiyi
             * @date   - 08.02.2018
             * @time   - 14:10
            */
            return new List<ItemController>();
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