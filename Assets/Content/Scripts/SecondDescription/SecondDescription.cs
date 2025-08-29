using System;
using System.Collections.Generic;

namespace Content.Scripts.SecondDescription
{
    [Serializable]
    public class SecondDescription
    {
        public int SInt = 9;
        public bool SBool = true;
        public char SChar = 'a';
        public float SFloat;
        public string SString = "Todo";
        public List<Person> ListPreson;
    }

    [Serializable]
    public class Person
    {
        public string Name;
        public int Age;
        public bool IsAlive;
    }
}