using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreNlog.Model
{
    public partial class Log
    {
        public int id { get; set; }

        private string _Application;
        [Key]
        public string Application
        {
            get { return _Application; }
            set
            {
                if (_Application == value)
                    return;
                _Application = value;
            }
        }

        private string _Logged;
        public string Logged
        {
            get { return _Logged; }
            set
            {
                if (_Logged == value)
                    return;
                _Logged = value;
            }
        }

        private string _Level;
        public string Level
        {
            get { return _Level; }
            set
            {
                if (_Level == value)
                    return;
                _Level = value;
            }
        }

        private string _Message;
        public string Message
        {
            get { return _Message; }
            set
            {
                if (_Message == value)
                    return;
                _Message = value;
            }
        }

        private string _CallSite;
        public string CallSite
        {
            get { return _CallSite; }
            set
            {
                if (_CallSite == value)
                    return;
                _CallSite = value;
            }
        }

        private string _Logger;
        public string Logger
        {
            get { return _Logger; }
            set
            {
                if (_Logger == value)
                    return;
                _Logger = value;
            }
        }

        private string _Exception;
        public string Exception
        {
            get { return _Exception; }
            set
            {
                if (_Exception == value)
                    return;
                _Exception = value;
            }
        }
    }
}
