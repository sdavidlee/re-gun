using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GameUI
{
    public class ProgressBar : MonoBehaviour
    {
        private RectTransform Rect { get; set; }

        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }

        public void ScaleBar(float scalar)
        {
            var clampedScalar = Mathf.Clamp(value: scalar, min: 0, max: 1);
            Rect.localScale = new Vector3(clampedScalar, 1, 1);
        }
    }
}
