using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.UI.Otros
{
    public class ComboItem
    {
        public object Codigo { get; set; }
        public string Valor { get; set; }

        public override string ToString()
        {
            return Valor ;
        }
    }
}
