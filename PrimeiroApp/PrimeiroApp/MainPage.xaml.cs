using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PrimeiroApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        int count = 0;

        private void Button_Clicked(object sender, EventArgs e)
        {
            count++;
            ((Button)sender).Text = "Você clicou " + count.ToString() + " Vezes";
        }
        private void btnVerificar_Clicked(object sender, EventArgs e)
        {
            string texto = $" O nome tem {txtNome.Text.Length} caracteres";
            DisplayAlert("Mensagem", texto, "ok");
        }

        private async void btnLimpar_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Pergunta", "Deseja realmente limpar tela?", "Yes", "No"))
            {
                txtNome.Text = string.Empty;
                btnCliqueAqui.Text = "Clique Aqui";
            }
        }
        private async void btnInformarDataNAscimento_Clicked(object sender,EventArgs e)
        {
            try
            {
                string dataDigitada = await DisplayPromptAsync("Info",
                    "Digite a Data de Nascimento",
                    "ok");
                DateTime dataConvertida;
                bool converteu = DateTime.TryParse(dataDigitada, new CultureInfo("pt-br"), DateTimeStyles.None, out dataConvertida);
                //If(converteu)
                if(converteu == false)
                {
                    throw new Exception("Esta data não é válida");
                }
                else
                {
                    lblDataNascimento.Text = string.Format("{0:dd/MM/yyyy}", dataDigitada);
                    int diasVividos = (int)DateTime.Now.Subtract(dataConvertida).TotalDays;
                    await DisplayAlert("Info", $"Você já viveu{diasVividos} dias.","ok");
                }


            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + ex.InnerException, "ok");
            }

        }

        private async void btnOpcoes_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(lblDataNascimento.Text))
                    throw new Exception("Informe a Data de Nascimento");
                else
                {
                    DateTime dtNascimento = Convert.ToDateTime(lblDataNascimento.Text, new CultureInfo("pt-BR"));

                    string resposta = await
                        DisplayActionSheet("Pergunta",
                        "Selecione uma opção",
                        "Cancelar",
                        "Saber o dia da semana",
                        "Saber o dia o mês",
                        "Saber o dia do ano");

                    if(resposta == "Saber o dia da semana")
                    {
                        string msg = $"Você nasceu no(a) {dtNascimento.DayOfWeek}";
                        await DisplayAlert("Info", msg, "ok");
                    }
                    else if (resposta == "Saber o dia do mês")
                    {
                        string msg = $"Você nasceu no(a) {dtNascimento.Day}Dia do mês";
                        await DisplayAlert("Info", msg, "ok");
                    }
                    else if (resposta == "Saber o dia do ano")
                    {
                        string msg = $"Você nasceu no(a) {dtNascimento.DayOfYear}Dia do ano";
                        await DisplayAlert("Info", msg, "ok");
                    }

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message + ex.InnerException, "ok");
            }
        }
    }
}
