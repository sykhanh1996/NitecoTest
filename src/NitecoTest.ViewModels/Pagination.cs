﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NitecoTest.ViewModels
{
    public class Pagination<T> : PaginationBase where T : class
    {
        public List<T> Items { get; set; }
    }
}
