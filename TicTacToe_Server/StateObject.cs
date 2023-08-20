using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_Server
{
    public class StateObject
    {
        public Socket Socket { get; set; }
        public byte[] Buffer { get; set; }
    }
}
