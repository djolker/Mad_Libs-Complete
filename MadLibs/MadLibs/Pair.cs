using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadLibs
{
    class Pair
    {
        public string piece { get; private set; }
        public string gap { get; private set; }

        public Pair()
        {
            piece = "";
            gap = "";
        }

        /// <summary>
        /// Sets a string to piece in this Pair object
        /// </summary>
        /// <param name="p"></param>
        public void setPiece(string p)
        {
            this.piece = p;
        }
        
        /// <summary>
        /// Sets a string to gap in this Pair object
        /// </summary>
        /// <param name="g"></param>
        public void setGap(string g)
        {
            this.gap = g;
        }
    }
}
