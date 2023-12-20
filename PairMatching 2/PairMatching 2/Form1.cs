using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace PairMatching_2
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        List<string> icons = new List<string>()
        {
            "!","!"
            ,"N","N",
            ",",",",
            "k","k",
            "b","b",
            "v","v",
            "w","w",
            "z","z" // To  use Images, each letter will transfr to image as we are using webding font
                                                                            
        };

        Label firstclicked, secondclicked; //To store first and second clicked

        int count = 0;

        public Form1()
        {
            InitializeComponent();
            AssignIconToSuare();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void AssignIconToSuare()
        {
            Label label;
            int randomnumber;
            for (int i = 0; i < tableLayoutPanel1.Controls.Count; i++)
            {
                if (tableLayoutPanel1.Controls[i] is Label)
                {
                    label = (Label)tableLayoutPanel1.Controls[i]; //casting controls to labels
                }
                else
                {
                    continue; //if it is not label do nothing and continue 
                }
                randomnumber = rand.Next(0, icons.Count);// To randomly assign every  icon to one cell in table
                label.Text = icons[randomnumber]; // To set randomly assigned icon as label text to show in cell
                icons.RemoveAt(randomnumber); //To remove the assigned icon from list so that it should not be used again
            }
        }

        private void label_Click(object sender, EventArgs e)
        {

            if (firstclicked != null && secondclicked != null)
            {
                return; // if first and second click is not null, means you already clicked two times then third click is not allowed until the timer is not expired
            }

            Label clickedlabel = sender as Label; //
            if (clickedlabel == null)
            {
                return; //this condition mean Do not count the click if you clicked out of the table area
            }
            if (clickedlabel.ForeColor == Color.Black)
            {
                return;// This condition mean if you click on a label and it is open and you clicked again on same opened label, it will not count it as new click
            }
            if (firstclicked == null)
            {
                firstclicked = clickedlabel;//To store which label you clicked in this  first click
                firstclicked.ForeColor = Color.Black;// To make the clicked label visible
                return;
            }

            secondclicked = clickedlabel;//after dealing with first click now we will see what happened if we click second button
            secondclicked.ForeColor = Color.Black;//To view second pic 


            if (firstclicked.Text == secondclicked.Text)//if both the pictures are same do not use    timer and do not make invisible
            {
                firstclicked = null;
                secondclicked = null;
                count++;
                if (count == 8)
                {
                    MessageBox.Show("You win");
                }
            }
            else
            {
                timer1.Start(); //To enable user to view both the icons for 800ms
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstclicked.ForeColor = firstclicked.BackColor;
            secondclicked.ForeColor = secondclicked.BackColor; //To set back the colors of both label again invisible
            firstclicked = null;
            secondclicked = null;//To make it possible to again start first click and second click
        }
    }
}



