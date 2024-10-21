using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Interactables
{
    public interface IHoverAble
    {
        public void OnHovered();

        public void OnStoppedHovering();
    }
}