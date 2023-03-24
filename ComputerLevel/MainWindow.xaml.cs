using ComputerLevel.Common;
using ComputerLevel.VIewModel;
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

namespace ComputerLevel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            CreateQuestionBox();
        }

        /// <summary>
        /// 创建问题按钮
        /// </summary>
        public void CreateQuestionBox()
        {
            int COUNT = 1;
            //创建操作系统单选选项按钮
            for(var i = 0; i < 5; i++)
            {
                for(var j = 0; j < 6; j++)
                {
                    var Button = new Button();
                    Button.Content = COUNT++;
                    Button.Command = (DataContext as MainViewModel).ChangeQuestionCommand;
                    Button.CommandParameter = Button; //传递Button本身
                    Button.Name = "OperationButton" + COUNT;
                    QuestionBox.Children.Add(Button);
                }
            }
            //创建操作系统多选按钮
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    var Button = new Button();
                    Button.Content = COUNT++;
                    Button.Command = (DataContext as MainViewModel).ChangeQuestionCommand;
                    Button.CommandParameter = Button; //传递Button本身
                    Button.Name = "OperationMultiButton" + COUNT;
                    QuestionCheckBox.Children.Add(Button);
                }
            }
            //创建数据库单选按钮
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 6; j++)
                {
                    var Button = new Button();
                    Button.Content = COUNT++;
                    Button.Command = (DataContext as MainViewModel).ChangeQuestionCommand;
                    Button.CommandParameter = Button; //传递Button本身
                    Button.Name = "DatabaseButton" + COUNT;
                    DataBaseQuestionBox.Children.Add(Button);
                }
            }
            //创建数据库多选按钮
            for (var i = 0; i < 5; i++)
            {
                for (var j = 0; j < 2; j++)
                {
                    var Button = new Button();
                    Button.Content = COUNT++;
                    Button.Command = (DataContext as MainViewModel).ChangeQuestionCommand;
                    Button.CommandParameter = Button; //传递Button本身
                    Button.Name = "DatabaseMultiButton" + COUNT;
                    DataBaseQuestionCheckBox.Children.Add(Button);
                }
            }
            COUNT = 0;
            for(var i = 0; i < 30; i++)
            {
                var Button = new Button();
                Button.Content = $"第{COUNT +1}套真题";
                Button.Command = (DataContext as MainViewModel).ChangeQuestionBankCommand;
                Button.CommandParameter = COUNT++; //传递索引
                QuestionBankList.Children.Add(Button);
            }
        }

        /// <summary>
        /// 点击提交按钮 判断当前的选项是否和答案一致
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_CommitMyAnswer(object sender, RoutedEventArgs e)
        {
            var model = this.DataContext as MainViewModel;
            var answer = "";
            //拼接答案
            if(RadioQuestion.Visibility== Visibility.Visible)
            {
                if (this.Radio_OptionA.IsChecked == true)
                {
                    answer += "A";
                }
                if (this.Radio_OptionB.IsChecked == true)
                {
                    answer += "B";
                }
                if (this.Radio_OptionC.IsChecked == true)
                {
                    answer += "C";
                }
                if (this.Radio_OptionD.IsChecked == true)
                {
                    answer += "D";
                }
            }
            else if(CheckBoxQuestion.Visibility == Visibility.Visible)
            {
                if (this.CheckBox_OptionA.IsChecked == true)
                {
                    answer += "A";
                }
                if (this.CheckBox_OptionB.IsChecked == true)
                {
                    answer += "B";
                }
                if (this.CheckBox_OptionC.IsChecked == true)
                {
                    answer += "C";
                }
                if (this.CheckBox_OptionD.IsChecked == true)
                {
                    answer += "D";
                }
                if (this.CheckBox_OptionE.IsChecked == true)
                {
                    answer += "E";
                }
            }
           //判断答案是否正确
            if (CheckAnswer(answer,model.Question.answer.Replace(",","")))
            {
                //回答正确
                AnalysisText.Visibility = Visibility.Visible;
                AnalysisText.Foreground = Brushes.Green;
                AnalysisText.Text = $"回答正确! {model.Question.analysis}";
                foreach (var button in FindVisualChildren<Button>(this))
                {
                    if ((button.Content+"").Equals(Convert.ToUInt32(model.Question.id) +1+""))
                    {
                        button.Background = Brushes.Green;
                        break;
                    }
                }
            }
            else
            {
                //回答错误
                AnalysisText.Foreground = Brushes.Red;
                AnalysisText.Visibility = Visibility.Visible;
                AnalysisText.Text = $"回答错误! 你的答案:{answer}, {model.Question.analysis}";
                foreach (var button in FindVisualChildren<Button>(this))
                {
                    if ((button.Content + "").Equals(Convert.ToUInt32(model.Question.id) + 1 + ""))
                    {
                        button.Background = Brushes.Red;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 遍历按钮
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        /// <summary>
        /// 通过算法匹配答案是否正确
        /// </summary>
        /// <param name="YourAnswer"></param>
        /// <param name="AureAnswer"></param>
        /// <returns></returns>
        public bool CheckAnswer(string YourAnswer,string SureAnswer)
        {
            if (YourAnswer.Length != SureAnswer.Length)
            {
                return false;
            }
            bool isEqual = YourAnswer.ToLower().OrderBy(c => c).SequenceEqual(SureAnswer.ToLower().OrderBy(c => c));
            return isEqual;
        }
    }
}
