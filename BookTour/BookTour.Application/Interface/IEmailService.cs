﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IEmailService
    {
        public Task sendHtmlMessage(String to, String subject, String htmlBody);
    }
}