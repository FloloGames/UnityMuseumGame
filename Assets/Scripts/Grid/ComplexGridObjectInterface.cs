using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [Serializable]
    /// <summary>
    /// Interface for <see cref="GridObject"/> which are ComplexGridObjects
    /// also saves important information 
    /// </summary>
    public abstract class ComplexGridObjectInterface
    {
        public abstract void OnItemSet(int i, int j, GridObject complexObject);
    }
}