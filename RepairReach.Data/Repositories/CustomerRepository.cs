using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepairReach.Core.Model;
using RepairReach.Core.Enum;
using RepairReach.Data.Repositories.Interfaces;

namespace RepairReach.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RepairReachContext _context;

        /// <summary>
        /// Creates a new instance of CustomerRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public CustomerRepository(RepairReachContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="ICustomerRepository"/>
        /// </summary>
        /// <param name="customerId"><see cref="ICustomerRepository"/></param>
        /// <returns><see cref="ICustomerRepository"/></returns>
        public async Task<Customer> GetAsync(int? customerId)
        {
            return await _context.Customers.FindAsync(customerId);
        }

        public async Task<Customer> GetForJobAsync(int jobId)
        {
            var job = await _context.Jobs.FindAsync(jobId);
            return job.Customer;
        }

        /// <summary>
        /// <see cref="ICustomerRepository"/>
        /// </summary>
        /// <returns><see cref="ICustomerRepository"/></returns>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();

        }

        public async Task<IEnumerable<Customer>> GetAllByDesignationAndNameLetterAsync(string designation, string nameLetter)
        {
            var designationEnum = CustomerDesignationEnum.Individual;
            if (string.IsNullOrEmpty(designation) == false)
            {
                if (designation.Equals("Company")) designationEnum = CustomerDesignationEnum.Company;
            }
            
            if (string.IsNullOrEmpty(nameLetter) == false) nameLetter = nameLetter.ToLower();
            
            //both?
            if (string.IsNullOrEmpty(designation) == false && string.IsNullOrEmpty(nameLetter) == false)
            {
                return await _context.Customers.Where(c => c.Designation == designationEnum && (c.LastName.ToLower().StartsWith(nameLetter) || c.CompanyName.ToLower().StartsWith(nameLetter))).ToListAsync();
            }

            //nameletter?
            if (string.IsNullOrEmpty(nameLetter) == false)
            {
                return await _context.Customers.Where(c => (c.LastName.ToLower().StartsWith(nameLetter) || c.CompanyName.ToLower().StartsWith(nameLetter))).ToListAsync();
            }

            //designation
            if (string.IsNullOrEmpty(designation) == false)
            {
                return await _context.Customers.Where(c => c.Designation == designationEnum).ToListAsync();
            }

            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetByDesignationAndNameLetterPagedAsync(string designation, string nameLetter, int pageIndex, int pageSize)
        {
            var designationEnum = CustomerDesignationEnum.Individual;
            if (string.IsNullOrEmpty(designation) == false)
            {
                if (designation.Equals("Company")) designationEnum = CustomerDesignationEnum.Company;
            }

            if (string.IsNullOrEmpty(nameLetter) == false) nameLetter = nameLetter.ToLower();

            //both?
            if (string.IsNullOrEmpty(designation) == false && string.IsNullOrEmpty(nameLetter) == false)
            {
                return await _context.Customers
                    .Where(c => c.Designation == designationEnum && (c.LastName.ToLower().StartsWith(nameLetter) || c.CompanyName.ToLower().StartsWith(nameLetter)))
                    .OrderByDescending(c => c.CustomerId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            //nameletter?
            if (string.IsNullOrEmpty(nameLetter) == false)
            {
                return await _context.Customers
                    .Where(c => (c.LastName.ToLower().StartsWith(nameLetter) || c.CompanyName.ToLower().StartsWith(nameLetter)))
                    .OrderByDescending(c => c.CustomerId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            //designation
            if (string.IsNullOrEmpty(designation) == false)
            {
                return await _context.Customers
                    .Where(c => c.Designation == designationEnum)
                    .OrderByDescending(c => c.CustomerId)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }

            return await _context.Customers
                .OrderByDescending(c => c.CustomerId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetCountForDesignationAndNameLetterAsync(string designation, string nameLetter)
        {
            var designationEnum = CustomerDesignationEnum.Individual;
            if (string.IsNullOrEmpty(designation) == false)
            {
                if (designation.Equals("Company")) designationEnum = CustomerDesignationEnum.Company;
            }

            if (string.IsNullOrEmpty(nameLetter) == false) nameLetter = nameLetter.ToLower();

            //both?
            if (string.IsNullOrEmpty(designation) == false && string.IsNullOrEmpty(nameLetter) == false)
            {
                return await _context.Customers
                    .Where(c => c.Designation == designationEnum && (c.LastName.ToLower().StartsWith(nameLetter) || c.CompanyName.ToLower().StartsWith(nameLetter)))
                    .CountAsync();
            }

            //nameletter?
            if (string.IsNullOrEmpty(nameLetter) == false)
            {
                return await _context.Customers
                    .Where(c => (c.LastName.ToLower().StartsWith(nameLetter) || c.CompanyName.ToLower().StartsWith(nameLetter)))
                    .CountAsync();
            }

            //designation
            if (string.IsNullOrEmpty(designation) == false)
            {
                return await _context.Customers
                    .Where(c => c.Designation == designationEnum)
                    .CountAsync();
            }

            return await _context.Customers
                .OrderByDescending(c => c.CustomerId)
                .CountAsync();
        }

        /// <summary>
        /// <see cref="ICustomerRepository"/>
        /// </summary>
        /// <returns><see cref="ICustomerRepository"/></returns>
        public async Task<IEnumerable<Customer>> GetAllByDesignationAsync(string designation)
        {
            if (designation.Equals("Individual") == true)
            {
                return await _context.Customers.Where(c => c.Designation == CustomerDesignationEnum.Individual).ToListAsync();
            }
            else
            {
                return await _context.Customers.Where(c => c.Designation == CustomerDesignationEnum.Company).ToListAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetAllBySearchAsync(string searchTerm)
        {
            string searchTermLower = searchTerm.ToLower();

            return await _context.Customers.Where(c =>
                c.FirstName.ToLower().Contains(searchTermLower) ||
                c.LastName.ToLower().Contains(searchTermLower) ||
                c.CompanyName.ToLower().Contains(searchTermLower) ||
                c.Address1.ToLower().Contains(searchTermLower) ||
                c.City.ToLower().Contains(searchTermLower) ||
                c.State.ToLower().Contains(searchTermLower) ||
                c.Zipcode.ToLower().Contains(searchTermLower)).ToListAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllIndividualByFirstLast(string firstName, string lastName)
        {
            return await _context.Customers.Where(c => c.Designation == CustomerDesignationEnum.Individual &&
                                                       c.FirstName.Trim().ToLower() == firstName &&
                                                       c.LastName.Trim().ToLower() == lastName).ToListAsync();
        }

        /// <summary>
        /// <see cref="ICustomerRepository"/>
        /// </summary>
        /// <param name="customer"><see cref="ICustomerRepository"/></param>
        /// <returns><see cref="ICustomerRepository"/></returns>
        public async Task<int> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer.CustomerId;
        }

        /// <summary>
        /// <see cref="ICustomerRepository"/>
        /// </summary>
        /// <param name="customer"><see cref="ICustomerRepository"/></param>
        public async Task UpdateAsync(Customer customer)
        {
            _context.Entry<Customer>(customer)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="ICustomerRepository"/>
        /// </summary>
        /// <param name="customerId"><see cref="ICustomerRepository"/></param>
        public async Task DeleteAsync(int? customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetTermAsync(string term)
        {
            //circular ref problems when serializing entire json - so return only what we want
            return await _context.Customers.Where(s => s.FirstName.ToLower().Contains(term.ToLower()) ||
                s.LastName.ToLower().Contains(term.ToLower()) ||
                s.CompanyName.ToLower().Contains(term.ToLower())).ToListAsync();
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
