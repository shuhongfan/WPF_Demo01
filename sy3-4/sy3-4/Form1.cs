namespace sy3_4
{
    public partial class Form1 : Form
    {
        // ����һ��ί��
        public delegate void InputHandler(String s);

        // ����һ���¼�
        public static event InputHandler InputEvent;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            InputEvent(textBox1.Text.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}