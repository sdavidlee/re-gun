 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Fundamentals
{
    public class Scroll<T>
    {
        public List<T> Items { get; private set; } = new List<T>();
        public int index;

        #region Consructors
        public Scroll(params T[] items)
        {
            this.Items = items.ToList();
        }

        public Scroll(List<T> items)
        {
            this.Items = items;
        }
        #endregion

        public T GetCurrent() => Items[index];

        public Scroll<T> MoveRight()
        {
            int finalIndex = Items.Count - 1;

            if (this.index == finalIndex)
            {
                index = 0;
                return this;
            }

            this.index += 1;
            return this;
        }

        public Scroll<T> MoveLeft()
        {
            int finalIndex = Items.Count - 1;

            if (this.index == 0)
            {
                index = finalIndex;
                return this;
            }

            this.index -= 1;
            return this;
        }
    }
}
