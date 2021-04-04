using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class ListUtils
    {
        public static IEnumerable<TSource> MixRandomly<TSource>(this List<TSource> me)
        {
            int amountLeft = me.Count;
            var mixedList = new List<TSource>();

            if (me.Count <= 1) return me;
            
            while (amountLeft > 0)
            {
                var randomIndex = Random.Range(0, amountLeft - 1);
                mixedList.Add(me[randomIndex]);
                
                if (randomIndex != amountLeft - 1)
                {
                    var itemToSwap = me[amountLeft - 1];
                    me[amountLeft - 1] = me[randomIndex];
                    me[randomIndex] = itemToSwap;    
                }

                amountLeft--;
            }

            return mixedList;
        }
    }
}