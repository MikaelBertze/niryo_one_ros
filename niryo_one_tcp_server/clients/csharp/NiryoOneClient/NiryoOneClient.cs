﻿/*  MIT License

    Copyright (c) 2019 Niryo

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
 */

using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NiryoOneClient {
    public enum CalibrateMode {
        AUTO,
        MANUAL
    }

    public class NiryoOneClient {
        private TcpClient _client;
        private int _port;
        private string _server;

        public NiryoOneClient (string server, int port) {
            _server = server;
            _port = port;
        }

        public async Task<NiryoOneConnection> Connect () {
            if (_client != null) {
                _client.Close ();
                _client.Dispose ();
                _client = null;
            }

            _client = new TcpClient ();
            await _client.ConnectAsync (_server, _port);
            var stream = _client.GetStream ();
            return new NiryoOneConnection (new System.IO.StreamReader (stream), new System.IO.StreamWriter (stream));
        }
    }
}