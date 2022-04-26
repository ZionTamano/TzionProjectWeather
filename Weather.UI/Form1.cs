using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Weather.Entites;
using Weather.DAL;
using Weather.Model;

namespace Weather.UI
{
    public partial class Form1 : Form
    {

        WeatherManager weatherManager = new WeatherManager();
        WeatherCity weatherCity = new WeatherCity();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string nameCity = textBox1.Text;
            label2.Visible = true;

            if (nameCity != null && nameCity != "")
            {
                var data = await weatherManager.GetCity(nameCity); //Func
                var d = await weatherManager.DataTabelCityAndGrid(nameCity);  //Func
                label2.Text = data;
                dataGridView1.Rows.Clear();
                foreach (var weather in d)
                {
                    dataGridView1.Rows.Add(weather.Key, weather.Value.current.temp_c.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please enter City!");
            }








        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            WeatherManager weatherManager = new WeatherManager();
            string nameCity = textBox1.Text;
            await weatherManager.Auto(nameCity);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string nameCity = textBox1.Text;
            await weatherManager.SaveDataDictionry(nameCity);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nameCity = textBox1.Text;
            int timeByUser = int.Parse(textBox2.Text);
            weatherManager.RefreshTableByUser(nameCity, timeByUser);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            string cityWeather = weatherManager.GetCityOfFile();
            label2.Text = cityWeather;
        }
    }
}
