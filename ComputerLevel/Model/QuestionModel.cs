using ComputerLevel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerLevel.Model
{
    /// <summary>
    /// 题目类
    /// </summary>
    internal class QuestionModel:PropertyChangedNotify
    {
        private string _id;
        private string _question;
        private string _qimage;
        private string _answer;
        private Option _options;
        private string _analysis;
        public string id { 
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                this.DoNotify();
            } 
        }
        public string question { 
            get
            {
                return _question;
            }
            set 
            {
                this._question = value;
                this.DoNotify();
            }
        }
        public string qimage { get { return _qimage; } set { this._qimage = value;this.DoNotify(); } }

        public Option options { get { return _options; } set { this._options = value;this.DoNotify(); } }

        public string answer { get { return _answer; } set { _answer = value;this.DoNotify(); } }
        public string analysis { get { return _analysis; } set { this._analysis = value;DoNotify(); } }

        public QuestionModel(string id,string question,string qimage, Option options,string answer,string analysis)
        {
            this.id = id;
            this.question = question;
            this.qimage = qimage;
            this.answer = answer;
            this.options = options;
            this.analysis = analysis;
        }

        public QuestionModel()
        {
            
        }
    }

    class Option
    {
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
    }
}
