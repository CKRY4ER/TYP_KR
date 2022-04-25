using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТЯП_Лекс_Анализ
{
    internal class SyntaxAnalizer
    {
        public delegate void Err(string errmsg);
        public event Err ErrorMessage;
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
            GetLexem();
            if (EQ("{"))
                GetLexem();
            else
            {
                ErrorMessage("Программа должна начинаться символом {");
                return;
            }

            if (!EQ("}"))
                ErrorMessage("Программа должна заканчиваться символом }");
        }
        private void GetLexem()
        {
            int numberTable;
            int indexInTable;
            string lex = _listLexAnaliz[_index];
            numberTable = int.Parse(lex[0].ToString());
            indexInTable = int.Parse(lex[2].ToString());
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
            for (int i = 0; i < _tableInd.Count(); i++)
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
        private bool EQ(string lex)
        {
            return lex == _lexem ? true : false;
        }
    }
}
