using System;

namespace Content.Scripts.EnemyDescription
{
    [Serializable]
    public class EnemyDescription
    {
        public string Name = "";
        public int Level;
        public float Damage = 0.5f;
        public float Mana = 100.0f;
        public float Stamina = 100.0f;
        public int ExpDrop = 10;
    }
}