using System;
using System.Collections.Generic;

namespace Content.Scripts.FourthDescription
{
    [Serializable]
    public class FourthDescription
    {
        public List<ElementOne> Element;
    }

    [Serializable]
    public class ElementOne
    {
        public List<ElementTwo> ElementOnes;
    }

    [Serializable]
    public class ElementTwo
    {
        public string ETString;
        public int ETInt;
    }
}