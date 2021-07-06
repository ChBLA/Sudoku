using System.Collections.Generic;

namespace Sudoku.WebLoader
{
    internal class EntryDef
    {
        public string response { get; set; }
        public string size { get; set; }
        public List<CellDef> squares { get; set; }
    }
}