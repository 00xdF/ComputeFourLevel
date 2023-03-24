using ComputerLevel.Common;
using ComputerLevel.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerLevel.VIewModel
{
    internal class MainViewModel
    {
        public QuestionModel Question { get; set; }
        public BaseCommand NextQuestionCommand { get; set; }
        public BaseCommand ChangeQuestionCommand { get;set; }
        public BaseCommand ChangeQuestionBankCommand { get; set; }
        public BaseCommand FrontQuestionCommand { get;set; }
        public MainViewModel()
        {
            Question= new QuestionModel();
            GetQuestion(0);
            NextQuestionCommand = new BaseCommand();
            FrontQuestionCommand = new BaseCommand();
            ChangeQuestionCommand = new BaseCommand();
            ChangeQuestionBankCommand = new BaseCommand();
            //下个题
            NextQuestionCommand.DoExecute += ((obj) =>
            {
                var id = obj as string;
                Trace.WriteLine($"id->{id}");
                if (Convert.ToInt32(id) < 79)
                    GetQuestion(Convert.ToInt32(id) + 1);
            });
            NextQuestionCommand.DoCanExcute += ((obj) =>
            {
                return true;
            });
            //通过按钮改变题
            ChangeQuestionCommand.DoExecute += ((obj) =>
            {
                var button = obj as Button;
                GetQuestion(Convert.ToInt32(button.Content)-1);
            });
            ChangeQuestionCommand.DoCanExcute += ((obj) =>
            {
                return true;
            });
            //上个题
            FrontQuestionCommand.DoExecute += ((obj) =>
            {
                var id = obj as string;
                Trace.WriteLine($"id->{id}");
                if (Convert.ToInt32(id) >= 1)
                    GetQuestion(Convert.ToInt32(id) - 1);
            });
            FrontQuestionCommand.DoCanExcute += ((obj) =>
            {
                return true;
            });
            //改变题库
            ChangeQuestionBankCommand.DoExecute += ((obj) =>
            {
                var id = Convert.ToInt32(obj);
                Data.QuestionData.WhenQuestion = Data.QuestionData.GetQuestions(id);
                GetQuestion(0);
            });
            ChangeQuestionBankCommand.DoCanExcute += ((obj) =>
            {
                return true;
            });
        }

        void GetQuestion(int index)
        {
            string questions = Data.QuestionData.WhenQuestion;
            dynamic obj = JsonConvert.DeserializeObject(questions);
            int TotalQuestionCount = obj.data.total_qnum;
            if (index >= TotalQuestionCount || index < 0)
            {
                return;
            }
            string id = index+"";
            string question = ((string)obj.data.list[index].question).Replace("<p>", "").Replace("</p>", "");
            string qimage = obj.data.list[index].qimage;
            string rightkey = obj.data.list[index].rightkey;
            string analysis = ((string)obj.data.list[index].analysis).Replace("<p>", "").Replace("</p>", "");
            
            Option options = new Option();
            for(var i = 0; i < 5; i++)
            {
                try
                {
                    switch ((string)obj.data.list[index].option[i].o)
                    {
                        case "A":
                            options.A = ((string)obj.data.list[index].option[i].p).Replace("<p>","").Replace("</p>","");
                            break;
                        case "B":
                            options.B = ((string)obj.data.list[index].option[i].p).Replace("<p>", "").Replace("</p>", "");
                            break;
                        case "C":
                            options.C = ((string)obj.data.list[index].option[i].p).Replace("<p>", "").Replace("</p>", ""); ;
                            break;
                        case "D":
                            options.D = ((string)obj.data.list[index].option[i].p).Replace("<p>", "").Replace("</p>", "");
                            break;
                        case "E":
                            options.E = ((string)obj.data.list[index].option[i].p).Replace("<p>", "").Replace("</p>", "");
                            break;

                    }
                }
                catch(Exception e)
                {
                    options.E = "";
                    Trace.WriteLine("option存取错误-->"+e.Message);
                    continue;
                }
            }
            Question.qimage = qimage;
            Question.id = id;
            Question.analysis = analysis;
            Question.answer = rightkey;
            Question.question = question;
            Question.options = options;
        }
    }
}
