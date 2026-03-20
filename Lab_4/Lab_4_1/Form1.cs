namespace Lab_4_1
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            IProgress<int> onChangeProgress = new Progress<int>((i) =>
            {
                label1.Text = i.ToString();
                progressBar1.Value = i;
            });

            cts = new CancellationTokenSource();
            button1.Enabled = false;

            try
            {
                int result = await Process(100, onChangeProgress, cts.Token);
                label1.Text = "Готово: " + result;
            }
            catch (OperationCanceledException)
            {
                label1.Text = "Скасовано!";
                progressBar1.Value = 0;
            }
            finally
            {
                button1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts?.Cancel();
        }

        async Task<int> Process(int count, IProgress<int> ChangeProgressBar, CancellationToken cancellToken)
        {
            return await Task.Run(() =>
            {
                int i;
                for (i = 1; i <= count; i++)
                {
                    if (cancellToken.IsCancellationRequested)
                        cancellToken.ThrowIfCancellationRequested();

                    ChangeProgressBar.Report(i);
                    Thread.Sleep(100);
                }
                return i - 1;
            });
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
