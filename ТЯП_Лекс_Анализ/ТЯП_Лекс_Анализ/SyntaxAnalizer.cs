using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
		private DataGridView _tableIndFor4;
		private Dictionary<int, string> _tableInd;
        private Dictionary<int, string> _tableLimiter;
        private Dictionary<int, double> _tableNumber;
		private int[] _lexem2 = new int[2];
		private Stack<int> ints = new Stack<int>();
		private Stack<string> typesAndOpers = new Stack<string>();
        private string[] _listLexAnaliz;
        private int _index = 0;
		private DataGridView _tableOpers;

		public SyntaxAnalizer(string[] list,
			Dictionary<int, string> tableRW,
			Dictionary<int, string> tableInd,
			Dictionary<int, string> tableLimiter,
			Dictionary<int, double> tableNumber,
			DataGridView dataGridView,
			DataGridView dataGridView1)
        {
			_tableIndFor4 = dataGridView;
			_tableInd = tableInd;
            _tableLimiter = tableLimiter;
            _tableRW = tableRW;
            _listLexAnaliz = list;
            _tableNumber = tableNumber;
			_tableOpers = dataGridView1;
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
                GetLexem();
                if (!EQ(";"))
                    throw new Exception("Пропущен символ ;");
                return true;
            }
            Oper();
			if (!EQ(";"))
				throw new Exception("Пропущен символ ;");
            
            return true;
        }

		private void Opis() 
		{
			ints.Clear();
			if (EQ("dim"))
			{
				Sid();
				Type();
			}
			else
				throw new Exception("Ошибка в составлении оператора");
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
				throw new Exception("Ошибка в списке идентификаторов");
			Insts(_lexem2[1]);
			GetLexem();
		}

		private void Type()
		{
			if (!EQ("integer") && !EQ("real") && !EQ("boolean"))
			{
				throw new Exception("Не верный тип данных. Доступные типы данных: integer, real, boolean");
			}
			Dec(_lexem);
		}

		private void Oper()
		{
			if (EQ("begin"))
			{
				Sostav();
			}
			else if (IsID())
			{
				Prisvaiv();
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
			else if (EQ("readln"))
			{
				Vvod();
			}
			else if (EQ("writeln")) 
			{
				Vivod();
			}
			else
			{
				throw new Exception("Ошибка в построении оператора (неизвестный оператор) или лишний символ ;");
			}
		}

		private void Sostav()
		{
			GetLexem();
			Opers();
			if (!EQ("end"))
				throw new Exception("Составной опреатор не закрыт. Пропущенно ключевое слово: end");
			GetLexem();
		}

		private void Opers()
		{
			Oper();
			while (EQ(";"))
			{
				GetLexem();
				Opers();
			}
		}

		private void Prisvaiv()
		{
			if (!IsID())
				throw new Exception("Ошибка в построении оператора присваивания");
			CheckId();
			string type = GetTypePerem();
			GetLexem();
			if (EQ(":="))
			{
				typesAndOpers.Clear();
				Viraj();
			}
			else
				throw new Exception("Ошибка оператора присваивания. Ожидалось: :=");
			CheckOp();
			string resultType = typesAndOpers.Pop();
			if (type=="real" && resultType == "integer")
            {
				return;
            }
			else if (type != resultType)
            {
				throw new Exception($"Новозможно записать тип {resultType} в тип {type}");
            }
		}

		private void Yslov()
		{
			typesAndOpers.Clear();
			GetLexem();
			if (!EQ("("))
				throw new Exception("Ошибка в составлении выражения внутри оператора if: выражение должно заключаться в скобки");
			typesAndOpers.Clear();
			Viraj();
			CheckOp();
			string resultType = typesAndOpers.Pop();
			if (resultType != "boolean")
            {
				throw new Exception("Выражение внутри условия должно иметь тип boolean");
            }
			if (!EQ(")"))
				throw new Exception("Ошибка в составлении выражения внутри оператора if: выражение должно заключаться в скобки");
			GetLexem();
			Oper();
			if (EQ("else")) // Опастное место
			{
				typesAndOpers.Clear();
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
			typesAndOpers.Clear();
			Viraj();
			if (EQ("step"))
			{
				Viraj();
			}
			Oper();
			GetLexem();
			if (!EQ("next"))
				throw new Exception("Ошибка в построении оператороа фиксированного цикла.");
			GetLexem();
		}

		private void YslovCikla()
		{
			GetLexem();
			if (!EQ("("))
				throw new Exception("Ошибка в составлении выражения внутри оператора while: выражение должно заключаться в скобки");
			typesAndOpers.Clear();
			Viraj();
			CheckOp();
			string resultType = typesAndOpers.Pop();
			if (resultType != "boolean")
			{
				throw new Exception("Выражение внутри условного цикла должно иметь тип boolean");
			}
			if (!EQ(")"))
				throw new Exception("Ошибка в составлении выражения внутри оператора while: выражение должно заключаться в скобки");
			GetLexem();
			Oper();
		}

		private void Vvod()
		{
			Sid();
		}

		private void Vivod()
		{
			Sviraj();
		}
		private void Sviraj()
		{
			typesAndOpers.Clear();
			Viraj();
			CheckOp();
			while (EQ(","))
			{
				Sviraj();
			}
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
			if (IsID())
            {
				CheckId();
				//AddTypeInStack();
				GetLexem();
			}
			else if(IsDigit())
			{ 
                try
                {
					int x = int.Parse(_lexem);
					typesAndOpers.Push("integer");

                }
                catch (Exception e)
                {
					typesAndOpers.Push("real");
                }
				GetLexem();
			}
			else if (EQ("true") || EQ("false"))
			{ 
				GetLexem();
				typesAndOpers.Push("boolean");
            }
			else if (YnarOper())
			{
				GetLexem();
				Mnoj();
				//Доработать данное место
				CheckNot();
			}
			else if (EQ("("))
			{
				Viraj();
				if (!EQ(")"))
					throw new Exception("Ошибка в построении выражения: пропущена закрывающая скобка");
				CheckOp();
				GetLexem();
			}
			else
				throw new Exception("Ошибка в построении выражения");
		}

		private bool OperGroupOtn()
		{
			if (EQ("!=") || EQ("==") || EQ("<") || EQ("<=") || EQ(">") || EQ(">="))
			{
				typesAndOpers.Push(_lexem);
				return true;
			}
			return false;
		}

		private bool OperGroupSloj()
		{
			if (EQ("+") || EQ("-") || EQ("||"))
			{
				typesAndOpers.Push(_lexem);
				return true;
			}
			return false;
		}

		private bool OperGroupMnoj()
		{
			if (EQ("*") || EQ("/") || EQ("&&"))
			{
				typesAndOpers.Push(_lexem);
				return true;
			}
			return false;
		}

		private bool YnarOper()
		{
			if (EQ("!"))
				return true;
			return false;
		}



		#region

		private void AddTypeInStack()
        {
			typesAndOpers.Push(_tableIndFor4.Rows[_lexem2[1] - 1].Cells[3].Value.ToString());
        }

		private void CheckNot()
        {
			string type =  typesAndOpers.Pop();
			if (type != "boolean")
            {
				throw new Exception("Операцию отрицания можно применять только к булевскому типу.");
            }
            typesAndOpers.Push(type);
        }

		private string FindMatchByOper(string oper, string type1, string type2)
		{
			string result = null;

			for (int i = 0; i < _tableOpers.Rows.Count; i++) 
			{
				if (_tableOpers.Rows[i].Cells[0].Value.ToString() == oper &&
					_tableOpers.Rows[i].Cells[1].Value.ToString() == type1 &&
					_tableOpers.Rows[i].Cells[2].Value.ToString() == type2)
                {
					result = _tableOpers.Rows[i].Cells[3].Value.ToString();
                }
			}
			return result;
        }

		private void CheckOp()
        {
			do
			{
				if (typesAndOpers.Count == 1 || typesAndOpers.Count == 2)
                {
					return;
                }
				string type2 = typesAndOpers.Pop();
				string oper = typesAndOpers.Pop();
				string type1 = typesAndOpers.Pop();

				string resultType = FindMatchByOper(oper, type1, type2);
				if (resultType != null)
				{
					typesAndOpers.Push(resultType);
				}
				else
				{
					throw new Exception($"Ошибка! Операцию {oper} нельзя применять к {type1} {type2}");
				}
			}
			while (typesAndOpers.Count != 1);
		}

		private void CheckId()
        {
			if (_tableIndFor4.Rows[_lexem2[1] - 1].Cells[2].Value.ToString() != "0")
			{
				typesAndOpers.Push(_tableIndFor4.Rows[_lexem2[1]-1].Cells[3].Value.ToString());
			}
			else
				throw new Exception($"Ошибка! Идентификатор {_tableIndFor4.Rows[_lexem2[1]-1].Cells[1].Value} не инициализирован");
		}

		private void Decid(int i, string type)
        {
			if (_tableIndFor4.Rows[i-1].Cells[2].Value.ToString() == "1")
				throw new Exception($"Ошибка! Идентификатор {_tableIndFor4.Rows[i-1].Cells[1].Value} уже инициализирован.");
            else
            {
				_tableIndFor4.Rows[i-1].Cells[2].Value = "1";
				_tableIndFor4.Rows[i-1].Cells[3].Value = type;
			}

		}

		private void Dec(string type)
        {
			while (ints.Count != 0)
            {
				Decid(ints.Pop(), type);
            }
        }

		private string GetTypePerem()
		{
			return _tableIndFor4.Rows[_lexem2[1] - 1].Cells[3].Value.ToString();
        }

		private int Outst()
        {
			return ints.Pop();
        }

		private void Insts(int l)
        {
			ints.Push(l);
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
                        if (_lexem == "/*")
                        {
                            _index+=2;
                            GetLexem();
                            return;
                        }
                        break;
                    case (3):
                        _lexem = _tableNumber[indexInTable].ToString();
                        break;
                    case (4):
                        _lexem = _tableInd[indexInTable];
						_lexem2[0] = 4;
						_lexem2[1] = indexInTable;
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
		#endregion
    }
}
