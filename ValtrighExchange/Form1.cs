using System.Net.Http.Headers;
using System.Text.Json;

namespace ValtrighExchange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ClientREST.init();
        }
        private async void Read(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            Rootobject product = null;
            product = await ClientREST.GetValue("/FinanzaMercati/api/TickerInfo/GetItemJSON?tickerName=SPMib&tickerName=UKX.LSE&tickerName=DAX.XET&tickerName=INX.USD&tickerName=EURUS.FX&tickerName=SPREAD_ITL_DEM.MTS");

            foreach (QuotaIndice q in product.list)
            {
                listView1.Items.Add($"QUOTE: {q.QUOTE}");
                listView1.Items.Add($"F10011: {q.F10011}");
                listView1.Items.Add($"F10061: {q.F10061}");
                listView1.Items.Add($"segno: {q.segno}");
                listView1.Items.Add($"F10015: {q.F10015}");
                listView1.Items.Add($"F20002: {q.F20002}");
                listView1.Items.Add($"F20721: {q.F20721}");
                listView1.Items.Add($"Open: {q.Open}");
                listView1.Items.Add("");
                listView1.Items.Add("-----");
                listView1.Items.Add("");
            }
        }
    }

    class ClientREST
    {
        static HttpClient client = new HttpClient();
        public static void init()
        {
            client.BaseAddress = new Uri("https://vwd-proxy.ilsole24ore.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<Rootobject> GetValue(string path)
        {
            Rootobject product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await JsonSerializer.DeserializeAsync<Rootobject>(await response.Content.ReadAsStreamAsync());
            }
            return product;
        }
    }

    public class Rootobject
    {
        public string ITEMS { get; set; }
        public bool loggedIn { get; set; }
        public QuotaIndice[] list { get; set; }
    }

    public class QuotaIndice
    {
        public string QUOTE { get; set; }
        public string F10011 { get; set; }
        public string F10061 { get; set; }
        public string segno { get; set; }
        public string F10015 { get; set; }
        public string F20002 { get; set; }
        public string F20721 { get; set; }
        public bool Open { get; set; }
    }
}