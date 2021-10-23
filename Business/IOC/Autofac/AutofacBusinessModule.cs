using Autofac;
using AutoMapper;
using Business.Managers.Auth.Commands.Register;
using Business.Utilities.Mapping.AutoMapper;
using Business.Utilities.Validation.FluentValidation.User;
using Core.DataAccess.Abstract;
using Core.DataAccess.Concrete;
using Core.Utilities.Security;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Business.IOC.Autofac
{
    public class AutofacBusinessModule : Module
    {
        private readonly IConfiguration _Configuration;
        public AutofacBusinessModule(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AllowNullCollections = true;
            }
             )).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            builder.RegisterType<DapperDbContext>().As<IDbContext>();
            builder.Register<IDbConnection>(connection => new NpgsqlConnection(_Configuration.GetConnectionString("DbFellowshipConnection")));
            builder.RegisterType<UserRegisterValidator>().As<IValidator<RegisterCommand>>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
        }

    }
}
