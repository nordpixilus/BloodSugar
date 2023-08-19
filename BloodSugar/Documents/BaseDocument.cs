using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BloodSugar.Documents
{
    public class BaseDocument : FlowDocument
    {
        public BaseDocument()
        {
            this.ColumnWidth = PrintLayout.A4.ColumnWidth;
        }
    }
}
