﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(VitalbetSportsProvider.WebClient.Startup))]
namespace VitalbetSportsProvider.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}