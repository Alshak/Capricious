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
        public int nameNumber = 1;
        public int indexNr = 0;

        public String GetName()
        {
            Debug.Log("Get a name!");
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
