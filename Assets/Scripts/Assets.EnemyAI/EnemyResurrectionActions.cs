using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.EnemyAI
{
    public abstract class EnemyResurrectionActions : MonoBehaviour
    {
        protected Enemy Self { get; set; }
        public abstract void Resurrect(bool shouldRessurect = true);
        public abstract void UponResurrectionUpdateInfo();
    }
}
