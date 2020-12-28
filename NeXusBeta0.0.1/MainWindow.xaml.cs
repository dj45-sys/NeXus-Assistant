using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using NeXusLibrary;
using System.Data.SqlClient;

namespace NeXusBeta0._0._1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SpeechRecognitionEngine rec = new SpeechRecognitionEngine();
        SpeechSynthesizer nexus = new SpeechSynthesizer();
        List<Commands> list = new List<Commands>();
        public MainWindow()
        {
            InitializeComponent();
            fillList();
        }

        private void btnConfig_Click(object sender, RoutedEventArgs e)
        {
           ConfigVista c = new ConfigVista();
            c.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rec.SetInputToDefaultAudioDevice();

            Choices sentences = new Choices(loadSentences());

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(sentences);

            Grammar g = new Grammar(gb);

            rec.LoadGrammar(g);
            rec.RecognizeAsync(RecognizeMode.Multiple);

            rec.SpeechRecognized += Rec_SpeechRecognized;
        }

        private void Rec_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (Commands c in list)
            {
                if (e.Result.Confidence > 0.6)
                {
                    if (c.Command.ToString().Equals(e.Result.Text.ToString()))
                    {
                        //nexus.Speak(c.Command.ToString());

                        if (c.Action.Trim().Length > 0 || c.Action != null)
                        {
                            System.Diagnostics.Process.Start(c.Action.ToString());
                        }
                        if (c.Response.Trim().Length > 0)
                        {
                            nexus.SpeakAsync(c.Response.ToString());
                        }
                    }
                } 
            }
        }
        private void fillList()
        {
            SqlConnection con = ConnectionDB.getConnection();
            List<Commands> list = new List<Commands>();

            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM Command;"), con);

            SqlDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                list.Add(new Commands(rs.GetInt32(0), rs.GetString(1), rs.GetString(2), rs.GetString(3)));
            }
            con.Close();
        }

        private string[] loadSentences()
        {   
            string[] sentences = new string[0];
            
            SqlConnection con = ConnectionDB.getConnection();
            
            SqlCommand cmd = new SqlCommand(string.Format("SELECT Command FROM Command;"), con);

            SqlDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                Array.Resize(ref sentences, sentences.Length + 1);
                sentences[sentences.Length - 1] = rs.GetString(0);
            }
            
            return sentences;
        }
    }
}
