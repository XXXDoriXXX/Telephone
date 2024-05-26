using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Telephone
{
    public partial class Form1 : Form
    {

        struct info
        {
            public string prizv, imja, adresa, tel;
        }
        info r;
        info[] arr = new info[100];
        int n = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 1;
            comboBox1.Items.Add("");
            comboBox1.SelectedIndex = 0;
            tabPage1.Text = "Список всіх об'єктів";
            tabPage2.Text = "Редагування та пошук записів";
        }

        private void зберегтиЗміниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((textBox2.Text!=""&&textBox3.Text!=""&&textBox4.Text!=""&&textBox5.Text!=""))
                {
                   
                    dataGridView1.Rows[selectedRowIndex].Cells[0].Value = textBox2.Text;
                    dataGridView1.Rows[selectedRowIndex].Cells[1].Value = textBox5.Text;
                    dataGridView1.Rows[selectedRowIndex].Cells[2].Value = textBox3.Text;
                    dataGridView1.Rows[selectedRowIndex].Cells[3].Value = textBox4.Text;
                    newComboBox();
                }
                else if(comboBox1.SelectedIndex< 0)
                {
                    MessageBox.Show("Такого користувача не знайдено!");
                }
                else  MessageBox.Show("Введіть дані!");
            }
            catch
            {
                MessageBox.Show("Невідома помилка, спробуйте ще раз!");
            }
        
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void кінецьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void проПрограмуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about form2 = new about();
            form2.ShowDialog();
        }
        public void newComboBox()
        {
            n = 0;
            comboBox1.Items.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                object prizvValue = dataGridView1.Rows[i].Cells[0].Value;
                if (prizvValue != null)
                {
                    string prizv = prizvValue.ToString();
                    comboBox1.Items.Add(prizv);
                }
                n++;
            }
        }
        private void прочитатиЗФайлуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear(); 
            dataGridView1.RowCount = File.ReadAllLines("data.txt").Length+1;
            info tmp = new info();
            int i = 0, n = 0;
            string line;
            var file = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "data.txt"));
            while ((line = file.ReadLine()) != null)
            {
                string[] arrayInfo = line.Split('\t');
                if (arrayInfo.Length >= 4)
                {
                    tmp.prizv = arrayInfo[0];
                    tmp.imja = arrayInfo[1];
                    tmp.adresa = arrayInfo[2];
                    tmp.tel = arrayInfo[3];

                    arr[n] = tmp;

                    dataGridView1.Rows[i].Cells[0].Value = tmp.prizv;
                    dataGridView1.Rows[i].Cells[1].Value = tmp.imja;
                    dataGridView1.Rows[i].Cells[2].Value = tmp.adresa;
                    dataGridView1.Rows[i].Cells[3].Value = tmp.tel;

                    i++;
                    n++;
                }
            }
            newComboBox();
            file.Close();
        }

        private void записатиУФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            info tmp = new info();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "data.txt");

            using (var file = new StreamWriter(filePath))
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++) 
                {
                    tmp.prizv = dataGridView1.Rows[i].Cells[0].Value?.ToString() ?? string.Empty;
                    tmp.imja = dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? string.Empty;
                    tmp.adresa = dataGridView1.Rows[i].Cells[2].Value?.ToString() ?? string.Empty;
                    tmp.tel = dataGridView1.Rows[i].Cells[3].Value?.ToString() ?? string.Empty;

                    if (string.IsNullOrEmpty(tmp.prizv) || string.IsNullOrEmpty(tmp.imja) ||
                        string.IsNullOrEmpty(tmp.adresa) || tmp.tel.Length<18||string.IsNullOrEmpty(tmp.tel))
                    {
                        MessageBox.Show("Всі поля повинні бути заповнені. Дані не збережено.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    file.WriteLine($"{tmp.prizv}\t{tmp.imja}\t{tmp.adresa}\t{tmp.tel}");
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Items.Count > 0 && comboBox1.SelectedIndex > 0)
                {
                    comboBox1.SelectedIndex--;
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show("Сталася помилка: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Items.Count > 0 && comboBox1.SelectedIndex < comboBox1.Items.Count - 1)
                {
                    comboBox1.SelectedIndex++;
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Сталася помилка: " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1.Text = textBox1.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex > 0;

         
            button4.Enabled = comboBox1.SelectedIndex < comboBox1.Items.Count - 1;
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            newComboBox();
        }
    
        
        void clears()
        {
            textBox1.Text = "";
            textBox5.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Items.Clear();
            comboBox1.Text = "";
            button1.Enabled = false;
            button4.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clears();
        }
        int selectedRowIndex = -1;
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
            string searchValue = textBox1.Text.Trim();
            bool found = false;

            listBox1.Items.Clear(); 

            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["Column1"].Value != null && row.Cells["Column1"].Value.ToString().Equals(searchValue, StringComparison.OrdinalIgnoreCase))
                    {
                     
                        string item = $"{row.Cells["Column1"].Value.ToString()} - {row.Cells["Column2"].Value.ToString()} - {row.Cells["Column3"].Value.ToString()} - {row.Cells["Column4"].Value.ToString()}";
                        listBox1.Items.Add(item);
                        selectedRowIndex = row.Index;
                        found = true;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Елемент не знайдено.", "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox5.Clear();
                }
                else
                {
                 
                    MessageBox.Show($"{listBox1.Items.Count} збіг(ів) знайдено.", "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Дані користувача невірні!", "Результат пошуку", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

        private void аЯToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("Column1"))
            {
             
                dataGridView1.Columns["Column1"].SortMode = DataGridViewColumnSortMode.Programmatic;

               
                dataGridView1.Sort(dataGridView1.Columns["Column1"], ListSortDirection.Ascending);
            }
            newComboBox();
        }

        private void яАToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Columns.Contains("Column1"))
            {
             
                dataGridView1.Columns["Column1"].SortMode = DataGridViewColumnSortMode.Programmatic;

            
                dataGridView1.Sort(dataGridView1.Columns["Column1"], ListSortDirection.Descending);
            }
            newComboBox(); clears();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        void namecheck(object sender, EventArgs e) { 
            //if(this.Text())
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
               
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsPunctuation(e.KeyChar) || char.IsSymbol(e.KeyChar))
            {
            
                e.Handled = true;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 3|| dataGridView1.CurrentCell.ColumnIndex == 1 || dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                if (e.Control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.KeyPress -= TextBox_KeyPress;
                    textBox.KeyPress += TextBox_KeyPress;
                }
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox textBox = sender as System.Windows.Forms.TextBox;
          
            if (dataGridView1.CurrentCell != null)
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 1 || dataGridView1.CurrentCell.ColumnIndex == 0)
                {
                    if (char.IsLetter(e.KeyChar))
                    {
                        return;
                    }

                    e.Handled = true;
                }
                else if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["Column4"].Index)
                {
                    if (textBox.Text == "")
                    {
                        textBox.Text += "+38(0";
                        textBox.SelectionStart = textBox.Text.Length;
                    }

                    if ((textBox.Text.Length == 7 || textBox.Text.Length == 12 || textBox.Text.Length == 15)&&!(char.IsControl(e.KeyChar)))
                    {
                        int selectionStart = textBox.SelectionStart;

                        if (textBox.Text.Length == 7)
                        {
                            textBox.Text += ")";
                            selectionStart++;
                        }

                        textBox.Text += "-";
                        selectionStart++;

                        textBox.SelectionStart = selectionStart;
                    }
                          textBox.SelectionStart = textBox.Text.Length;
                    if ((char.IsDigit(e.KeyChar) && textBox.Text.Length < 18) || char.IsControl(e.KeyChar))
                    {
                        return;
                    }

                    e.Handled = true;
                }

                
            }
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string[] selectedValues = listBox1.SelectedItem.ToString().Split(new string[] { " - " }, StringSplitOptions.None);

                if (selectedValues.Length == 4)
                {
                    textBox2.Text = selectedValues[0];
                    textBox5.Text = selectedValues[1];
                    textBox3.Text = selectedValues[2];
                    textBox4.Text = selectedValues[3];
                }
            }
        }
    }
}
    

