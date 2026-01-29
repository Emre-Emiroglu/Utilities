using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Runtime
{
    /// <summary>
    /// Provides various runtime utility methods for game logic.
    /// </summary>
    public static class RuntimeUtilities
    {
        #region Executes
        /// <summary>
        /// Fades the alpha value of a CanvasGroup over a specified duration.
        /// </summary>
        /// <param name="canvasGroup">The CanvasGroup to modify.</param>
        /// <param name="targetValue">The target alpha value.</param>
        /// <param name="duration">The duration of the fade.</param>
        /// <returns>An IEnumerator for use in a coroutine.</returns>
        public static IEnumerator SetCanvasGroupAlpha(CanvasGroup canvasGroup, float targetValue, float duration = 1f)
        {
            float t = 0f;
            float startValue = canvasGroup.alpha;
            
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                
                canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, t);
                
                yield return null;
            }
        }
        
        /// <summary>
        /// Shuffles a list of items randomly.
        /// </summary>
        /// <param name="ts">The list to shuffle.</param>
        /// <returns>A new list with the shuffled items.</returns>
        public static List<T> Shuffle<T>(List<T> ts)
        {
            List<T> newList = ts;
            int count = newList.Count;
            var last = count - 1;
            for (int i = 0; i < last; i++)
            {
                var r = Random.Range(i, count);
                (newList[r], newList[i]) = (newList[i], newList[r]);
            }
            return newList;
        }
        
        /// <summary>
        /// Sorts a list of integers using the bubble sort algorithm.
        /// </summary>
        /// <param name="ts">The list of integers to sort.</param>
        /// <returns>A sorted list of integers.</returns>
        public static IList<int> BubbleSort(IList<int> ts)
        {
            IList<int> newList = ts;
            int count = newList.Count;
            for (int i = 0; i < count - 1; i++)
                for (int j = 0; j < count - 1; j++)
                    if (newList[j] > newList[j + 1])
                        (newList[j], newList[j + 1]) = (newList[j + 1], newList[j]);
            
            return newList;
        }
        #endregion
    }
}