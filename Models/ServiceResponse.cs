﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_new_try
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public String Message { get; set; } = null;
        public List<String> Errors { get; set; } = new List<String>();

    }
}
