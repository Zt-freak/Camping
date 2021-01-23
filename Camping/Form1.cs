using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Camping
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double costs = 0;
            DateTime elevenJulyStart = new DateTime(this.dateTimePicker1.Value.Year, 7, 11);

            DateTime fifteenAugustStart = new DateTime(this.dateTimePicker1.Value.Year, 8, 15);

            TimeSpan fullJulyAugustSpan = fifteenAugustStart - elevenJulyStart;
            Console.WriteLine(fullJulyAugustSpan.Days);


            DateTime rentEndToStartYear = new DateTime(this.dateTimePicker1.Value.Year, this.dateTimePicker2.Value.Month, this.dateTimePicker2.Value.Day);
            Console.WriteLine(rentEndToStartYear);

            TimeSpan totalrentSpan = this.dateTimePicker2.Value - this.dateTimePicker1.Value;

            var mainSeason = new { start = elevenJulyStart, end = fifteenAugustStart };
            var rentRange = new { start = this.dateTimePicker1.Value, end = rentEndToStartYear };
            var intersectionStart = mainSeason.start < rentRange.start ? rentRange.start : mainSeason.start;
            var intersectionEnd = mainSeason.end < rentRange.end ? mainSeason.end : rentRange.end;
            var intersection = intersectionStart < intersectionEnd ? new { start = intersectionStart, end = intersectionEnd } : null;

            TimeSpan rentSpan = rentRange.end - rentRange.start;
            double rentSpanDays = rentSpan.Days;
            if (rentSpanDays < 0)
            {
                rentSpanDays = 365 + rentSpanDays;
            }
            Console.WriteLine(rentSpanDays);

            if (intersection != null)
            {
                TimeSpan intersectSpan = intersection.end - intersection.start;
                double intersectSpanDays = intersectSpan.TotalDays;

                double daysOutsideSeason = rentSpanDays - intersectSpanDays;
                costs += 30 * intersectSpanDays;
                costs += 25 * daysOutsideSeason;
            }
            else
            {
                costs += 25 * rentSpanDays;
            }

            if (this.dateTimePicker1.Value.Year != this.dateTimePicker2.Value.Year)
            {
                double yearsbetween = this.dateTimePicker2.Value.Year - this.dateTimePicker1.Value.Year;
                costs += yearsbetween * fullJulyAugustSpan.Days * 30 + 365 - fullJulyAugustSpan.Days * 25;
            }

            if (this.numericUpDown1.Value > 10)
            {
                costs += costs * 1.03 * (Decimal.ToDouble(this.numericUpDown1.Value) - 10);
            }
            else if (this.numericUpDown1.Value < 10)
            {
                costs -= costs * 0.02 * (10 -  Decimal.ToDouble(this.numericUpDown1.Value));
            }

            costs += totalrentSpan.TotalDays * Decimal.ToDouble(this.numericUpDown2.Value) * 5;
            costs += totalrentSpan.TotalDays * Decimal.ToDouble(this.numericUpDown3.Value) * 4;

            if (this.checkBox1.Checked)
            {
                costs += totalrentSpan.TotalDays * 6;
            }

            this.textBox1.Text = costs.ToString();
        }
    }
}
