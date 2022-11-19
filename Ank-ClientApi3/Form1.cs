using Newtonsoft.Json;

namespace Ank_ClientApi3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_kaydet_Click(object sender, EventArgs e)
        {
            DateTimeView model = new DateTimeView();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://worldtimeapi.org/api/");
                var getTask = client.GetAsync("ip");
                getTask.Wait();
                var result = getTask.Result;
                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show(" Ýstek Atýlamadý. ");
                }
                var readJsonTask = result.Content.ReadAsStringAsync();
                readJsonTask.Wait();
                var okunan = readJsonTask.Result;
                model = JsonConvert.DeserializeObject<DateTimeView>(okunan);
                txt_tarih.Text = model.dateTime.ToShortDateString();
                txt_saat.Text = model.dateTime.ToShortTimeString();
                txt_hafta.Text = model.week_number.ToString();
                dateTimePicker1.Value = model.dateTime.AddDays(3);
            }
        }
    }
}