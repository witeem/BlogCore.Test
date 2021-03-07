﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace BlogCore.Application.Services
{
    public class AppService : IAppService
    {
        protected IMapper Mapper { get; set; }

        protected AppSrvResult AppSrvResult()
        {
            return new AppSrvResult();
        }

        protected AppSrvResult<TValue> AppSrvResult<TValue>([NotNull] TValue value)
        {
            return new AppSrvResult<TValue>(value);
        }

        protected ProblemDetails Problem(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null)
        {
            return new ProblemDetails(statusCode, detail, title, instance, type);
        }

        protected Expression<Func<TEntity, object>>[] UpdatingProps<TEntity>(params Expression<Func<TEntity, object>>[] expressions)
        {
            return expressions;
        }
    }
}