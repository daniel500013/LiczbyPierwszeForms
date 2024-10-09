using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiczbyPierwszeForms
{
    public partial class MainForm : Form
    {
        private PrimaryNumbersRepository primaryNumbersRepository { get; set; }
        private XmlRepository xmlRepository { get; set; }
        private CycleTimer cycleTimerRepository { get; set; }

        public MainForm()
        {
            InitializeComponent();

            primaryNumbersRepository = new PrimaryNumbersRepository();
            cycleTimerRepository = new CycleTimer();
            xmlRepository = new XmlRepository();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;

            richTextBox.Clear();
            
            cycleTimerRepository.StartNewCycle();
            cycleTimer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = true;

            cycleTimer.Stop();
            breakTimer.Stop();

            primaryNumbersRepository.Print(richTextBox);
            primaryNumbersRepository.Reset();

            statusLabel.Text = "Status: Oczekuje na start";
            timerLabel.Text = "00:00/02:00";
        }

        private void cycleTimer_Tick(object sender, EventArgs e)
        {
            statusLabel.Text = "Status: Szuka liczb pierwszych";

            timerLabel.Text =
                $"{cycleTimerRepository.GetCycleTimer().Minutes:00}:{cycleTimerRepository.GetCycleTimer().Seconds:00}/02:00";

            if (cycleTimerRepository.GetCycleTimer() >= TimeSpan.FromMinutes(2))
            {
                try
                {
                    cycleTimer.Stop();

                    primaryNumbersRepository.Print(richTextBox);
                    xmlRepository.ZapiszDaneDoXML(primaryNumbersRepository.Step, cycleTimerRepository.GetCycleTimer(), primaryNumbersRepository.BiggestPrimeNumber);

                    cycleTimerRepository.StartNewCycle();
                    breakTimer.Start();
                }
                catch
                {
                    MessageBox.Show($"Nie znaleziono nowej liczby pierwszej w cyklu {primaryNumbersRepository.Step}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                primaryNumbersRepository.GetPrimeNumber();
            }
        }

        private void breakTimer_Tick(object sender, EventArgs e)
        {
            statusLabel.Text = "Status: Przerwa";

            timerLabel.Text =
                $"{cycleTimerRepository.GetBreakTimer().Minutes:00}:{cycleTimerRepository.GetBreakTimer().Seconds:00}/01:00";

            if (cycleTimerRepository.GetBreakTimer() >= TimeSpan.FromMinutes(1))
            {
                breakTimer.Stop();
                cycleTimerRepository.StartNewCycle();
                primaryNumbersRepository.SetCurrentNumber(primaryNumbersRepository.BiggestPrimeNumber);
                primaryNumbersRepository.SetNextStep();
                cycleTimer.Start();
            }
        }
    }
}
