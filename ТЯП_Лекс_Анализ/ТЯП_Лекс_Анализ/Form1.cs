using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ТЯП_Лекс_Анализ
{
    public partial class Form1 : Form
    {
        LexicalAnalyzer lexicalAnalyzer;
        SyntaxAnalizer syntaxAnalizer;
        public Form1()
        {
            InitializeComponent();
            InitializeTable();
            Dictionary<int, string> tablerw = new Dictionary<int, string>();
            Dictionary<int, string> tablelimiter = new Dictionary<int, string>();
            //Dictionary<int, string> tableind = new Dictionary<int, string>();
            for (int i = 0; i < TableReservWord.Rows.Count; i++)
            {
                tablerw.Add(Convert.ToInt32(TableReservWord.Rows[i].Cells[0].Value), Convert.ToString(TableReservWord.Rows[i].Cells[1].Value));
            }
            for (int i = 0; i < LimiterTable.Rows.Count; i++)
            {
                tablelimiter.Add(Convert.ToInt32(LimiterTable.Rows[i].Cells[0].Value), Convert.ToString(LimiterTable.Rows[i].Cells[1].Value));
            }
            //for (int i = 0; i < TableIdentifications.Rows.Count; i++)
            //{
            //    tableind.Add(Convert.ToInt32(TableIdentifications.Rows[i].Cells[0].Value), Convert.ToString(TableIdentifications.Rows[i].Cells[1]));
            //} 
            lexicalAnalyzer = new LexicalAnalyzer(tablerw, tablelimiter);
            lexicalAnalyzer.NewTI += AddNewRecordInTI;
            lexicalAnalyzer.NewTN += AddNewRecordInTN;
            lexicalAnalyzer.OutResult += Out;
        }
        public void ErrorMessage(string errMsg)
        {
           // if (ResultTextBox.Text == "")
                ResultTextBox.Text = errMsg;
        }
        public void Out(string s)
        {
            AnalisResultTextBox.Text += $" {s}";
        }
        public void AddNewRecordInTI(string s)
        {
            TableIdentifications.Rows.Add(Convert.ToInt32(TableIdentifications.Rows.Count + 1), s);
            Out($"(4,{TableIdentifications.Rows.Count})");
        }
        public void AddNewRecordInTN(string s)
        {
            TableNumbers.Rows.Add(Convert.ToInt32(TableNumbers.Rows.Count + 1), s);
            AnalisResultTextBox.Text += $" (3,{TableNumbers.Rows.Count})";
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CreateAnalisButton_Click(object sender, EventArgs e)
        {
            if (ProgramtextTextBox.Text == "")
                return;
            TableIdentifications.Rows.Clear();
            TableNumbers.Rows.Clear();
            AnalisResultTextBox.Text = "";
            lexicalAnalyzer.ProgramText = ProgramtextTextBox.Text;
            if (lexicalAnalyzer.Scaner())
                ResultTextBox.Text = "Лексический анализ выполнен успешно";
            else
                ResultTextBox.Text = "Ошибка проведения лексического анализа";
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void DateReservWord_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AnalisResultTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SyntaxAnalisButton_Click(object sender, EventArgs e)
        {
                CreateAnalisButton_Click(new object(), new EventArgs());
            if (ResultTextBox.Text == "Лексический анализ выполнен успешно")
            {
                ResultTextBox.Text = "";
                string arg = AnalisResultTextBox.Text.Replace("(", "");
                arg = arg.Replace(")", "");
                arg = arg.TrimStart(' ');
                string[] listAnalizer = arg.Split(' ');
                Dictionary<int, string> tablerw = new Dictionary<int, string>();
                Dictionary<int, string> tablelimiter = new Dictionary<int, string>();
                Dictionary<int, string> tableind = new Dictionary<int, string>();
                for (int i = 0; i < TableReservWord.Rows.Count; i++)
                {
                    tablerw.Add(Convert.ToInt32(TableReservWord.Rows[i].Cells[0].Value), Convert.ToString(TableReservWord.Rows[i].Cells[1].Value));
                }
                for (int i = 0; i < LimiterTable.Rows.Count; i++)
                {
                    tablelimiter.Add(Convert.ToInt32(LimiterTable.Rows[i].Cells[0].Value), Convert.ToString(LimiterTable.Rows[i].Cells[1].Value));
                }
                for (int i = 0; i < TableIdentifications.Rows.Count; i++)
                {
                    tableind.Add(Convert.ToInt32(TableIdentifications.Rows[i].Cells[0].Value), Convert.ToString(TableIdentifications.Rows[i].Cells[1].Value));
                }
                syntaxAnalizer = new SyntaxAnalizer(listAnalizer, tablerw, tableind, tablelimiter);
                syntaxAnalizer.ErrorMessage += ErrorMessage;
                syntaxAnalizer.Pr();
                if (ResultTextBox.Text == "")
                    ResultTextBox.Text = "Синтаксический анализ успешно выполнен.";
            }
        }
    }
}
