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
        private Dictionary<int, int> _tableNumber;
        private string[] _listLexAnaliz;
        private int _index = 0;
        public SyntaxAnalizer(string[] list, Dictionary<int, string> tableRW, Dictionary<int, string> tableInd, Dictionary<int, string> tableLimiter, Dictionary<int, int> tableNumber)
        {
            _tableInd = tableInd;
            _tableLimiter = tableLimiter;
            _tableRW = tableRW;
            _listLexAnaliz = list;
            _tableNumber = tableNumber;
        }
        public void Pr()
        {
            GetLexem();
            if (!EQ("{"))
            {
                throw new Exception("Программа должна начинаться символом: {");
            }
            GetLexem();
            while (!EQ("}") && WhosNext())
            {
                GetLexem();
            }
            if (!EQ("}"))
                throw new Exception("Программа должна заканчиваться символом: }");
        }
        private bool WhosNext()
        {
            //GetLexem();
            if (EQ("dim"))
            {
                Opis();
                return true;
            }
            Oper();
            return true;
        }
        private void Opis()
        {
            if (EQ("dim"))
            {
                Sid();
                Type();
            }
            else
                throw new Exception("Ошибка в составлении описания");
        }
        private void Sid()
        {
            GetLexem();
            Id();
            while (EQ(","))
            {
                GetLexem();
                Id();
            }
        }
        private void Id()
        {
            if (!IsID())
                throw new Exception("В описании должен быть идентификатор или список идентификаторов");
            GetLexem();
        }
        private void Type()
        {
            if (EQ("integer") || EQ("real") || EQ("boolean"))
            {
                GetLexem();
                if (!EQ(";"))
                    throw new Exception("Пропущен символ признака конца строки: ;");
            }
            else
                throw new Exception("Не верный тип данных. Доступные типы данных: integer, real, boolean");
        }
        private void Oper()
        {
            if (EQ("begin"))
                Sostav();
            else if (IsID())
            {
                Prisvaiv();
                if (!EQ(";"))
                    throw new Exception("Оператор присваивания вне цикла for должен оканчиваться символом: ;");
            }
            else if (EQ("if"))
            {
                Yslov();
            }
            else if (EQ("for"))
            {
                FiksCikla();
            }
            else if (EQ("while"))
            {
                YslovCikla();
            }
            else
            {
                throw new Exception("Ошибка в построении оператора (неизвестный оператор)");
            }
        }
        private void Sostav()
        {
            GetLexem();
            Opers();
            if (!EQ("end"))
                throw new Exception("Составной опреатор не закрыт. Пропущенно ключевое слово: end");
        }
        private void Yslov()
        {
            GetLexem();
            if (!EQ("("))
                throw new Exception("Ошибка в составлении выражения внутри оператора if: выражение должно заключаться в скобки");
            Viraj();
            if (!EQ(")"))
                throw new Exception("Ошибка в составлении выражения внутри оператора if: выражение должно заключаться в скобки");
            GetLexem();
            Oper();
            GetLexem();
            if (EQ("else")) // Опастное место
            {
                GetLexem();
                Oper();
            }
        }
        private void FiksCikla()
        {
            GetLexem();
            Prisvaiv();
            if (!EQ("to"))
                throw new Exception("Ошибка в построении оператороа фиксированного цикла.");
            Viraj();
            if (EQ("step"))
            {
                Viraj();
            }
            if (!EQ("next"))
                throw new Exception("Ошибка в построении оператороа фиксированного цикла.");
            GetLexem();
            Oper();
            
        }
        private void YslovCikla()
        {
            GetLexem();
            if (!EQ("("))
                throw new Exception("Ошибка в составлении выражения внутри оператора while: выражение должно заключаться в скобки");
            Viraj();
            if (!EQ(")"))
                throw new Exception("Ошибка в составлении выражения внутри оператора while: выражение должно заключаться в скобки");
            GetLexem();
            Oper();
        }
        private void Opers()
        {
            Oper();
            while (EQ(";"))
            {
                GetLexem();
                if (!EQ("end") && !EQ("else"))
                    Opers();
            }
        }
        private void Prisvaiv()
        {
            if (!IsID())
                throw new Exception("Ошибка в построении оператора присваивания");
            GetLexem();
            if (EQ(":="))
            {
                Viraj();
            }
            else
                throw new Exception("Ошибка оператора присваивания. Ожидалось: :=");
        }
        private void Viraj()
        {
            GetLexem();
            Soperand();  
        }
        private void Soperand()
        {
            Operand();
            while (OperGroupOtn())
            {
                GetLexem();
                Soperand();
            }
        }
        private void Operand()
        {
            Slagaemoe();
            if (OperGroupSloj())
            {
                GetLexem();
                Operand();
            }
        }
        private void Slagaemoe()
        {
            Mnoj();
            if (OperGroupMnoj())
            {
                GetLexem();
                Slagaemoe();
            }
        }
        private void Mnoj()
        {
            if (IsID() || IsDigit() || EQ("true") || EQ("false"))
            {
                GetLexem();
            }
            else if (YnarOper())
            {
                GetLexem();
                Mnoj();
            }
            else if (EQ("("))
            {
                Viraj();
                if (!EQ(")"))
                    throw new Exception("Ошибка в построении выражения: пропущена закрывающая скобка");
                GetLexem();
            }
            else
                throw new Exception("Ошибка в построении выражения");
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
                        _lexem = _tableNumber[indexInTable].ToString();
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
        private bool OperGroupOtn()
        {
            if (EQ("!=") || EQ("==") || EQ("<") || EQ("<=") || EQ(">") || EQ(">="))
                return true;
            return false;
        }
        private bool OperGroupSloj()
        {
            if (EQ("+") || EQ("-") || EQ("||"))
                return true;
            return false;
        }
        private bool OperGroupMnoj()
        {
            if (EQ("*") || EQ("/") || EQ("&&"))
                return true;
            return false;
        }
        private bool YnarOper()
        {
            if (EQ("!"))
                return true;
            return false;
        }
    }
}
