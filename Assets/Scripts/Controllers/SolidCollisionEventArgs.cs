using System;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class SolidCollisionEventArgs : EventArgs
    {
        public Collision2D Collision { get; private set; }
        public SolidCollisionEventArgs(Collision2D collision) 
        { 
            Collision = collision;
        }
    }
}
