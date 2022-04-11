using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТЯП_Лекс_Анализ
{
    class SyntaxAnalizer
    {
        private string _lexem;
        private Dictionary<int, string> _tableRW;
        private Dictionary<int, string> _tableInd;
        private Dictionary<int, string> _tableLimiter;
        private string[] _listLexAnaliz;
        private int _index = 0;
        public SyntaxAnalizer(string[] list, Dictionary<int, string> tableRW, Dictionary<int, string> tableInd, Dictionary<int, string> tableLimiter)
        {
            _tableInd = tableInd;
            _tableLimiter = tableLimiter;
            _tableRW = tableRW;
            _listLexAnaliz = list;
        }
        public void Pr()
        {

        }
        private void GetLexem()
        {
            int numberTable;
            int indexInTable;
            string lex = _listLexAnaliz[_index];
            numberTable = Convert.ToInt32(lex[0]);
            indexInTable = Convert.ToInt32(lex[2]);
            switch (numberTable)
            {
                case (1):
                    _lexem = _tableRW[indexInTable];
                    break;
                case (2):
                    _lexem = _tableLimiter[indexInTable];
                    break;
                case (3):
                    _lexem = "2";
                    break;
                case (4):
                    _lexem = _tableInd[indexInTable];
                    break;
            }
            _index++;
        }
        private bool IsID()
        {
            for(int i = 0; i < _tableInd.Count(); i++)
            {
                if (_lexem == _tableInd[i])
                    return true;
            }
            return false;
        }
        private bool IsDigit()
        {
            return char.IsDigit(_lexem[0]);
        }
    }
}
