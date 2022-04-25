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
            while(_lexem!="}")
            {
                WhosNext();
                GetLexem();
            }
            //if (!WhosNext())
            //    return;
            //GetLexem();
            //if (!EQ("}"))
            //    ErrorMessage("Программа должна заканчиваться символом }");
        }
        private bool WhosNext()
        {
            if (EQ("dim"))
            {
                if (!Opis())
                    return false;
            }
            else
            {
                if (!Oper())
                    return false;
            }
            return true;
        }
        private bool Opis()
        {
            if (EQ("dim"))
            {
                GetLexem();
                if (!Sid())
                    return false;
                if (!Type())
                    return false;
            }
            else
                ErrorMessage("Описание переменных должно начинаться c dim");
            return true;
        }
        private bool Oper()
        {
            return true;
        }
        private bool Sid()
        {
            if (!Id())
                return false;
            while (EQ(","))
            {
                GetLexem();
                if (!Id())
                    return false;
            }
            return true;
        }
        private bool Id()
        {
            if (IsID())
            {
                GetLexem();
                return true;
            }
            else
            {
                ErrorMessage("Не верный идентификатор");
                return false;
            }
        }
        private bool Type()
        {
            if (EQ("integer") || EQ("real") || EQ("boolean"))
            {
                GetLexem();
                if (EQ(";"))
                {
                    return true;
                }
                else
                {
                    ErrorMessage("Каждая строка должна заканчиваться символом ;");
                    return false;
                }
            }
            else
            {
                ErrorMessage("Не верный тип переменной");
                return false;
            }
        }
        private void GetLexem()
        {
            if (_index < _listLexAnaliz.Length)
            {
                int numberTable;
                int indexInTable;
                string lex = _listLexAnaliz[_index];
                try
                {
                    numberTable = int.Parse(lex[0].ToString());
                }
                catch(Exception e)
                {
                    ErrorMessage("Программа должна заканчиваться символом }");
                    return;
                }
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
        }
        private bool IsID()
        {
            for (int i = 1; i <= _tableInd.Count(); i++)
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
