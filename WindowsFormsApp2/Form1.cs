using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WindowsFormsApp2.Entity;
using WindowsFormsApp2.Repository;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public List<KeyValuePair<Label, TextBox>> currentControls;
        ProductRepository productRepository;
        public int choice = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            currentControls = new List<KeyValuePair<Label, TextBox>>();
            radioButton1_CheckedChanged(null, EventArgs.Empty);

            groupBox2.Hide();

            productRepository = new ProductRepository();
        }

        private void ClearControls()
        {
            groupBox2.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";

            foreach (KeyValuePair<Label, TextBox> pair in currentControls)
            {
                Controls.Remove(pair.Key);
                Controls.Remove(pair.Value);
                
                pair.Key.Hide();
                pair.Value.Hide();
            }

            currentControls.Clear();
        }

        private void ResizeCurrentControls()
        {
            Point location = new Point(20, groupBox1.Height + 20);

            for (int i = 0; i < currentControls.Count; i++)
            {
                currentControls[i].Key.Location = new Point(location.X, location.Y + (i * 80));
                currentControls[i].Value.Location = new Point(location.X, currentControls[i].Key.Bottom + 10);

                Controls.Add(currentControls[i].Key);
                Controls.Add(currentControls[i].Value);

                currentControls[i].Key.Show();
                currentControls[i].Value.Show();
            }

            button1.Location = new Point(location.X, currentControls[currentControls.Count - 1].Value.Location.Y + 50);

            Controls.Add(button1);
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ClearControls();

            currentControls.Add(new KeyValuePair<Label, TextBox>(label1, textBox1));

            choice = 1;

            ResizeCurrentControls();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ClearControls();

            currentControls.Add(new KeyValuePair<Label, TextBox>(label6, textBox6));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label7, textBox7));

            choice = 2;

            ResizeCurrentControls();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ClearControls();

            currentControls.Add(new KeyValuePair<Label, TextBox>(label2, textBox2));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label3, textBox3));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label4, textBox4));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label5, textBox5));

            choice = 3;

            ResizeCurrentControls();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ClearControls();

            currentControls.Add(new KeyValuePair<Label, TextBox>(label1, textBox1));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label2, textBox2));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label3, textBox3));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label4, textBox4));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label5, textBox5));

            choice = 4;

            ResizeCurrentControls();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            ClearControls();

            currentControls.Add(new KeyValuePair<Label, TextBox>(label1, textBox1));

            choice = 5;

            ResizeCurrentControls();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            ClearControls();

            currentControls.Add(new KeyValuePair<Label, TextBox>(label6, textBox6));
            currentControls.Add(new KeyValuePair<Label, TextBox>(label7, textBox7));

            choice = 6;

            ResizeCurrentControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch(choice)
            {
                case 1:
                    try
                    {
                        groupBox2.Show();

                        Product product = productRepository.getById(Convert.ToInt32(textBox1.Text.Trim()));

                        label8.Text = "id категорії: " + product.categoryId;
                        label9.Text = "назва: " + product.name;
                        label10.Text = "ціна в доларах: " + product.priceInDollars;
                        label11.Text = "кількість: " + product.amount;
                    }
                    catch
                    {
                        MessageBox.Show("Ви ввели id неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 2:
                    try
                    {
                        List<Product> products = productRepository.getMany(Convert.ToInt32(textBox6.Text.Trim()), Convert.ToInt32(textBox7.Text.Trim()));
                        
                        if (!File.Exists("products.txt"))
                        {
                            File.Create("products.txt");
                        }
                        
                        using (StreamWriter writer = new StreamWriter("products.txt"))
                        {
                            foreach (Product product in products)
                            {
                                writer.WriteLine("id товару: " + product.id);
                                writer.WriteLine("id категорії: " + product.categoryId);
                                writer.WriteLine("назва: " + product.name);
                                writer.WriteLine("ціна в доларах: " + product.priceInDollars);
                                writer.WriteLine("кількість: " + product.amount);
                                writer.WriteLine();
                            }
                            System.Diagnostics.Process.Start("products.txt");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Ви ввели аргументи неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 3:
                    try
                    {
                        int priceInDollars = textBox4.Text.Trim() == "" ? 0 : Convert.ToInt32(textBox4.Text.Trim());
                        int amount = textBox5.Text.Trim() == "" ? 0 : Convert.ToInt32(textBox5.Text.Trim());

                        productRepository.createProduct(Convert.ToInt32(textBox2.Text.Trim()), textBox3.Text.Trim(),
                                priceInDollars, amount);
                    }
                    catch
                    {
                        MessageBox.Show("Ви ввели аргументи неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 4:
                    if (textBox1.Text.Trim() == "")
                    {
                        MessageBox.Show("Ви ввели id неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        try
                        {
                            int id = Convert.ToInt32(textBox1.Text.Trim());

                            int categoryId = textBox2.Text.Trim() == "" ? productRepository.getById(id).categoryId : Convert.ToInt32(textBox2.Text.Trim());
                            string name = textBox3.Text.Trim() == "" ? productRepository.getById(id).name : textBox3.Text.Trim();
                            int priceInDollars = textBox4.Text.Trim() == "" ? productRepository.getById(id).priceInDollars : Convert.ToInt32(textBox4.Text.Trim());
                            int amount = textBox5.Text.Trim() == "" ? productRepository.getById(id).amount : Convert.ToInt32(textBox5.Text.Trim());

                            productRepository.updateProduct(id, categoryId, name, priceInDollars, amount);
                        }
                        catch
                        {
                            MessageBox.Show("Ви ввели id неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;
                case 5:
                    try
                    {
                        productRepository.deleteById(Convert.ToInt32(textBox1.Text.Trim()));
                    }
                    catch
                    {
                        MessageBox.Show("Ви ввели id неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 6:
                    try
                    {
                        productRepository.deleteMany(Convert.ToInt32(textBox6.Text.Trim()), Convert.ToInt32(textBox7.Text.Trim()));
                    }
                    catch
                    {
                        MessageBox.Show("Ви ввели аргументи неправильно!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
