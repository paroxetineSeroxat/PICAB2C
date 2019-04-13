﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TB.Domain.BE;
using TB.Domain.Context;
using TB.Repository.Base;

namespace TB.Repository.Repositories
{
    public class TransportCompanyRepository : BaseRepository<TransportCompany>
    {
        public TransportCompanyRepository(TBContext context) : base(context)
        {
        }

        public override int Add(TransportCompany entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TransportCompany entity)
        {
            throw new NotImplementedException();
        }

        public override TransportCompany FindById(int id)
        {
            
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    TransportCompany query = ((TBContext)context).TransportCompany.First(u => u.Id == id);
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override TransportCompany FindByName(string name)
        {
            try
            {

                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    bool exist = ((TBContext)context).TransportCompany.Any(u => u.CompanyName == name);
                    if (exist)
                    {
                        TransportCompany query = ((TBContext)context).TransportCompany.First(u => u.CompanyName == name);
                        return query;
                    }
                    else
                        return null;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<TransportCompany> GetAll()
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<TransportCompany>().ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override List<TransportCompany> GetAll(Expression<Func<TransportCompany, bool>> Predicate)
        {
            try
            {
                if (context == null || IsDisposed())
                    context = new TBContext();

                using (context)
                {
                    var query = context.Set<TransportCompany>().Where(Predicate).ToList();
                    return query;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override bool IsDisposed()
        {
            var result = true;

            var typeDbContext = typeof(DbContext);
            var typeInternalContext = typeDbContext.Assembly.GetType("System.Data.Entity.Internal.InternalContext");

            var fi_InternalContext = typeDbContext.GetField("_internalContext", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var pi_IsDisposed = typeInternalContext.GetProperty("IsDisposed");

            var ic = fi_InternalContext.GetValue(context);

            if (ic != null)
            {
                result = (bool)pi_IsDisposed.GetValue(ic);
            }

            return result;
        }

        public override void Update(TransportCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}
