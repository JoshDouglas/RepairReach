using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;
using RepairReach.Data.Repositories.Interfaces;

namespace RepairReach.Data.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of CompanyRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public CompanyRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="ICompanyRepository"/>
        /// </summary>
        /// <param name="companyId"><see cref="ICompanyRepository"/></param>
        /// <returns><see cref="ICompanyRepository"/></returns>
        public async Task<Company> GetAsync(int? companyId)
        {
            return await _context.Companies.FindAsync(companyId);

        }

        /// <summary>
        /// <see cref="ICompanyRepository"/>
        /// </summary>
        /// <param name="companyId"><see cref="ICompanyRepository"/></param>
        /// <returns><see cref="ICompanyRepository"/></returns>
        public async Task<Company> GetFirstAsync()
        {
            return await _context.Companies.FirstAsync();
        }

        /// <summary>
        /// <see cref="ICompanyRepository"/>
        /// </summary>
        /// <returns><see cref="ICompanyRepository"/></returns>
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies.ToListAsync();

        }

        /// <summary>
        /// <see cref="ICompanyRepository"/>
        /// </summary>
        /// <param name="company"><see cref="ICompanyRepository"/></param>
        /// <returns><see cref="ICompanyRepository"/></returns>
        public async Task<int> AddAsync(Company company)
        {
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company.CompanyId;
        }

        /// <summary>
        /// <see cref="ICompanyRepository"/>
        /// </summary>
        /// <param name="company"><see cref="ICompanyRepository"/></param>
        public async Task UpdateAsync(Company company)
        {
            //06.30.2014 JDD - When changing state here and grabbing the same object again in the controller it will throw an error because it thinks someone else might change the data.
            //_context.Entry<Job>(job)
            //    .State = EntityState.Modified;
            var currentCompany = await _context.Companies.FindAsync(company.CompanyId);
            _context.Entry(currentCompany).CurrentValues.SetValues(company);

            await _context.SaveChangesAsync();

            //_context.Entry<Company>(company)
            //    .State = EntityState.Modified;

            //await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="ICompanyRepository"/>
        /// </summary>
        /// <param name="companyId"><see cref="ICompanyRepository"/></param>
        public async Task DeleteAsync(int? companyId)
        {
            var company = await _context.Companies.FindAsync(companyId);
            if (company != null)
            {
                _context.Companies.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
    }
}
