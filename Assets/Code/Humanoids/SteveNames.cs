using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class SteveNames : MonoBehaviour
    {
        public List<String> ListPrefixes;
        private int nameNumber = 1;
        private int indexNr = 0;

        public String GetName()
        {
            if (indexNr < ListPrefixes.Count - 1)
            {
                indexNr++;
                return ListPrefixes[indexNr] + " Steve"; 
            }
            else
            {
                nameNumber++;
                return "Steve " + nameNumber;
            }
        }
    }
}
