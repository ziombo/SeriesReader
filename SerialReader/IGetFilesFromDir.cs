﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialReader
{
    public interface IGetFilesFromDir
    {
        List<string> GetFiles();
    }
}
