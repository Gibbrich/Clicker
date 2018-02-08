using System;
using System.Collections.Generic;
using Gamelogic.Extensions;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameField : MonoBehaviour
    {
        #region Private fields

        [Inject] private GameSettings gameSettings;
        private List<ValuePair<int, int>> freeFields;

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
        }

        #endregion

        #region Public methods

        public ValuePair<int, int> GetRandomFreeField()
        {
            var freeField = Utils.GetRandomItem(freeFields);
            freeFields.Remove(freeField);
            return freeField;
        }

        public void AddFreeField(int x, int y)
        {
            freeFields.Add(new ValuePair<int, int>(x, y));
        }

        /// <summary>
        /// Returns neighbours by grid offset 1 in all directions
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<Item> GetNeighbours(Item item)
        {
            /* todo    - implement
             * @author - Dvurechenskiyi
             * @date   - 08.02.2018
             * @time   - 14:10
            */
            return new List<Item>();
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