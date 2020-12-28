using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeXusLibrary
{
    public class Commands
    {
        private int _id;
        private string _command;
        private string _action;
        private string _response;

        public Commands()
        {

        }

        public Commands(string command, string action, string response)
        {
            _command = command;
            _action = action;
            _response = response;
        }

        public Commands(int id, string command, string action, string response)
        {
            _id = id;
            _command = command;
            _action = action;
            _response = response;
        }

        public int Id { get => _id; set => _id = value; }
        public string Command { get => _command; set => _command = value; }
        public string Action { get => _action; set => _action = value; }
        public string Response { get => _response; set => _response = value; }
    }
}
