﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.Humanoids
{
    public class SteveNames : MonoBehaviour
    {
        public List<String> ListPrefixes;
        public static int nameNumber = 1;
        public static int indexNr = 0;

        public String GetNextName()
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

        public String GetCurrentName()
        {
            if (indexNr < ListPrefixes.Count - 1)
            {
                return ListPrefixes[indexNr] + " Steve";
            }
            else
            {
                return "Steve " + nameNumber;
            }
        }
    }
}
