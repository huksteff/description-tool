using UnityEngine;

namespace Content.Scripts
{
    public abstract class BaseDescription<T> : ScriptableObject
    {
        public T Description;
    }
}