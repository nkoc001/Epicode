﻿namespace Epicode.Interfaces
{
    public interface IConfiguration
    {
        string ServerAddress { get; set; }
        string SenderAddress { get; set; }
        int Port { get; set; }
        bool IsSslEnabled { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
