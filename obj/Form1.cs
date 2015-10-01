using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Regex_Maker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text;
            string matchingField = textBox4.Text;
            string matchingValue = textBox2.Text;

            bool hasLetters = Regex.IsMatch(matchingValue, @"[a-zA-Z]");
            bool hasNumbers = Regex.IsMatch(matchingValue, @"\d");
            matchingField = matchingField.Replace(">", string.Empty).Replace("<", string.Empty);
            string final;
            if (matchingValue == input)
            {
                textBox3.Text = string.Format(@"^(?<{0}>.+)", matchingField);
            }

            else if (Regex.IsMatch(input, matchingValue))
            {
                if (input.IndexOf(matchingValue) > 0)
                {
                    //string must be further in capture
                    final = @"^.{" + (input.IndexOf(matchingValue)).ToString() + "}" + string.Format(@"(?<{0}>", matchingField);
                    final = final + GetRegex(input.Substring(input.IndexOf(matchingValue), matchingValue.Length)) + ")";
                    if (!string.IsNullOrEmpty(input.Remove(0,(input.IndexOf(matchingValue) + matchingValue.Length)))) {
                        final = final + ".+";
                    }
                    textBox3.Text = final;
                }
                else
                {
                    final = string.Format(@"^(?<{0}>", matchingField);

                    final = final + GetRegex(matchingValue);

                    final = final + ")";

                    if (!string.IsNullOrEmpty(input.Replace(matchingValue, string.Empty)))
                    {
                        final = final + ".+";
                    }

                    textBox3.Text = final;
                }



            }
            else
            {
                textBox3.Text = "Error";
            }
        }

        public string GetRegex(string value)
        {
            string final = "";

            List<string> groups = new List<string>();
            while (value.Length > 0)
            {
                groups.Add(Regex.Match(value, @"[a-zA-z]+|\d+|\W+").ToString());
                value = value.Remove(0, Regex.Match(value, @"[a-zA-z]+|\d+|\W+").ToString().Length);
            }

            foreach (string thing in groups)
            {
                if (Regex.IsMatch(thing, @"[a-zA-Z]"))
                {
                    final = final + @"[a-zA-Z]{" + thing.Length + "}";
                }
                else if (Regex.IsMatch(thing, @"\d"))
                {
                    final = final + @"\d{" + thing.Length + "}";
                }
                else if (Regex.IsMatch(thing, @"\W"))
                {
                    final = final + @"\W{" + thing.Length + "}";
                }
            }

            return final;
        }

    }
}
