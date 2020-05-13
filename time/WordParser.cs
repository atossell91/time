using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace time
{
    class WordParser
    {
        private readonly string line;
        private readonly char separator;
        private int index = 0;

        public WordParser(string text, char sep)
        {
            line = text;
            separator = sep;
        }
        private bool isSeparator(char c)
        {
            return c == separator;
        }
        public string NextWord()
        {
            while(isSeparator(line[index]) && index < line.Length)
            {
                ++index;
            }
            string output = "";
            while(index < line.Length && !isSeparator(line[index]))
            {
                output += line[index];
                ++index;
            }

            if (output.Length > 0)
            {
                return output;
            }
            else
            {
                return null;
            }
            /*
            if (index == line.Length)
            {
                return null;
            }
            else
            {
                return output;
            }*/
        }
        public void ResetIndex()
        {
            index = 0;
        }
    }
}
