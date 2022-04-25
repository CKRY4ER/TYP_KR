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
            while(_lexem!="}" && WhosNext())
            {
                GetLexem();
            }
            if (_lexem!="}")
            {
                return;
                
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
        private bool Oper()
        {
            if (EQ("begin"))
            {
                if (!Sostov())
                    return false;
                return true;
            }
            else if (IsID())
            {
                if (!Prisvaiv())
                    return false;
                return true;
            }
            //else if (EQ("if"))
            //{
            //    if (!Uslovn())
            //        return false;
            //    return true;
            //}
            //else if (EQ("for"))
            //{
            //    if (!FiksirCikl())
            //        return false;
            //    return true;
            //}
            //else if (EQ("while"))
            //{
            //    if (!UslovCikl())
            //        return false;
            //    return true;
            //}
            //else if (EQ("writeln"))
            //{
            //    if (!Vivod())
            //        return false;
            //    return true;
            //}
            //else if (EQ("readln"))
            //{
            //    if (!Vvod())
            //        return false;
            //    return true;
            //}
            else
            {
                ErrorMessage("Не понятно ниче!!!");
                return false;
            }
        }
        private bool Sostov()
        {
            GetLexem();
            if (!Opers())
            {
                ErrorMessage("Ошибка в составлении составного оператора");
                return false;
            }
            
            if (EQ("end"))
                return true;
            ErrorMessage("Составной оператор должен заканчиваться: end");
            return false;
        }
        private bool Opers()
        {
            if (!Oper())
            {
                ErrorMessage("Ошибка в составлении операторов внутри составного оператора");
                return false;
            }
            while (EQ(";"))
            {
                GetLexem();
                if (Oper())
                {
                    ErrorMessage("Ошибка в составлении операторов внутри составного оператора");
                    return false;
                }
            }
            return true;
        }
        private bool Prisvaiv()
        {
            GetLexem();
            if (!EQ(":="))
            {
                ErrorMessage("Не верный синтаксис оператора присваивания: :=");
                return false;
            }
            if (!Viraj())
            { 
                return false;
            }
            //GetLexem();
            if (EQ(";"))
            {
                return true;
            }
            else
            {
                ErrorMessage("Каждая строка должна заканчиваться символом: ;");
                return false;
            }
        }
        private bool Viraj()
        {
            GetLexem();
            if (!Operand())
                return false;
            if (OperGrupOtn())
            {
                GetLexem();
                if (!Viraj())
                    return false;
            }
            return true;
        }
        private bool OperGrupOtn()
        {
            if (_lexem == "!=" || _lexem == "==" || _lexem == "<" || _lexem == "<=" || _lexem == ">" || _lexem == ">=")
                return true;
            return false;
        }
        private bool Operand()
        {
            if (!Slagaem())
                return false;
            if (OperGrupSloj())
            {
                GetLexem();
                if (!Operand())
                    return false;
            }
            return true;
        }
        private bool OperGrupSloj()
        {
            if (_lexem == "+" || _lexem == "-" || _lexem == "||")
                return true;
            return false;
        }
        private bool Slagaem()
        {
            if (!Mnoj())
                return false;
            if (OperGrupMnoj())
            {
                GetLexem();
                if (!Slagaem())
                    return false;
            }
            return true;
        }
        private bool OperGrupMnoj()
        {
            if (_lexem == "*" || _lexem == "/" || _lexem == "&&")
                return true;
            return false;
        }
        private bool Mnoj()
        {
            if (IsID() || IsDigit() || _lexem == "true" || _lexem == "false") 
            {
                GetLexem();
                return true;
            }
            if (YnarOper())
            {
                GetLexem();
                if (!Mnoj())
                    return false;
            }
            else if (_lexem == "(") 
            {
                if (!Viraj())
                    return false;
                if (EQ(")"))
                {
                    GetLexem();
                    return true;
                }
                else
                {
                    ErrorMessage("Ошибка в построении выражения: пропущена закрывающая скобка");
                    return false;
                }
                
            }
            else
            {
                ErrorMessage("Ошибка в построении выражения");
                return false;
            }
            return false;
        }
        private bool YnarOper()
        {
            if (_lexem == "!")
                return true;
            return false;
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
                if (lex.Length > 3)
                {
                    string numb = lex[2].ToString() + lex[3].ToString();
                    indexInTable = int.Parse(numb);
                }
                else
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
