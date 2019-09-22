using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assets.Fundamentals
{
    public enum KeyNames {
        AttackKey,
        HorizontalKey,
        VerticalKey,
        SelectKey,
        StartKey,
        ThrowWeaponKey,
        RollKey
    }

    public class Keys
    {
        public int Index { get; private set; }

        private Dictionary<KeyNames, string> keysDict = new Dictionary<KeyNames, string>();

        public string this[KeyNames name]
        {
            get => keysDict[name];

            set
            {
                keysDict[name] = value;
            }
        }

        public Keys(int index)
        {
            SetIndex(index);
        }
        
        public void SetIndex(int index)
        {
            this.Index = index;

            foreach (KeyNames item in Enum.GetValues(typeof(KeyNames)))
            {
                keysDict[item] = item.ToString() + index.ToString();
            }
        }
    }
}
