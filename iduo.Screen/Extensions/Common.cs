using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iduo.Screen.Extensions
{
    public class Common
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int[] SortId(int[] id)
        {
            int temp = 0;
            for (int i = 0; i < id.Length - 1; i++)
            {
                for (int j = 0; j < id.Length - 1 - i; j++)
                {
                    if (id[j] > id[j + 1])
                    {
                        temp = id[j];
                        id[j] = id[j + 1];
                        id[j + 1] = temp;
                    }
                }
            }
            return id;
        }
        /// <summary>
        /// 查找相邻的课程ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int[] GetIntXLID(int[] id)
        {
            List<int> ids = new List<int>();
            for (int i = 0; i < id.Length - 1; i++)
            {
                var item = id[i];
                var next = id[i + 1];
                var temp = next - item;
                if (temp == 1)
                {
                    var q = ids.Contains(item);
                    var a = ids.Contains(next);
                    if (!q)
                        ids.Add(item);
                    if (!a)
                        ids.Add(next);
                }
            }
            var idArray = ids.ToArray();
            return idArray;
        }
    }
}