﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRM_2.Startup))]
namespace CRM_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
