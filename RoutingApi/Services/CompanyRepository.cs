using Microsoft.EntityFrameworkCore;
using RoutingApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoutingApi.Services
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RoutingDbContext _context;
        public CompanyRepository(RoutingDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void UpadateCompany(Company company)
        {

        }
        public void AddCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Add(company);
        }

        public async Task<bool> CompanyExistAsync(Guid CompanyId)
        {
            if (CompanyId == null)
            {
                throw new ArgumentNullException(nameof(CompanyId));
            }
            return await _context.Companies.AnyAsync(predicate: x => x.Id == CompanyId);
        }
        public void DeleteCompany(Company company)
        {
            if (company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Remove(company);
        }
        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
           if (companyIds == null)
            {
                throw new ArgumentNullException(nameof(companyIds));
            }
            return await _context.Companies.
                Where(x => companyIds.Contains(x.Id)).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            return await _context.Companies
                .FirstOrDefaultAsync(predicate: x => x.Id == companyId);
        }
        public void AddEmployee(Guid companydId, Employee employee)
        {
            if (companydId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companydId));
            }
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            employee.CompanyId = companydId;
            _context.employees.Add(employee);
        }
        public async Task<Employee> GetEmployeeAsync(Guid companyId,Guid employeeID)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
            if (employeeID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(employeeID));
            }
            return await _context.employees.Where(x => x.CompanyId == companyId && x.Id == employeeID).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentException(nameof(companyId));
            }
            return await _context.employees
                .Where(x => x.CompanyId == companyId)
                .OrderBy(x => x.EmployeeNo)
                .ToListAsync();
        }
        public void DeleteEmployee(Employee employee)
        {
            _context.employees.Remove(employee);
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void UpdateEmployee(Employee employee)
        {
            //throw new NotImplementedException();
        }
    }
}
