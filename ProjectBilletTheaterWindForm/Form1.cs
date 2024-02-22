using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProjectBilletTheaterWindForm
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        const int NB_RANGS = 25;
        const int PLACES_PAR_RANG = 40;
        int[,] matricePlaces = new int[NB_RANGS, PLACES_PAR_RANG]; // Déclarer la matrice comme une variable de classe
        public Form1()
        {
            InitializeComponent();
            instance = this;

            // Initialize matricePlaces with random 0s and 1s
            InitializeRandomMatrice();
        }

        private void InitializeRandomMatrice()
        {
            //  matricePlaces[0, 0] = Haut Gauche
            //  matricePlaces[0, 40] = Haut Droite
            //  matricePlaces[25, 0] = Bas Gauche
            //  matricePlaces[25, 40] = Bas Droite
            
            Random random = new Random();

            for (int row = 0; row < NB_RANGS; row++)
            {
                for (int col = 0; col < PLACES_PAR_RANG; col++)
                {
                    // Generate a random number between 0 and 9
                    int randomNumber = random.Next(10);

                    // Assign 1 if the random number is less than 3 (30% probability)
                    matricePlaces[row, col] = randomNumber < 2 ? 1 : 0;
                }
            }
        }

        private void Form1_Validating(object sender, CancelEventArgs e)
        {

        }

        private void VerifierDisponibilitePlaces(int n)
        {
            for (int rang = 0; rang < NB_RANGS; rang++)
            {
                for (int siege = 0; siege <= PLACES_PAR_RANG - n; siege++)
                {
                    bool disponible = true;
                    for (int i = 0; i < n; i++)
                    {
                        if (matricePlaces[rang, siege + i] == 1)
                        {
                            disponible = false;
                            break;
                        }
                    }
                    if (disponible)
                    {
                        string message = $"Vous pouvez réserver les places suivantes : rang {rang + 1}, sièges {siege + 1} à {siege + n}. Confirmez-vous la réservation?";

                        // Ask in French if you want to confirm the reservation
                        DialogResult result = MessageBox.Show(message, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                        if (result == DialogResult.Yes)
                        {
                            // Set all values in the matrix to 1
                            for (int i = 0; i < n; i++)
                            {
                                matricePlaces[rang, siege + i] = 1;
                            }

                            MessageBox.Show("Réservation confirmée. Matrice mise à jour.");
                        }
                        else
                        {
                            MessageBox.Show("Réservation annulée. Aucune modification apportée à la matrice.");
                        }
                        return;
                    }
                }
            }
            MessageBox.Show($"Désolé, il n'y a pas de série de {n} places contiguës disponibles.");
        }

    private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Return) && String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Saisir un nombre place !");

            }
            else button1.Enabled = true;
            // Tester la touche backspace
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int numberOfPlaces))
            {
                // Successfully parsed to an integer, proceed with the logic
                VerifierDisponibilitePlaces(numberOfPlaces);
            }
            else
            {
                // Display an error message for non-integer input
                MessageBox.Show("Saisir un nombre valide de places (entier).");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(matricePlaces); // Pass the matricePlaces data to Form2
            if (form2.ShowDialog() == DialogResult.OK)
            {
                // Handle the return value from Form2 if needed
                // For example, you can access properties or methods in Form2
            }
        }
    }
}
