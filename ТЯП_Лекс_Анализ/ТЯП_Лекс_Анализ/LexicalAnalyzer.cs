using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ТЯП_Лекс_Анализ
{
    enum Tables
    {
        TW = 1, //таблица служебных слов
        TL, //таблица ограничителей
        TN, //Таблица чисел
        TI, //таблица идентификаторов
    }
    enum States
    {
        ER,//Ошибка
        V, //Выход
        H, //Начало
        I, //Идентификатор
        C1, C2, C3, //Комментарий
        N2, //Двоичное число
        N8, //Восьмеричное число
        N10, //Десятичное число
        N16, //Шeстнадцатеричное число
        B, //B или b
        O, //O или о
        D, //D или d
        HX, //H или h
        E11, //E или е
        E12, E13, E21, E22, //порядок числа 
        ZN, //знак порядка
        P1, //точка
        P2, //дробная часть
        OT, //отрицание ! !=
        PR, //присваивание :=
        RV, //равенство ==
        M1, //> >=
        M2, //< <=
        OG, //остальные ограничители
        NA,
        AND, //||
        OR, // &&
    }
    class LexicalAnalyzer
    {
        public delegate void NewEntryTable(string s);
        public event NewEntryTable NewTI;
        public event NewEntryTable NewTN;
        public event NewEntryTable OutResult;
        char _CH;
        Dictionary<int, string> tableRW;
        Dictionary<int, string> tableInd;
        Dictionary<int, string> tableLimiter;
        public LexicalAnalyzer(Dictionary<int, string> tablerw, Dictionary<int, string> tablelimiter)
        {
            tableRW = tablerw;
            tableLimiter = tablelimiter;
            tableInd = new Dictionary<int, string>();
        }
        public string ProgramText
        {
            get;
            set;
        }
        string _bufferS = "";
        int _chNumber = 0;
        States CS;
        public bool Scaner()
        {
            if (ProgramText[ProgramText.Length - 1] != '}')
                return false;
            tableInd.Clear();
            CS = States.H;
            _chNumber = 0;
            GetChar();
            while (CS != States.ER)
            {
                switch (CS)
                {
                    case (States.H):
                        if (_CH == ' ' || _CH == '\n' || _CH == '\r')
                            GetChar();
                        else if (_CH == '}')
                        {
                            Nill();
                            AddInBuf();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.V;
                        }
                        else if (Letter())
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.I;
                        }
                        else if (_CH == '1' || _CH == '0')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.N2;
                        }
                        else if (_CH >= '2' && _CH <= '7')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.N8;
                        }
                        else if (_CH == '8' || _CH == '9')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.N10;
                        }
                        else if (_CH == '/')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.C1;
                        }
                        else if (_CH == '!')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.OT;
                        }
                        else if (_CH == ':')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.PR;
                        }
                        else if (_CH == '=')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.RV;
                        }
                        else if (_CH == '>')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.M1;
                        }
                        else if (_CH == '<')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.M2;
                        }
                        else if (_CH == '|')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.OR;
                        }
                        else if (_CH == '&')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.AND;
                        }
                        else if (_CH != '^')
                        {
                            Nill();
                            AddInBuf();
                            CS = States.OG;
                        }
                        break;
                    case (States.I):
                        if (Letter())
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.I;
                        }
                        else if (Look(Tables.TW) != 0)
                        {
                            Out(Tables.TW, Look(Tables.TW));
                            CS = States.H;
                        }
                        else if (Look(Tables.TI) != 0)
                        {
                            CS = States.H;
                        }
                        else
                        {
                            NewTI(_bufferS);
                            int a = tableInd.Count + 1;
                            tableInd.Add(a, _bufferS);
                            CS = States.H;
                        }
                        break;
                    case (States.N2):
                        if (_CH == '0' || _CH == '1')
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (_CH == 'B' || _CH == 'b')
                        {
                            GetChar();
                            CS = States.B;
                        }
                        else if (_CH == 'O' || _CH == 'o')
                        {
                            GetChar();
                            CS = States.O;
                        }
                        else if (_CH == 'D' || _CH == 'd')
                        {
                            GetChar();
                            CS = States.D;
                        }
                        else if (_CH >= '2' && _CH <= '7')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N8;
                        }
                        else if (_CH == '8' || _CH == '9')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N10;
                        }
                        else if (Letter() && !AFH() && _CH != 'O' && _CH != 'o')
                        {
                            CS = States.ER;
                        }
                        else if (_CH == 'A' || _CH == 'C' || _CH == 'a' || _CH == 'c' || _CH == 'F' || _CH == 'f')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N16;
                        }
                        else if (_CH == 'e' || _CH == 'E')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E11;
                        }
                        else if (_CH == '.')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.P1;
                        }
                        else
                        {
                            CS = States.N10;
                        }
                        break;
                    case (States.B):
                        if (Letter() && !AFH())
                        {
                            CS = States.ER;
                        }
                        else if (_CH == 'H' || _CH == 'h')
                        {
                            _bufferS += "B";
                            GetChar();
                            CS = States.HX;
                        }
                        else if (CheckHex(_CH))
                        {
                            _bufferS += "B";
                            AddInBuf();
                            GetChar();
                            CS = States.N16;
                        }
                        else
                        {
                            Translate(2);
                            CS = States.H;
                        }
                        break;
                    case (States.N8):
                        if (_CH >= '0' && _CH <= '7')
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (_CH == 'O' || _CH == 'o')
                        {
                            GetChar();
                            CS = States.O;
                        }
                        else if (_CH == 'D' || _CH == 'd')
                        {
                            GetChar();
                            CS = States.D;
                        }
                        else if (Letter() && !AFH() && _CH != 'O' && _CH != 'o')
                        {
                            CS = States.ER;
                        }
                        else if (_CH == '8' || _CH == '9')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N10;
                        }
                        else if (_CH == 'H' || _CH == 'h')
                        {
                            GetChar();
                            CS = States.HX;
                        }
                        else if (_CH >= 'A' && _CH <= 'C' || _CH == 'F' || _CH == 'f')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N16;
                        }
                        else if (_CH == 'e' || _CH == 'E')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E11;
                        }
                        else if (_CH == '.')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.P1;
                        }
                        else
                        {

                            CS = States.N10;
                        }
                        break;
                    case (States.O):
                        if (Letter() || Digit())
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Translate(8);
                            CS = States.H;
                        }
                        break;
                    case (States.N10):
                        if (_CH >= '0' && _CH <= '9')
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (Letter() && !AFH())
                        {
                            CS = States.ER;
                        }
                        else if (_CH == 'D' || _CH == 'd')
                        {
                            GetChar();
                            CS = States.D;
                        }
                        else if (_CH == 'H' || _CH == 'h')
                        {
                            GetChar();
                            CS = States.HX;
                        }
                        else if (_CH >= 'A' && _CH <= 'C' || _CH == 'F' || _CH == 'f' || _CH >= 'a' && _CH <= 'c')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N16;
                        }
                        else if (_CH == 'E' || _CH == 'e')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E11;
                        }
                        else if (_CH == '.')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.P1;
                        }
                        else
                        {
                            Translate(10);
                            CS = States.H;
                        }
                        break;
                    case (States.D):
                        if (_CH == 'H' || _CH == 'h')
                        {
                            _bufferS += "D";
                            GetChar();
                            CS = States.HX;
                        }
                        else if (CheckHex(_CH) || Digit())
                        {
                            _bufferS += "D";
                            GetChar();
                            CS = States.N16;
                        }
                        else if (Letter() && !AFH())
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Translate(10);
                            CS = States.H;
                        }
                        break;
                    case (States.N16):
                        if (Digit() || CheckHex(_CH))
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (_CH == 'h' || _CH == 'H')
                        {
                            GetChar();
                            CS = States.HX;
                        }
                        else if (/*Letter() && */!AFH() || !Digit())
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.HX):
                        if (Letter() || Digit())
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Translate(16);
                            CS = States.H;
                        }
                        break;
                    case (States.E11):
                        if (CheckHex(_CH))
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N16;
                        }
                        else if (_CH == 'H' || _CH == 'h')
                        {
                            GetChar();
                            CS = States.HX;
                        }
                        else if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E12;
                        }
                        else if (_CH == '-' || _CH == '+')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.ZN;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.E12):
                        if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (CheckHex(_CH))
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.N16;
                        }
                        else if (Letter())
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Conv();
                            CS = States.H;
                        }
                        break;
                    case (States.ZN):
                        if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E13;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.E13):
                        if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (Letter() || _CH == '.')
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Conv();
                            CS = States.H;
                        }
                        break;
                    case (States.P1):
                        if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.P2;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.P2):
                        if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (_CH == 'E' || _CH == 'e')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E21;
                        }
                        else if (Letter() || _CH == '.')
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Conv();
                            CS = States.H;
                        }
                        break;
                    case (States.E21):
                        if (_CH == '+' || _CH == '-')
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.ZN;
                        }
                        else if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                            CS = States.E22;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.E22):
                        if (Digit())
                        {
                            AddInBuf();
                            GetChar();
                        }
                        else if (Letter() || _CH == '.')
                        {
                            CS = States.ER;
                        }
                        else
                        {
                            Conv();
                            CS = States.H;
                        }
                        break;
                    case (States.C1):
                        if (_CH == '*')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.C2;
                        }
                        else
                        {
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        break;
                    case (States.C2):
                        if (_CH == '}')
                        {
                            CS = States.ER;
                        }
                        else if (_CH == '*')
                        {
                            Nill();
                            AddInBuf();
                            GetChar();
                            CS = States.C3;
                        }
                        else
                        {
                            GetChar();
                        }
                        break;
                    case (States.C3):
                        if (_CH == '/')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.C2;
                        }
                        break;
                    case (States.OT):
                        if (_CH == '=')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else if (Letter())
                        {
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.PR):
                        if (_CH == '=')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.RV):
                        if (_CH == '=')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.M1):
                        if (_CH == '=')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        break;
                    case (States.M2):
                        if (_CH == '=')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        break;
                    case (States.OG):
                        if (Look(Tables.TL) != 0)
                        {
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.OR):
                        if (_CH == '|')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.AND):
                        if (_CH == '&')
                        {
                            AddInBuf();
                            GetChar();
                            Out(Tables.TL, Look(Tables.TL));
                            CS = States.H;
                        }
                        else
                        {
                            CS = States.ER;
                        }
                        break;
                    case (States.V):
                        return true;

                }
            }
            return false;
        }
        public void GetChar()
        {
            if (_chNumber < ProgramText.Length)
            {
                _CH = ProgramText[_chNumber];
                _chNumber++;
            }
        }
        public void AddInBuf() =>
            _bufferS += _CH.ToString();
        public bool Letter() =>
            char.IsLetter(_CH);
        public bool Digit() =>
            char.IsDigit(_CH);

        public void Nill() =>
            _bufferS = "";
        public bool CheckHex(char ch)
        {
            if ((ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f'))
                return true;
            else
                return false;
        }
        public bool AFH()
        {
            if ((_CH >= 'A' && _CH <= 'F' || _CH == 'H') || (_CH >= 'a' && _CH <= 'f' || _CH == 'h'))
                return true;
            else
                return false;
        }
        public int Look(Tables tabl)
        {
            int z = 0;
            switch (tabl)
            {
                case (Tables.TW):
                    for (int i = 1; i <= tableRW.Count; i++)
                    {
                        if (tableRW[i] == _bufferS)
                            z = i;
                    }
                    break;
                case (Tables.TL):
                    for (int i = 1; i <= tableLimiter.Count; i++)
                    {
                        if (tableLimiter[i] == _bufferS)
                            z = i;
                    }
                    break;
                case (Tables.TI):
                    for (int i = 1; i <= tableInd.Count; i++)
                    {
                        if (tableInd[i] == _bufferS)
                            z = i;
                    }
                    break;
            }
            return z;
        }
        public void Translate(short bs)
        {
            double number = 0;
            switch (bs)
            {
                case (2):
                    for (int i = _bufferS.Length - 1; i >= 0; i--)
                    {
                        if (_bufferS[i] == '1')
                            number += (int)Math.Pow(2, _bufferS.Length - i - 1);
                    }
                    break;
                case (8):
                    for (int i = _bufferS.Length - 1; i >= 0; i--)
                    {
                        int digit = int.Parse(_bufferS[i].ToString());
                        number = number + digit * Math.Pow(8, _bufferS.Length - i - 1);
                    }
                    break;
                case (16):
                    for (int i = _bufferS.Length - 1; i >= 0; i--)
                    {
                        int digit;
                        if (CheckHex(_bufferS[i]))
                        {
                            digit = GetDecimalFromHex(_bufferS[i]);
                        }
                        else
                        {
                            digit = int.Parse(_bufferS[i].ToString());
                        }
                        number = number + digit * Math.Pow(16, _bufferS.Length - 1 - i);
                    }
                    break;
                case (10):
                    number = double.Parse(_bufferS);
                    break;
            }
            NewTN(Convert.ToString(number));
        }
        public void Out(Tables tabl, int z)
        {
            string outparam = $"({((int)tabl)},{z})";
            OutResult(outparam);
        }
        public void Conv()
        {
            double number;
            string sNumber = "";
            string sNumberAfterExpOrSign = "";
            char sign = '+';
            int i;
            for (i = 0; i < _bufferS.Length; i++)
            {
                if (_bufferS[i] != 'e' && _bufferS[i] != 'E')
                {
                    if (_bufferS[i] != '.')
                    {
                        sNumber += _bufferS[i].ToString();
                    }
                    else
                    {
                        sNumber += ',';
                    }
                }
                else
                {
                    break;
                }
            }
            i++;
            for (int j = i; j < _bufferS.Length; j++)
            {
                if (_bufferS[j] == '+' || _bufferS[j] == '-')
                {
                    sign = _bufferS[j];
                }
                else
                {
                    sNumberAfterExpOrSign += _bufferS[j].ToString();
                }
            }
            if (sNumberAfterExpOrSign == "")
            {
                NewTN(sNumber);
                return;
            }
            number = double.Parse(sNumber);
            if (sign == '+')
            {
                number = number * Math.Pow(10, Convert.ToDouble(sNumberAfterExpOrSign));
            }
            else
            {
                number = number * Math.Pow(10, 0 - Convert.ToDouble(sNumberAfterExpOrSign));
            }
            NewTN(Convert.ToString(number));
        }
        private int GetDecimalFromHex(char ch)
        {
            int numb = 0;
            switch (ch)
            {
                case ('A'):
                case ('a'):
                    numb = 10;
                    break;
                case ('B'):
                case ('b'):
                    numb = 11;
                    break;
                case ('C'):
                case ('c'):
                    numb = 12;
                    break;
                case ('D'):
                case ('d'):
                    numb = 13;
                    break;
                case ('E'):
                case ('e'):
                    numb = 14;
                    break;
                case ('F'):
                case ('f'):
                    numb = 15;
                    break;
            }
            return numb;
        }

    }
}
