
namespace ТЯП_Лекс_Анализ
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeTable()
        {
            TableReservWord.Rows.Add(1, "dim");
            TableReservWord.Rows.Add(2, "integer");
            TableReservWord.Rows.Add(3, "real");
            TableReservWord.Rows.Add(4, "boolean");
            TableReservWord.Rows.Add(5, "begin");
            TableReservWord.Rows.Add(6, "end");
            TableReservWord.Rows.Add(7, "true");
            TableReservWord.Rows.Add(8, "false");
            TableReservWord.Rows.Add(9, "if");
            TableReservWord.Rows.Add(10, "else");
            TableReservWord.Rows.Add(11, "for");
            TableReservWord.Rows.Add(12, "to");
            TableReservWord.Rows.Add(13, "step");
            TableReservWord.Rows.Add(14, "next");
            TableReservWord.Rows.Add(15, "while");
            TableReservWord.Rows.Add(16, "readln");
            TableReservWord.Rows.Add(17, "writeln");

            LimiterTable.Rows.Add(1, "{");
            LimiterTable.Rows.Add(2, ";");
            LimiterTable.Rows.Add(3, "}");
            LimiterTable.Rows.Add(4, "=");
            LimiterTable.Rows.Add(5, ":");
            LimiterTable.Rows.Add(6, ",");
            LimiterTable.Rows.Add(7, ">");
            LimiterTable.Rows.Add(8, "<");
            LimiterTable.Rows.Add(9, "!");
            LimiterTable.Rows.Add(10, "||");
            LimiterTable.Rows.Add(11, "&&");
            LimiterTable.Rows.Add(12, "-");
            LimiterTable.Rows.Add(13, "+");
            LimiterTable.Rows.Add(14, "/");
            LimiterTable.Rows.Add(15, "*");
            LimiterTable.Rows.Add(16, ".");
            LimiterTable.Rows.Add(17, ")");
            LimiterTable.Rows.Add(18, "(");
            LimiterTable.Rows.Add(19, ":=");
            LimiterTable.Rows.Add(20, "<=");
            LimiterTable.Rows.Add(21, ">=");
            LimiterTable.Rows.Add(22, "!=");
            LimiterTable.Rows.Add(23, "/*");
            LimiterTable.Rows.Add(24, "*/");
            LimiterTable.Rows.Add(25, "==");
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.CreateAnalisButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TableReservWord = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TableNumbers = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LimiterTable = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TableIdentifications = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.ProgramtextTextBox = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.AnalisResultTextBox = new System.Windows.Forms.TextBox();
            this.SyntaxAnalisButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableReservWord)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableNumbers)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimiterTable)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TableIdentifications)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ResultTextBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 704);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1368, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подробности анализа";
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ResultTextBox.Location = new System.Drawing.Point(6, 21);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ReadOnly = true;
            this.ResultTextBox.Size = new System.Drawing.Size(1356, 57);
            this.ResultTextBox.TabIndex = 1;
            // 
            // CreateAnalisButton
            // 
            this.CreateAnalisButton.Location = new System.Drawing.Point(12, 12);
            this.CreateAnalisButton.Name = "CreateAnalisButton";
            this.CreateAnalisButton.Size = new System.Drawing.Size(255, 32);
            this.CreateAnalisButton.TabIndex = 1;
            this.CreateAnalisButton.Text = "Выполнить лексический анализ";
            this.CreateAnalisButton.UseVisualStyleBackColor = true;
            this.CreateAnalisButton.Click += new System.EventHandler(this.CreateAnalisButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TableReservWord);
            this.groupBox2.Location = new System.Drawing.Point(18, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 319);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Таблица служебных слов(1)";
            // 
            // TableReservWord
            // 
            this.TableReservWord.AllowUserToAddRows = false;
            this.TableReservWord.AllowUserToDeleteRows = false;
            this.TableReservWord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableReservWord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Word});
            this.TableReservWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableReservWord.Location = new System.Drawing.Point(3, 18);
            this.TableReservWord.Name = "TableReservWord";
            this.TableReservWord.ReadOnly = true;
            this.TableReservWord.RowHeadersWidth = 51;
            this.TableReservWord.RowTemplate.Height = 24;
            this.TableReservWord.Size = new System.Drawing.Size(259, 298);
            this.TableReservWord.TabIndex = 0;
            this.TableReservWord.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DateReservWord_CellContentClick);
            // 
            // ID
            // 
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 6;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ID.Width = 55;
            // 
            // Word
            // 
            this.Word.Frozen = true;
            this.Word.HeaderText = "Слово";
            this.Word.MinimumWidth = 6;
            this.Word.Name = "Word";
            this.Word.ReadOnly = true;
            this.Word.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Word.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Word.Width = 150;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.TableNumbers);
            this.groupBox3.Location = new System.Drawing.Point(18, 379);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 319);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Таблица чисел(3)";
            // 
            // TableNumbers
            // 
            this.TableNumbers.AllowUserToAddRows = false;
            this.TableNumbers.AllowUserToDeleteRows = false;
            this.TableNumbers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableNumbers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4});
            this.TableNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableNumbers.Location = new System.Drawing.Point(3, 18);
            this.TableNumbers.Name = "TableNumbers";
            this.TableNumbers.ReadOnly = true;
            this.TableNumbers.RowHeadersWidth = 51;
            this.TableNumbers.Size = new System.Drawing.Size(259, 298);
            this.TableNumbers.TabIndex = 0;
            // 
            // Column3
            // 
            this.Column3.Frozen = true;
            this.Column3.HeaderText = "ID";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 55;
            // 
            // Column4
            // 
            this.Column4.Frozen = true;
            this.Column4.HeaderText = "Число";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LimiterTable);
            this.groupBox4.Location = new System.Drawing.Point(289, 54);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(265, 319);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Таблица разделителей(2)";
            // 
            // LimiterTable
            // 
            this.LimiterTable.AllowUserToAddRows = false;
            this.LimiterTable.AllowUserToDeleteRows = false;
            this.LimiterTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LimiterTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.LimiterTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LimiterTable.Location = new System.Drawing.Point(3, 18);
            this.LimiterTable.Name = "LimiterTable";
            this.LimiterTable.ReadOnly = true;
            this.LimiterTable.RowHeadersWidth = 51;
            this.LimiterTable.Size = new System.Drawing.Size(259, 298);
            this.LimiterTable.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "ID";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 55;
            // 
            // Column2
            // 
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Ограничитель";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TableIdentifications);
            this.groupBox5.Location = new System.Drawing.Point(289, 379);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(265, 319);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Таблица идентификаторов(4)";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // TableIdentifications
            // 
            this.TableIdentifications.AllowUserToAddRows = false;
            this.TableIdentifications.AllowUserToDeleteRows = false;
            this.TableIdentifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TableIdentifications.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6});
            this.TableIdentifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableIdentifications.Location = new System.Drawing.Point(3, 18);
            this.TableIdentifications.Name = "TableIdentifications";
            this.TableIdentifications.ReadOnly = true;
            this.TableIdentifications.RowHeadersWidth = 51;
            this.TableIdentifications.Size = new System.Drawing.Size(259, 298);
            this.TableIdentifications.TabIndex = 0;
            // 
            // Column5
            // 
            this.Column5.Frozen = true;
            this.Column5.HeaderText = "ID";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 55;
            // 
            // Column6
            // 
            this.Column6.Frozen = true;
            this.Column6.HeaderText = "Идентификатор";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 150;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.ProgramtextTextBox);
            this.groupBox6.Location = new System.Drawing.Point(560, 54);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(404, 644);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Исходный текст";
            // 
            // ProgramtextTextBox
            // 
            this.ProgramtextTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProgramtextTextBox.Location = new System.Drawing.Point(6, 21);
            this.ProgramtextTextBox.Multiline = true;
            this.ProgramtextTextBox.Name = "ProgramtextTextBox";
            this.ProgramtextTextBox.Size = new System.Drawing.Size(392, 617);
            this.ProgramtextTextBox.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.AnalisResultTextBox);
            this.groupBox7.Location = new System.Drawing.Point(970, 54);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(410, 644);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Результаты анализа";
            // 
            // AnalisResultTextBox
            // 
            this.AnalisResultTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AnalisResultTextBox.Location = new System.Drawing.Point(6, 18);
            this.AnalisResultTextBox.Multiline = true;
            this.AnalisResultTextBox.Name = "AnalisResultTextBox";
            this.AnalisResultTextBox.ReadOnly = true;
            this.AnalisResultTextBox.Size = new System.Drawing.Size(398, 620);
            this.AnalisResultTextBox.TabIndex = 0;
            this.AnalisResultTextBox.TextChanged += new System.EventHandler(this.AnalisResultTextBox_TextChanged);
            // 
            // SyntaxAnalisButton
            // 
            this.SyntaxAnalisButton.Location = new System.Drawing.Point(289, 12);
            this.SyntaxAnalisButton.Name = "SyntaxAnalisButton";
            this.SyntaxAnalisButton.Size = new System.Drawing.Size(265, 32);
            this.SyntaxAnalisButton.TabIndex = 7;
            this.SyntaxAnalisButton.Text = "Выполнить синтаксический анализ";
            this.SyntaxAnalisButton.UseVisualStyleBackColor = true;
            this.SyntaxAnalisButton.Click += new System.EventHandler(this.SyntaxAnalisButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1392, 789);
            this.Controls.Add(this.SyntaxAnalisButton);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.CreateAnalisButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Лексический анализатор";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableReservWord)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableNumbers)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LimiterTable)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TableIdentifications)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox ResultTextBox;
        private System.Windows.Forms.Button CreateAnalisButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox ProgramtextTextBox;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.DataGridView TableReservWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word;
        private System.Windows.Forms.TextBox AnalisResultTextBox;
        private System.Windows.Forms.DataGridView LimiterTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridView TableNumbers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridView TableIdentifications;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.Button SyntaxAnalisButton;
    }
}

