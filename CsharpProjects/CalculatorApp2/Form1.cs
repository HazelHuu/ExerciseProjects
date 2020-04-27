using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculatorApp2
{
    public partial class Form1 : Form
    {
        private string textViewString;

        public Form1()
        {
            InitializeComponent();
        }
        //1
        private void button1_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '1';
            textBox1.Text = textViewString;
        }
        //2
        private void button2_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '2';
            textBox1.Text = textViewString;
        }
        //3
        private void button3_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '3';
            textBox1.Text = textViewString;
        }
        //4
        private void button4_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '4';
            textBox1.Text = textViewString;
        }
        //5
        private void button5_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '5';
            textBox1.Text = textViewString;
        }
        //6
        private void button6_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '6';
            textBox1.Text = textViewString;
        }
        //7
        private void button7_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '7';
            textBox1.Text = textViewString;
        }
        //8
        private void button8_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '8';
            textBox1.Text = textViewString;
        }
        //9
        private void button9_Click(object sender, EventArgs e)
        {
            textViewString = textViewString + '9';
            textBox1.Text = textViewString;
        }
        //0
        private void button10_Click(object sender, EventArgs e)
        {
            char lastChar = textViewString[textViewString.Length - 1];
            if (lastChar != '/')
            {
                if (textViewString != null)
                {
                    textViewString = textViewString + '0';
                    textBox1.Text = textViewString;
                }
            }
            else
            {
                textBox1.Text = "ERROR";
            }
        }
        //+
        private void button13_Click(object sender, EventArgs e)
        {
            if (textViewString.Length > 0) {
                char lastChar = textViewString[textViewString.Length - 1];
                if (lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/')
                {
                    textViewString = textViewString + '+';
                    textBox1.Text = textViewString;
                }
                else
                {
                    textBox1.Text = textViewString + "ERROR";
                }
            }
            else
            {
                textBox1.Text = "ERROR";
            }
        }
        //-
        private void button14_Click(object sender, EventArgs e)
        {
            if (textViewString.Length > 0)
            {
                char lastChar = textViewString[textViewString.Length - 1];
                if (lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/')
                {
                    textViewString = textViewString + '-';
                    textBox1.Text = textViewString;
                }
                else
                {
                    textBox1.Text = textViewString + "ERROR";
                }
            }
            else
            {
                textBox1.Text = "ERROR";
            }
        }
        //*
        private void button15_Click(object sender, EventArgs e)
        {
            if (textViewString.Length > 0)
            {
                char lastChar = textViewString[textViewString.Length - 1];
                if (lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/')
                {
                    textViewString = textViewString + '*';
                    textBox1.Text = textViewString;
                }
                else
                {
                    textBox1.Text = textViewString + "ERROR";
                }
            }
            else
            {
                textBox1.Text = "ERROR";
            }
        }
        // '/'
        private void button11_Click(object sender, EventArgs e)
        {
            if (textViewString.Length > 0)
            {
                char lastChar = textViewString[textViewString.Length - 1];
                if (lastChar != '+' && lastChar != '-' && lastChar != '*' && lastChar != '/')
                {
                    textViewString = textViewString + '/';
                    textBox1.Text = textViewString;
                }
                else
                {
                    textBox1.Text = textViewString + "ERROR";
                }
            }
            else
            {
                textBox1.Text = "ERROR";
            }
        }
        //=
        private void button12_Click(object sender, EventArgs e)
        {
            if (textViewString.Length > 0)
            {
                char lastChar = textViewString[textViewString.Length - 1];
                if (lastChar != 'R')
                {
                    Emulator emulator = new Emulator();
                    string result = Convert.ToString(emulator.EvaluateExpression(textViewString));
                    textBox1.Text = result;
                    textViewString = result;
                }
            }
        }
        //CLEAR
        private void textBox1_TextClear(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textViewString = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }


    class Emulator
    {
        public string EvaluateExpression(string expressions)
        {//计算表达式
            expressions = expressions + '#';

            //存储运算值的栈
            myStack operation = new myStack();
            operation.Push(0);

            //存储运算符的栈
            myStack TR = new myStack();
            TR.Push('#');

            char c, theta;
            char[] num = new char[20];
            int j = 0;
            for (int i = 0; i < expressions.Length; i++)
            {
                c = Convert.ToChar(expressions[i]);
                int a, b;
                if (Char.IsDigit(c))
                {
                    num[j++] = c;
                    continue;
                }
                else
                {
                    if (j != 0) { num[j] = Convert.ToChar(0); operation.Push(strToNum(num)); j = 0; }
                    switch (CompareTR(TR, c))
                    {
                        case '<': { TR.Push(c); break; }
                        case '>':
                            {
                                theta = Convert.ToChar(TR.Pop());
                                b= Convert.ToInt32(operation.Pop()); a= Convert.ToInt32(operation.Pop());
                                operation.Push(Operate(a, theta, b));
                                break;
                            }
                        case '=': { c = Convert.ToChar(TR.Pop()); break; }
                    }
                }
            }
            return Convert.ToString(operation.Pop());
        }

        int strToNum(char[] s)
        {                  //字符转数字
            int m = 0, k = 0;
            while (s[k] != '\0')
            {
                m = m * 10 + s[k] - '0';
                k++;
            }
            return m;
        }

        private int Operate(int a, char theta, int b)
        {//计算
            int iResult = -1;
            if (theta == '+') return (a + b);
            else if (theta == '-') return (a - b);
            else if (theta == '*') return (a * b);
            else if (theta == '/') return (a / b);
            return iResult;
        }

        private char CompareTR(myStack S, char c)
        {//判断优先级
            char iResult =' ';
            switch (S.GetTop())
            {
                case '+':
                    {
                        if (c == '*' || c == '/' || c == '(') return '<';
                        else if (c == '+' || c == '-' || c == ')' || c == '#') return '>';
                        break;
                    }
                case '-':
                    {
                        if (c == '*' || c == '/' || c == '(') return '<';
                        else if (c == '+' || c == '-' || c == ')' || c == '#') return '>';
                        break;
                    }
                case '*':
                    {
                        if (c == '(') return '<';
                        else if (c == '+' || c == '-' || c == '*' || c == '/' || c == ')' || c == '#') return '>';
                        break;
                    }
                case '/':
                    {
                        if (c == '(') return '<';
                        else if (c == '+' || c == '-' || c == '*' || c == '/' || c == ')' || c == '#') return '>';
                        break;
                    }
                case '(':
                    {
                        if (c == ')') return '=';
                        else return '<';
                        break;
                    }
                case ')':
                    {
                        return '>';
                        break;
                    }
                case '#':
                    {
                        if (c == '#') return '=';
                        else return '<';
                        break;
                    }
            }
            return iResult;
        }
    }

    class myStack
    {
        StackForm top;//栈顶元素
        public void Push(object data)
        {//入栈
            //根据当前栈顶元素新构建一个新的栈顶，并将当前栈顶的NextItem指向原来的top
            top = new StackForm(top, data);
        }
        public object Pop()
        { //出栈
            if (top == null)
                throw new InvalidOperationException();
            object result = top.data;
            top = top.nextItem;//重新指定栈顶
            return result;
        }
        public object GetTop()
        { //获取栈顶元素
            if (top == null)
                throw new InvalidOperationException();
            object result = top.data;
            return result;
        }
        //栈的数据格式，用链表来实现栈
        class StackForm
        {
            public StackForm nextItem;//栈的下一个数据，自栈顶往下
            public object data;//栈顶数据
            public StackForm(StackForm sNext, object sData)
            {
                this.nextItem = sNext;
                this.data = sData;
            }
        }
    }

}
