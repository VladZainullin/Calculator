using System;
using System.Drawing;
using System.Windows.Forms;

namespace CalculatorVersionTwo
{
    public partial class Calculator : System.Windows.Forms.Form
    {
        Point lastPoint;
        int indexOperation; //Индекс знака операции в строке estimations.Text
        double value = 0, memory = 0;
        bool enterSpecialOperationAfterEqually = false; //Состояние кнопки равно: OFF
        bool findComma = false; //Найдена ли запятая в строке estimations.Text: OFF
        bool enterClassicOperation = false; //Состояние кнопки спец. операции: OFF
        bool enterSpecialOperation = false; //Состояние кнопки равно: OFF
        bool enterEqually = false; //Состояние кнопки равно: OFF
        bool ND = false; //Состояние кнопки Second: OFF
        bool enterSO; //Состояние кнопки равно: OFF
        char sign = '+'; //Знак
        string operation; //Переменная для текста операции
        public Calculator()
        {
            InitializeComponent();
        }
        //Ввод символов
        private void ClickNumber(object sender, EventArgs e)
        {
            if (enterEqually == false) //Кнопка равно не нажата
            {
                lblResult.Text = lblResult.Text == "0" ? (sender as Button).Text : lblResult.Text += (sender as Button).Text;
            }
            else //Если была нажата кнопка равно
            {
                lblResult.Text = (sender as Button).Text;
                lblEstimations.Text = "";
                value = Convert.ToDouble(lblResult.Text);
                enterEqually = false; 
            }
        }
        //Привести данные в исходное состояние
        private void ClickC(object sender, EventArgs e)
        {
            enterSpecialOperationAfterEqually = false;
            enterClassicOperation = false;
            enterSpecialOperation = false;
            enterEqually = false;
            findComma = false;
            enterSO = false;
            indexOperation = 0;
            value = 0;
            lblEstimations.Text = "";
            lblResult.Text = "0";
            sign = '+';
        }
        //Константа Пи
        private void ClickPI(object sender, EventArgs e)
        {
            lblResult.Text = Convert.ToString(Math.PI);
            if (enterEqually == true)
            {
                lblEstimations.Text = "";
                enterEqually = false;
            }
        }
        //Константа Е
        private void ClickE(object sender, EventArgs e)
        {
            lblResult.Text = Convert.ToString(Math.E);
            if (enterEqually == true)
            {
                lblEstimations.Text = "";
                enterEqually = false;
            }
        }
        //Поменять знак
        private void ClickPlusMinus(object sender, EventArgs e)
        {
            if (lblResult.Text != "0")
            {
                if (sign == '+')
                {
                    sign = '-';
                    if (lblResult.Text[0] != '-')
                    {
                        lblResult.Text = '-' + lblResult.Text;
                        if (enterSpecialOperation == true)
                        {
                            if (enterClassicOperation == false)
                            {
                                lblEstimations.Text = '-' + lblEstimations.Text;
                            }
                            else
                            {
                                lblEstimations.Text = lblEstimations.Text.Remove(indexOperation) + '-' + lblEstimations.Text.Substring(indexOperation);
                            }
                        }
                    }
                    else
                    {
                        lblResult.Text = lblResult.Text.Remove(0, 1);
                        if (enterSpecialOperation == true)
                        {
                            if (enterClassicOperation == false)
                            {
                                lblEstimations.Text = '-' + lblEstimations.Text;
                            }
                            else
                            {
                                lblEstimations.Text = lblEstimations.Text.Remove(indexOperation) + '-' + lblEstimations.Text.Substring(indexOperation);
                            }
                        }
                    }
                }
                else if (sign == '-')
                {
                    sign = '+';
                    if (lblResult.Text[0] == '-')
                    {
                        lblResult.Text = lblResult.Text.Remove(0, 1);
                        if (enterClassicOperation == false)
                        {
                            if (enterSpecialOperation == true)
                            {
                                lblEstimations.Text = lblEstimations.Text.Remove(0, 1);
                            }
                        }
                        else
                        {
                            if (enterSpecialOperation == true)
                            {
                                lblEstimations.Text = lblEstimations.Text.Remove(indexOperation) + lblEstimations.Text.Substring(indexOperation + 1);
                            }
                        }
                    }
                    else
                    {
                        if (enterClassicOperation == false)
                        {
                            lblResult.Text = '-' + lblResult.Text;
                            if (enterSpecialOperation == true)
                            {
                                lblEstimations.Text = lblEstimations.Text.Remove(0, 1);
                            }
                        }
                        else
                        {
                            if (enterSpecialOperation == false)
                            {
                                lblResult.Text = lblResult.Text.Remove(0, 1);
                                lblEstimations.Text = '-' + lblEstimations.Text;
                            }
                            else
                            {
                                lblResult.Text = '-' + lblResult.Text;
                                lblEstimations.Text = lblEstimations.Text.Remove(indexOperation) + lblEstimations.Text.Substring(indexOperation + 1);
                            }
                        }
                    }
                }
            }
        }
        //Стандартные операции +, -...
        private void ClickClassicOperation(object sender, EventArgs e)
        {
            sign = '+';
            operation = (sender as Button).Text;

            enterEqually = false;
            if (lblResult.Text[lblResult.Text.Length - 1] != ',')
            {
                if (enterClassicOperation == false)
                {
                    if (enterSpecialOperation == false)
                    {
                        lblEstimations.Text = lblResult.Text + (sender as Button).Text;
                    }
                    else
                    {
                        lblEstimations.Text += (sender as Button).Text;
                    }
                    enterClassicOperation = true;
                    value = Double.Parse(lblResult.Text);
                    lblResult.Text = "0";
                }
                else
                {
                    if (enterSpecialOperation == false)
                    {
                        lblEstimations.Text = lblEstimations.Text.Remove(lblEstimations.Text.Length - 1);
                        lblEstimations.Text += (sender as Button).Text;
                    }
                }
            }
            else
            {
                lblEstimations.Text = lblResult.Text.Remove(lblResult.Text.Length - 1) + (sender as Button).Text;
                if (enterClassicOperation == false)
                {
                    enterClassicOperation = true;
                    value = Double.Parse(lblResult.Text);
                    lblResult.Text = "0";
                }
                else
                {
                    lblEstimations.Text += (sender as Button).Text;
                }
            }
            findComma = false;
            enterSpecialOperation = false;
            indexOperation = lblEstimations.Text.Length;
        } 
        //Кнопка равно
        private void ClickEqually(object sender, EventArgs e)
        {
            if (enterClassicOperation == true)
            {
                if (enterEqually == false)
                {
                    // Проверка: последний символ не равен запятой
                    lblResult.Text = lblResult.Text[lblResult.Text.Length - 1] == ',' ? lblResult.Text.Remove(lblResult.Text.Length - 1) : lblResult.Text;
                    lblEstimations.Text = enterSpecialOperation == false ? lblEstimations.Text + lblResult.Text + '=' : lblEstimations.Text + '=';
                    enterSpecialOperationAfterEqually = true;
                    enterEqually = true; //Кнопка равно: ON
                    enterClassicOperation = false; //Кнопка операции: OFF
                    enterSpecialOperation = false; //Кнопка спец. операции: OFF
                    findComma = false; //Можно вновь ввести запятую
                    switch (operation) // Выполнение классической операции в зависимости от текста на кнопке
                    {
                        case "+":
                            lblResult.Text = Convert.ToString(value + Double.Parse(lblResult.Text));
                            break;
                        case "−":
                            lblResult.Text = Convert.ToString(value - Double.Parse(lblResult.Text));
                            break;
                        case "×":
                            lblResult.Text = Convert.ToString(value * Double.Parse(lblResult.Text));
                            break;
                        case "÷":
                            lblResult.Text = Convert.ToString(value / Double.Parse(lblResult.Text));
                            break;
                        case "^":
                            lblResult.Text = Convert.ToString(Math.Pow(value, Double.Parse(lblResult.Text)));
                            break;
                    }
                    value = 0;
                    sign = '+'; //Знак
                }
            }
        }
        //Закрыть окна
        private void ClickExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Свернуть окно
        private void ClickMinimized(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //Функции памяти
        private void ClickMemory(object sender, EventArgs e)
        {
            switch ((sender as Button).Text)
            {
                case "M+":
                    memory += Double.Parse(lblResult.Text);
                    break;
                case "M-":
                    memory -= Double.Parse(lblResult.Text);
                    break;
                case "MC":
                    memory = 0;
                    break;
                case "MR":
                    if (memory != 0)
                    {
                        lblResult.Text = Convert.ToString(memory);
                        if (enterEqually == true) lblEstimations.Text = "";
                    }
                    break;
                case "MS":
                    memory = 0;
                    memory = Double.Parse(lblResult.Text);
                    break;
            }
            lblMemory.Text = Convert.ToString(memory);
        }
        //Передвижение окна
        private void formMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void newLastPoint(object sender, MouseEventArgs e)
        {
            lastPoint.X = e.X;
            lastPoint.Y = e.Y;
        }
        //Дополнительные операции: arcsin, arcsh...
        private void ClickSecond(object sender, EventArgs e)
        {
            if (ND == false)
            {
                ND = true;
                btnSinArcsin.Text = "arcsin";
                btnCosArccos.Text = "arccos";
                btnTgArctg.Text = "arctg";
                btnCtgArcctg.Text = "arcctg";
                btnChArcch.Text = "arcch";
                btnShArcsh.Text = "arcsh";
                btnThArcth.Text = "arccth";
                btnCthArccth.Text = "arccth";
                btnND.BackColor = Color.DodgerBlue;
            }   
            else
            {
                ND = false;
                btnSinArcsin.Text = "sin";
                btnCosArccos.Text = "cos";
                btnTgArctg.Text = "tg";
                btnCtgArcctg.Text = "ctg";
                btnChArcch.Text = "ch";
                btnShArcsh.Text = "sh";
                btnThArcth.Text = "th";
                btnCthArccth.Text = "cth";
                btnND.BackColor = Color.Transparent;
            }
        }

        private void EnterExit(object sender, EventArgs e)
        {
            button36.ForeColor = Color.White;

        }

        private void LeaveExit(object sender, EventArgs e)
        {
            button36.ForeColor = Color.Black;
        }

        //Стереть предыдущее действие
        private void ClickCE(object sender, EventArgs e)
        {
            sign = '+';
            if (enterEqually == false)
            {
                if (enterClassicOperation == false)
                {
                    if (enterSpecialOperation == true)
                    {
                        lblEstimations.Text = "";
                        enterSpecialOperation = false;
                    }
                }
                else
                {
                    if (enterSpecialOperation == false)
                    {
                        if (lblResult.Text == "0")
                        {
                            lblEstimations.Text = "";
                        }
                    }
                    else
                    {
                        lblEstimations.Text = lblEstimations.Text.Remove(indexOperation);
                    }
                }
            }
            else
            {
                if (enterClassicOperation == false)
                {
                    if (enterSpecialOperation == true)
                    {
                        lblEstimations.Text = "";
                        enterSpecialOperation = false;
                        enterSO = false;
                    }
                }
            }
            lblResult.Text = "0";
        }

        private void ClickComma(object sender, EventArgs e)
        {
            if (enterSpecialOperation == false)
            {
                if (enterClassicOperation == false)
                {
                    if (findComma == false)
                    {
                        lblResult.Text += (sender as Button).Text;
                        findComma = true;
                    }
                }
                else
                {
                    if (findComma == false)
                    {
                        lblResult.Text += (sender as Button).Text;
                        findComma = true;
                    }
                }
            }
        }
        //Специальные операции: sin, ln, !...
        private void ClickSpecialOperation(object sender, EventArgs e)
        {
            sign = '+';
            enterSpecialOperation = true;
            if (lblResult.Text[lblResult.Text.Length - 1] == ',')
            {
                lblResult.Text = lblResult.Text.Substring(0, lblResult.Text.Length - 1);
            }
            if (enterClassicOperation == false)
            {
                if (enterEqually == true)
                {
                    if (enterSpecialOperationAfterEqually == false)
                    {
                        lblEstimations.Text = (sender as Button).Text + "(" + lblEstimations.Text + ")";
                    }
                    else
                    {
                        lblEstimations.Text = (sender as Button).Text + "(" + lblResult.Text + ")";
                        enterSpecialOperationAfterEqually = false;
                    }
                }
                else if (lblEstimations.Text == "")
                {
                    lblEstimations.Text = (sender as Button).Text + "(" + lblResult.Text + ")";
                }
                enterEqually = true;
            }
            else
            {
                if (enterEqually == false)
                {
                    if (enterSO == false)
                    {
                        lblEstimations.Text = lblEstimations.Text + (sender as Button).Text + "(" + lblResult.Text + ")";
                        enterSO = true;
                    }
                    else
                    {
                        lblEstimations.Text = lblEstimations.Text.Remove(indexOperation) + (sender as Button).Text + "(" + lblEstimations.Text.Substring(indexOperation) + ")";
                    }
                }
            }
            switch ((sender as Button).Text)
            {
                case "lg":
                    lblResult.Text = Convert.ToString(Math.Log10(Double.Parse(lblResult.Text)));
                    break;
                case "ln":
                    lblResult.Text = Convert.ToString(Math.Log(Double.Parse(lblResult.Text)));
                    break;
                case "abs":
                    lblResult.Text = Convert.ToString(Math.Abs(Double.Parse(lblResult.Text)));
                    break;
                case "√":
                    lblResult.Text = Convert.ToString(Math.Sqrt(Double.Parse(lblResult.Text)));
                    break;
                case "sin":
                    lblResult.Text = Convert.ToString(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180));
                    break;
                case "cos":
                    if (Double.Parse(lblResult.Text) % -360 < 90 || Double.Parse(lblResult.Text) % -360 > 270)
                        lblResult.Text = Convert.ToString(Math.Pow(1 - Math.Pow(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180), 2), 0.5));
                    else
                        lblResult.Text = Convert.ToString(-Math.Pow(1 - Math.Pow(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180), 2), 0.5));
                    break;
                case "ctg":
                    if (Double.Parse(lblResult.Text) % -360 < 90 || Double.Parse(lblResult.Text) % -360 > 270)
                        lblResult.Text = Convert.ToString(-Math.Pow(1 - Math.Pow(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180), 2), 0.5) / Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180));
                    else
                        lblResult.Text = Convert.ToString(Math.Pow(1 - Math.Pow(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180), 2), 0.5) / Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180));
                    break;
                    break;
                case "tg":
                    if (Double.Parse(lblResult.Text) % -360 < 90 || Double.Parse(lblResult.Text) % -360 > 270)
                    {
                        lblResult.Text = Convert.ToString(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180) / Math.Pow(1 - Math.Pow(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180), 2), 0.5));
                    }
                    else
                    {
                        lblResult.Text = Convert.ToString(-Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180) / Math.Pow(1 - Math.Pow(Math.Sin(Double.Parse(lblResult.Text) % 180 * Math.PI / 180), 2), 0.5));
                    }
                    break;
                case "sh":
                    lblResult.Text = Convert.ToString(Math.Sinh(Double.Parse(lblResult.Text)));
                    break;
                case "ch":
                    lblResult.Text = Convert.ToString(Math.Cosh(Double.Parse(lblResult.Text)));
                    break;
                case "th":
                    lblResult.Text = Convert.ToString(Math.Tanh(Double.Parse(lblResult.Text)));
                    break;
                case "cth":
                    lblResult.Text = Convert.ToString(Math.Cosh(Double.Parse(lblResult.Text)));
                    break;
                case "!":
                    double factorial = Double.Parse(lblResult.Text);
                    for (int i = 1; i < Double.Parse(lblResult.Text); i++)
                    {
                        factorial *= i;
                    }
                    lblResult.Text = Convert.ToString(factorial);
                    break;
                case "arccos":
                    lblResult.Text = Convert.ToString(Math.Acos(Double.Parse(lblResult.Text)) * 180 / Math.PI);
                    break;
                case "arcsin":
                    lblResult.Text = Convert.ToString(Math.Asin(Double.Parse(lblResult.Text)) * 180 / Math.PI);
                    break;
                case "arctg":
                    lblResult.Text = Convert.ToString(Math.Asin(Double.Parse(lblResult.Text)) * 180 / Math.PI);
                    break;
                case "arcctg":
                    lblResult.Text = Convert.ToString(Math.Asin(Double.Parse(lblResult.Text)) * 180 / Math.PI);
                    break;
                case "arcsh":
                    lblResult.Text = Convert.ToString(Math.Log(Double.Parse(lblResult.Text) + Math.Pow(Math.Pow(Double.Parse(lblResult.Text), 2) + 1, 0.5)));
                    break;
                case "arcch":
                    lblResult.Text = Convert.ToString(Math.Log(Double.Parse(lblResult.Text) + Math.Pow(Double.Parse(lblResult.Text) + 1, 0.5) * Math.Pow(Double.Parse(lblResult.Text) - 1, 0.5)));
                    break;
                case "arccth":
                    lblResult.Text = Convert.ToString(0.5 * Math.Log((Double.Parse(lblResult.Text) + 1) / (1 - Double.Parse(lblResult.Text))));
                    break;
                case "arcth":
                    lblResult.Text = Convert.ToString(0.5 * Math.Log((Double.Parse(lblResult.Text) + 1) / (Double.Parse(lblResult.Text) - 1)));
                    break;
                case "rad":
                    lblResult.Text = Convert.ToString(Math.PI * Double.Parse(lblResult.Text) / 180);
                    break;
            }
        }
    }
}
