using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RepairReach.Core.Enum;
using RepairReach.Core.Model;
using RepairReach.Data;
using RepairReach.Data.Infrastructure.Identity;
using RepairReach.Data.Repositories;
using RepairReach.Import.Interface;
using RepairReach.Import.BlueFolder.Helper;
using DataAccess;
using RepairReach.Import.Model;

namespace RepairReach.Import.BlueFolder
{
    public class BlueFolderImportService : ICustomImportService
    {
        private readonly RepairReachContext _context = new RepairReachContext();
        public bool ImportFilesExist()
        {
            if (Directory.Exists(ImportFileHelper.ImportDirectory) == false) return false;

            //customers
            if (File.Exists(ImportFileHelper.CustomersFilePath) == false) return false;
            if (File.Exists(ImportFileHelper.CustomerContactsFilePath) == false) return false;
            if (File.Exists(ImportFileHelper.CustomerContactEmailsFilePath) == false) return false;
            if (File.Exists(ImportFileHelper.CustomerLocationsFilePath) == false) return false;
            if (File.Exists(ImportFileHelper.UsersFilePath) == false) return false;

            return true;
        }

        public IEnumerable<ImportCustomer> ConvertCustomers()
        {
            IList<ImportCustomer> customers = new List<ImportCustomer>();

            //get datatables from excel files
            var dtCustomers = DataTable.New.ReadExcel(ImportFileHelper.CustomersFilePath);
            var dtCustomerContacts = DataTable.New.ReadExcel(ImportFileHelper.CustomerContactsFilePath);
            var dtCustomerContactEmails = DataTable.New.ReadExcel(ImportFileHelper.CustomerContactEmailsFilePath);
            var dtCustomerLocations = DataTable.New.ReadExcel(ImportFileHelper.CustomerLocationsFilePath);
            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);

            foreach (var customerRow in dtCustomers.Rows)
            {
                var importedCustomer = new ImportCustomer();

                int customerId = 0;
                if (Int32.TryParse(customerRow["customerID"], out customerId) == false) break;
                importedCustomer.Customer.ImportedCustomerId = customerId;

                
                var customerType = Convert.ToString(customerRow["customerType"]);
                if (customerType.Equals("Residential") || customerType.Length == 0)
                {
                    importedCustomer.Customer.Designation = CustomerDesignationEnum.Individual;
                }
                else
                {
                    importedCustomer.Customer.Designation = CustomerDesignationEnum.Company;
                    importedCustomer.Customer.CompanyName = Convert.ToString(customerRow["customerName"]);
                }

                //name, phone, & fax data - customerContacts
                var firstName = from row in dtCustomerContacts.Rows
                                where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                        && row["isPrimary"] == "TRUE"
                                  select row["firstName"];
                var lastName = from row in dtCustomerContacts.Rows
                               where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                      && row["isPrimary"] == "TRUE"
                                select row["lastName"];

                if (firstName.First().Length > 0) importedCustomer.Customer.FirstName = firstName.First();
                if (lastName.First().Length > 0) importedCustomer.Customer.LastName = lastName.First();

                var phone = from row in dtCustomerContacts.Rows
                            where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                          && row["isPrimary"] == "TRUE"
                    select row["phone"];
                var phoneFax = from row in dtCustomerContacts.Rows
                               where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                          && row["isPrimary"] == "TRUE"
                    select row["phoneFax"];
                var phoneHome = from row in dtCustomerContacts.Rows
                                where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                          && row["isPrimary"] == "TRUE"
                    select row["phoneHome"];
                var phoneMobile = from row in dtCustomerContacts.Rows
                                  where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                          && row["isPrimary"] == "TRUE"
                    select row["phoneMobile"];

                if (phone.First().Length > 0) importedCustomer.Customer.Phone1 = phone.First();
                if (phoneFax.First().Length > 0) importedCustomer.Customer.Fax = phoneFax.First();
                if (phoneHome.First().Length > 0)
                {
                    if (importedCustomer.Customer.Phone1.Length == 0)
                    {
                        importedCustomer.Customer.Phone1 = phoneHome.First();
                    }
                    else
                    {
                        importedCustomer.Customer.Phone2 = phoneHome.First();
                    }
                }
                if (phoneMobile.First().Length > 0)
                {
                    if (importedCustomer.Customer.Phone1.Length == 0)
                    {
                        importedCustomer.Customer.Phone1 = phoneMobile.First();
                    }
                    else
                    {
                        importedCustomer.Customer.Phone2 = phoneMobile.First();
                    }
                }

                //email - customerContactEmails
                var email = from row in dtCustomerContactEmails.Rows
                            where row["firstName"] == importedCustomer.Customer.FirstName
                                  && row["lastName"] == importedCustomer.Customer.LastName
                            select row["email"];
                if (email.First().Length > 0) importedCustomer.Customer.Email = email.First();

                //address info - customerLocations
                var city = from row in dtCustomerLocations.Rows
                           where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                  && row["isPrimary"] == "TRUE"
                            select row["addressCity"];
                var state = from row in dtCustomerLocations.Rows
                            where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                 && row["isPrimary"] == "TRUE"
                           select row["addressState"];
                var zip = from row in dtCustomerLocations.Rows
                          where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                 && row["isPrimary"] == "TRUE"
                           select row["addressPostalCode"];
                var street = from row in dtCustomerLocations.Rows
                             where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                && row["isPrimary"] == "TRUE"
                          select row["addressStreet"];

                if (city.First().Length > 0) importedCustomer.Customer.City = city.First();
                if (state.First().Length > 0) importedCustomer.Customer.State = state.First();
                if (zip.First().Length > 0) importedCustomer.Customer.Zipcode = zip.First();
                if (street.First().Length > 0) importedCustomer.Customer.Address1 = street.First();

                //marketing
                var phoneNumberAcquiredBy = from row in dtJobs.Rows
                                where row["customerID"] == Convert.ToString(importedCustomer.Customer.ImportedCustomerId)
                                select row["Phone Number Acquired By"];

                if (phoneNumberAcquiredBy.Count() != 0)
                {
                    string firstAcquiredBy = phoneNumberAcquiredBy.First();
                    if (string.IsNullOrEmpty(firstAcquiredBy) == false)
                    {
                        var howDidYouFindUs =
                            _context.HowDidYouFindUses.FirstOrDefault(
                                h => h.Description.ToLower().Equals(firstAcquiredBy.ToLower()));
                        if (howDidYouFindUs != null)
                            importedCustomer.Customer.HowDidYouFindUsId = howDidYouFindUs.HowDidYouFindUsId;
                    }
                }

                customers.Add(importedCustomer);
            }

            return customers;
        }

        public void SaveCustomers(IEnumerable<ImportCustomer> customers)
        {
            foreach (var importedCustomer in customers)
            {
                var customer = importedCustomer.Customer;
                _context.Customers.Add(customer);
            }

            _context.SaveChanges();
        }

        public IEnumerable<Job> ConvertJobs()
        {
            IList<Job> jobs = new List<Job>();

            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);
            foreach (var jobRow in dtJobs.Rows)
            {
                var job = new Job();

                //job fields
                int jobId = 0;
                if (Int32.TryParse(jobRow["serviceRequestID"], out jobId) == false) break;
                job.ImportedJobId = jobId;
                job.JobNumber = jobId; //job number is also id in blue folder

                //string customerLocationStreet = Convert.ToString(jobRow["customerLocationStreet"]);
                //string customerLocationCity = Convert.ToString(jobRow["customerLocationCity"]);
                //string customerLocationState = Convert.ToString(jobRow["customerLocationState"]);
                //string customerLocationPostalCode = Convert.ToString(jobRow["customerLocationPostalCode"]);

                //job.Address1 = customerLocationStreet;
                //job.City = customerLocationCity;
                //job.State = customerLocationState;
                //job.Zipcode = customerLocationPostalCode;

                double createdDouble = Convert.ToDouble(jobRow["dateTimeCreated"]);
                DateTime dateTimeCreated = DateTime.FromOADate(createdDouble);
                job.JobCreated = dateTimeCreated;

                var customerContactName = Convert.ToString(jobRow["customerContactName"]).Split(' ');
                string contactFirstName = string.Empty;
                string contactLastName = string.Empty;
                if (customerContactName.Length == 2)
                {
                    contactFirstName = customerContactName[0];
                    contactLastName = customerContactName[1];
                }
                else
                {
                    contactFirstName = Convert.ToString(jobRow["customerContactName"]);
                    contactLastName = Convert.ToString(jobRow["customerContactName"]);
                }

                job.ContactFirstName = contactFirstName;
                job.ContactLastName = contactLastName;

                string customerContactPhone = Convert.ToString(jobRow["customerContactPhone"]);
                string customerContactMobile = Convert.ToString(jobRow["customerContactPhoneMobile"]);

                job.ContactPhone1 = customerContactPhone;
                job.ContactPhone2 = customerContactMobile;

                //relationship fields
                var staff = new Staff();
                int assignedToUserId = Convert.ToInt32(jobRow["assignedToUserID"]);
                if (assignedToUserId != 0)
                {
                    staff = _context.Staff.SingleOrDefault(s => s.ImportedStaffId == assignedToUserId);
                    if (staff == null) staff = _context.Staff.First();
                    job.StaffId = staff.StaffId;
                }
                else
                {
                    //this is a required field, so if fail set it to the first demo user
                    job.StaffId = _context.Staff.First().StaffId;
                }

                var customer = new Customer();
                int importedCustomerId = Convert.ToInt32(jobRow["customerID"]);
                if (importedCustomerId != 0)
                {
                    customer = _context.Customers.SingleOrDefault(c => c.ImportedCustomerId == importedCustomerId);
                    if (customer == null) customer = _context.Customers.First();

                    job.CustomerId = customer.CustomerId;
                    job.Address1 = customer.Address1;
                    job.City = customer.City;
                    job.State = customer.State;
                    job.Zipcode = customer.Zipcode;
                }

                var jobStatus = new JobStatus();
                string status = Convert.ToString(jobRow["status"]);
                if (string.IsNullOrEmpty(status) == false)
                {
                    jobStatus = _context.JobStatuses.First(js => js.Description.ToLower().Equals(status.ToLower()));
                    job.JobStatusId = jobStatus.JobStatusId;
                }
                else
                {
                    //this is a required field, so if fail set it to first job status
                    job.JobStatusId = _context.JobStatuses.First().JobStatusId;
                }

                job.LastViewedTime = DateTime.Now;
                job.LastViewedBy = "IMPORT";

                double lat = Convert.ToDouble(jobRow["customerLocationLatitude"]);
                double longitude = Convert.ToDouble(jobRow["customerLocationLongitude"]);
                job.Location = new Location();
                job.Location.lat = lat;
                job.Location.lng = longitude;

                jobs.Add(job);
            }

            return jobs;
        }

        public void SaveJobs(IEnumerable<Job> jobs)
        {
            foreach (var job in jobs)
            {
                _context.Jobs.Add(job);
            }

            _context.SaveChanges();
        }

        public IEnumerable<LineItem> ConvertLineItems()
        {
            IList<LineItem> lineItems = new List<LineItem>();
            var dtLabor = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestLaborFilePath);
            var dtMaterials = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestMaterialsFilePath);
            var dtExpenses = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestExpensesFilePath);
            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);

            foreach (var laborRow in dtLabor.Rows)
            {
                var lineItem = new LineItem();

                int serviceRequestId = 0;
                if (Int32.TryParse(laborRow["serviceRequestID"], out serviceRequestId) == false) break;

                //if we can't find a job for this continue on
                var job = _context.Jobs.FirstOrDefault(j => j.ImportedJobId == serviceRequestId);
                if (job == null) continue;
                
                //fields - service fields for labor
                decimal duration = Convert.ToDecimal(laborRow["duration"]);
                string itemDescription = Convert.ToString(laborRow["itemDescription"]);
                decimal itemUnitCost = Convert.ToDecimal(laborRow["itemUnitCost"]);
                decimal itemUnitPrice = Convert.ToDecimal(laborRow["itemUnitPrice"]);
                bool taxable = Convert.ToString(laborRow["taxable"]) == "TRUE";
                int userId = Convert.ToInt32(laborRow["userID"]);

                lineItem.Description = itemDescription;
                lineItem.ServiceQty = duration;
                lineItem.ServiceEach = itemUnitPrice;
                lineItem.ServiceCost = itemUnitCost;
                lineItem.ServiceName = itemDescription;

                //relationships
                if (taxable == true)
                {
                    //grab job rate
                    var rate = from row in dtJobs.Rows
                                    where row["serviceRequestID"] == Convert.ToString(serviceRequestId)
                                    select row["taxRate"];
                    if (rate.First().Length > 0)
                    {
                        decimal dTaxRate = Convert.ToDecimal(rate.First()) * 100;
                        var taxRate = _context.TaxRates.SingleOrDefault(t => t.Amount == dTaxRate);
                        if (taxRate != null) lineItem.TaxRateId = taxRate.TaxRateId;
                    }
                }
                else
                {
                    //grab non-taxable
                    var taxRate = _context.TaxRates.SingleOrDefault(t => t.Amount == 0);
                    if (taxRate != null) lineItem.TaxRateId = taxRate.TaxRateId;
                }

                if (job != null) lineItem.JobId = job.JobId;

                var staff = _context.Staff.SingleOrDefault(s => s.ImportedStaffId == userId);
                if (staff != null) lineItem.StaffId = staff.StaffId;

                //line number
                var lineCountForJob = lineItems.Count(l => l.JobId == lineItem.JobId);
                lineItem.LineItemNumber = lineCountForJob + 1;

                lineItems.Add(lineItem);
            }

            foreach (var materialRow in dtMaterials.Rows)
            {
                var lineItem = new LineItem();

                int serviceRequestId = 0;
                if (Int32.TryParse(materialRow["serviceRequestID"], out serviceRequestId) == false) break;

                //if we can't find a job for this continue on
                var job = _context.Jobs.FirstOrDefault(j => j.ImportedJobId == serviceRequestId);
                if (job == null) continue;

                //fields - parts fields for materials
                decimal itemQuantity = Convert.ToDecimal(materialRow["itemQuantity"]);
                string itemDescription = Convert.ToString(materialRow["itemDescription"]);
                decimal itemUnitCost = Convert.ToDecimal(materialRow["itemUnitCost"]);
                decimal itemUnitPrice = Convert.ToDecimal(materialRow["itemUnitPrice"]);
                bool taxable = Convert.ToString(materialRow["taxable"]) == "TRUE";
                int userId = Convert.ToInt32(materialRow["inputByUserID"]);

                lineItem.Description = itemDescription;
                lineItem.PartQty = itemQuantity;
                lineItem.PartEach = itemUnitPrice;
                lineItem.PartCost = itemUnitCost;
                lineItem.PartName = itemDescription;
                lineItem.PartNumber = itemDescription;

                //relationships
                if (taxable == true)
                {
                    //grab job rate
                    var rate = from row in dtJobs.Rows
                               where row["serviceRequestID"] == Convert.ToString(serviceRequestId)
                               select row["taxRate"];
                    if (rate.First().Length > 0)
                    {
                        decimal dTaxRate = Convert.ToDecimal(rate.First()) * 100;
                        var taxRate = _context.TaxRates.SingleOrDefault(t => t.Amount == dTaxRate);
                        if (taxRate != null) lineItem.TaxRateId = taxRate.TaxRateId;
                    }
                }
                else
                {
                    //grab non-taxable
                    var taxRate = _context.TaxRates.SingleOrDefault(t => t.Amount == 0);
                    if (taxRate != null) lineItem.TaxRateId = taxRate.TaxRateId;
                }

                if (job != null) lineItem.JobId = job.JobId;

                var staff = _context.Staff.SingleOrDefault(s => s.ImportedStaffId == userId);
                if (staff != null) lineItem.StaffId = staff.StaffId;

                //line number
                var lineCountForJob = lineItems.Count(l => l.JobId == lineItem.JobId);
                lineItem.LineItemNumber = lineCountForJob + 1;

                lineItems.Add(lineItem);
            }

            foreach (var expenseRow in dtExpenses.Rows)
            {
                var lineItem = new LineItem();

                int serviceRequestId = 0;
                if (Int32.TryParse(expenseRow["serviceRequestID"], out serviceRequestId) == false) break;

                //if we can't find a job for this continue on
                var job = _context.Jobs.FirstOrDefault(j => j.ImportedJobId == serviceRequestId);
                if (job == null) continue;

                //fields - service fields for expenses
                decimal itemQuantity = Convert.ToDecimal(expenseRow["itemQuantity"]);
                string itemDescription = Convert.ToString(expenseRow["itemDescription"]);
                decimal itemUnitCost = Convert.ToDecimal(expenseRow["itemUnitCost"]);
                decimal itemUnitPrice = Convert.ToDecimal(expenseRow["itemUnitPrice"]);
                bool taxable = Convert.ToString(expenseRow["taxable"]) == "TRUE";
                int userId = Convert.ToInt32(expenseRow["userID"]);

                lineItem.Description = itemDescription;
                lineItem.ServiceQty = itemQuantity;
                lineItem.ServiceEach = itemUnitPrice;
                lineItem.ServiceCost = itemUnitCost;
                lineItem.ServiceName = itemDescription;

                //relationships
                if (taxable == true)
                {
                    //grab job rate
                    var rate = from row in dtJobs.Rows
                               where row["serviceRequestID"] == Convert.ToString(serviceRequestId)
                               select row["taxRate"];
                    if (rate.First().Length > 0)
                    {
                        decimal dTaxRate = Convert.ToDecimal(rate.First()) * 100;
                        var taxRate = _context.TaxRates.SingleOrDefault(t => t.Amount == dTaxRate);
                        if (taxRate != null) lineItem.TaxRateId = taxRate.TaxRateId;
                    }
                }
                else
                {
                    //grab non-taxable
                    var taxRate = _context.TaxRates.SingleOrDefault(t => t.Amount == 0);
                    if (taxRate != null) lineItem.TaxRateId = taxRate.TaxRateId;
                }

                if (job != null) lineItem.JobId = job.JobId;

                var staff = _context.Staff.SingleOrDefault(s => s.ImportedStaffId == userId);
                if (staff != null) lineItem.StaffId = staff.StaffId;

                //line number
                var lineCountForJob = lineItems.Count(l => l.JobId == lineItem.JobId);
                lineItem.LineItemNumber = lineCountForJob + 1;

                lineItems.Add(lineItem);
            }

            return lineItems;
        }

        public void SaveLineItems(IEnumerable<LineItem> lineItems)
        {
            foreach (var lineItem in lineItems)
            {
                _context.LineItems.Add(lineItem);
            }

            _context.SaveChanges();
        }

        public IEnumerable<Staff> ConvertStaffs()
        {
            IList<Staff> staffs = new List<Staff>();

            var dtUsers = DataTable.New.ReadExcel(ImportFileHelper.UsersFilePath);
            foreach (var userRow in dtUsers.Rows)
            {
                var staff = new Staff();

                int staffId = 0;
                if (Int32.TryParse(userRow["userId"], out staffId) == false) break;
                staff.ImportedStaffId = staffId;

                var displayName = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["displayName"];
                var email = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["email"];
                var firstName = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["firstName"];
                var lastName = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["lastName"];
                var jobTitle = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["jobTitle"];
                var phone = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["phone"];
                var inactive = from row in dtUsers.Rows
                            where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                            select row["inactive"];
                var userName = from row in dtUsers.Rows
                               where row["userId"] == Convert.ToString(staff.ImportedStaffId)
                               select row["userName"];

                if (displayName.First().Length > 0) staff.DisplayName = displayName.First();
                if (email.First().Length > 0) staff.Email = email.First();
                if (firstName.First().Length > 0) staff.FirstName = firstName.First();
                if (lastName.First().Length > 0) staff.LastName = lastName.First();
                if (phone.First().Length > 0) staff.Phone = phone.First();
                if (userName.First().Length > 0) staff.Username = userName.First();
                if (inactive.First().Length > 0) staff.IsActive = inactive.First().Equals("TRUE") == false;

                staff.UserTitle = UserTitleEnum.SalesRepresentative; //default
                if (jobTitle.First().Length > 0)
                {
                    var userTitle = jobTitle.First();
                    if (userTitle.Contains("dinator")) staff.UserTitle = UserTitleEnum.Dispatcher;
                    if (userTitle.Contains("Tech")) staff.UserTitle = UserTitleEnum.Technician;
                    if (userTitle.Contains("Manager")) staff.UserTitle = UserTitleEnum.SalesRepresentative;
                }

                staffs.Add(staff);
            }

            return staffs;
        }

        public void SaveStaffs(IEnumerable<Staff> staffs)
        {
            foreach (var staff in staffs)
            {
                _context.Staff.Add(staff);
            }

            _context.SaveChanges();
        }

        public IEnumerable<JobNote> ConvertJobNotes()
        {
            IList<JobNote> jobNotes = new List<JobNote>();
            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);

            foreach (var jobRow in dtJobs.Rows)
            {
                var jobNote = new JobNote();

                int jobId = 0;
                if (Int32.TryParse(jobRow["serviceRequestID"], out jobId) == false) break;

                var job = _context.Jobs.SingleOrDefault(j => j.ImportedJobId == jobId);
                if (job == null) continue;

                jobNote.JobId = job.JobId;

                double createdDouble = Convert.ToDouble(jobRow["dateTimeCreated"]);
                DateTime dateTimeCreated = DateTime.FromOADate(createdDouble);
                jobNote.CreatedDate = dateTimeCreated;

                var staff = new Staff();
                int assignedToUserId = Convert.ToInt32(jobRow["assignedToUserID"]);
                if (assignedToUserId != 0)
                {
                    staff = _context.Staff.SingleOrDefault(s => s.ImportedStaffId == assignedToUserId);
                    if (staff != null)
                    {
                        jobNote.CreatedBy = staff.DisplayName;
                    }
                    else
                    {
                        jobNote.CreatedBy = "IMPORT";
                    }
                    
                }
                else
                {
                    jobNote.CreatedBy = "IMPORT";
                }

                string notes = Convert.ToString(jobRow["notes"]);
                jobNote.Note = notes;

                if (notes.Length > 0)
                {
                    jobNotes.Add(jobNote);
                }
            }

            return jobNotes;
        }

        public void SaveJobNotes(IEnumerable<JobNote> jobNotes)
        {
            foreach (var jobNote in jobNotes)
            {
                _context.JobNotes.Add(jobNote);
            }

            _context.SaveChanges();
        }

        public IEnumerable<TaxRate> ConvertTaxRates()
        {
            IList<TaxRate> taxRates = new List<TaxRate>();

            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);

            foreach (var jobRow in dtJobs.Rows)
            {
                var taxRate = new TaxRate();
                var taxRateExists = true;

                decimal rate = 0;
                if (decimal.TryParse(jobRow["taxRate"], out rate) == true)
                {
                    rate = rate * 100; //get storage value

                    var taxRateCount = _context.TaxRates.Count(t => t.Amount == rate);
                    var newTaxRateCount = taxRates.Count(t => t.Amount == rate);

                    if (taxRateCount == 0 && newTaxRateCount == 0)
                    {
                        taxRateExists = false;
                        taxRate.Amount = rate;
                        if (rate == 0)
                        {
                            taxRate.Name = "Non-taxable";
                        }
                        else
                        {
                            taxRate.Name = rate + " Imported Rate";
                        }

                        taxRate.IsDefaultRate = false;
                    }
                }

                if (taxRateExists == false) taxRates.Add(taxRate);
            }

            return taxRates;
        }

        public void SaveTaxRates(IEnumerable<TaxRate> taxRates)
        {
            foreach (var taxRate in taxRates)
            {
                _context.TaxRates.Add(taxRate);
            }

            _context.SaveChanges();
        }

        public IEnumerable<JobStatus> ConvertJobStatuses()
        {
            IList<JobStatus> jobStatuses = new List<JobStatus>();

            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);

            foreach (var jobRow in dtJobs.Rows)
            {
                var jobStatus = new JobStatus();
                var statusExists = true;

                var status = Convert.ToString(jobRow["status"]);

                //does it exist?
                if (status.Length > 0)
                {
                    var statusCount = _context.JobStatuses.Count(js => js.Description.ToLower().Equals(status.ToLower()));
                    if (statusCount == 0 && jobStatuses.Count(js => js.Description.Equals(status)) == 0)
                    {
                        statusExists = false;
                        jobStatus.Description = status;
                        jobStatus.SequenceNumber = 0;
                    }
                }

                if (statusExists == false) jobStatuses.Add(jobStatus);
            }

            return jobStatuses;
        }

        public void SaveJobStatuses(IEnumerable<JobStatus> jobStatuses)
        {
            foreach (var status in jobStatuses)
            {
                _context.JobStatuses.Add(status);
            }

            _context.SaveChanges();
        }

        public IEnumerable<QuickLineItem> ConvertQuickLineItems()
        {
            IList<QuickLineItem> quickLineItems = new List<QuickLineItem>();
            var dtItems = DataTable.New.ReadExcel(ImportFileHelper.ItemsFilePath);

            foreach (var itemRow in dtItems.Rows)
            {
                var quickLineItem = new QuickLineItem();

                bool discontinued = Convert.ToString(itemRow["discontinued"]).Equals("TRUE");
                if (discontinued == true) continue; //don't add items that are discontinued

                string type = Convert.ToString(itemRow["type"]);
                if (type.Length == 0) continue; //can't do much without a type

                string itemNo = Convert.ToString(itemRow["itemNo"]);
                //bool taxable = Convert.ToString(itemRow["taxableDefault"]).Equals("TRUE");
                decimal unitCost = Convert.ToDecimal(itemRow["unitCost"]);
                decimal unitPrice = Convert.ToDecimal(itemRow["unitPrice"]);

                switch (type)
                {
                    case "materials": //part
                        quickLineItem.Description = itemNo;
                        quickLineItem.PartName = itemNo;
                        quickLineItem.PartNumber = itemNo;
                        quickLineItem.PartQty = 1;
                        quickLineItem.PartCost = unitCost;
                        quickLineItem.PartEach = unitPrice;
                        break;
                    case "labor": //service
                        quickLineItem.Description = itemNo;
                        quickLineItem.ServiceName = itemNo;
                        quickLineItem.ServiceQty = 1;
                        quickLineItem.ServiceCost = unitCost;
                        quickLineItem.ServiceEach = unitPrice;
                        break;
                    case "expense": //service
                        quickLineItem.Description = itemNo;
                        quickLineItem.ServiceName = itemNo;
                        quickLineItem.ServiceQty = 1;
                        quickLineItem.ServiceCost = unitCost;
                        quickLineItem.ServiceEach = unitPrice;
                        break;
                    default: //default as service
                        quickLineItem.Description = itemNo;
                        quickLineItem.ServiceName = itemNo;
                        quickLineItem.ServiceQty = 1;
                        quickLineItem.ServiceCost = unitCost;
                        quickLineItem.ServiceEach = unitPrice;
                        break;
                }

                quickLineItems.Add(quickLineItem);
            }

            return quickLineItems;
        }

        public void SaveQuickLineItems(IEnumerable<QuickLineItem> quickLineItems)
        {
            foreach (var quickLineItem in quickLineItems)
            {
                _context.QuickLineItems.Add(quickLineItem);
            }

            _context.SaveChanges();
        }

        public void SaveContext()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HowDidYouFindUs> ConvertHowDidYouFindUses()
        {
            IList<HowDidYouFindUs> howDidYouFindUses = new List<HowDidYouFindUs>();
            var dtJobs = DataTable.New.ReadExcel(ImportFileHelper.ServiceRequestsFilePath);

            foreach (var jobRow in dtJobs.Rows)
            {
                var howDidYouFindUs = new HowDidYouFindUs();

                string phoneNumberAcquiredBy = Convert.ToString(jobRow["Phone Number Acquired By"]);
                if (string.IsNullOrEmpty(phoneNumberAcquiredBy) == true) continue;

                bool existsInDB =
                    _context.HowDidYouFindUses.Count(
                        h => h.Description.ToLower().Equals(phoneNumberAcquiredBy.ToLower())) > 0;
                bool alreadyAddedToList = false;
                if (howDidYouFindUses.Count != 0)
                {
                    var listCount =
                        howDidYouFindUses.Count(h => h.Description.ToLower().Equals(phoneNumberAcquiredBy.ToLower()));
                    if (listCount > 0) alreadyAddedToList = true;
                }

                howDidYouFindUs.Description = phoneNumberAcquiredBy;
                howDidYouFindUs.SequenceNumber = 0;

                //if it doesn't exist add it
                if (existsInDB == false && alreadyAddedToList == false)
                {
                    howDidYouFindUses.Add(howDidYouFindUs);
                }
            }

            return howDidYouFindUses;
        }

        public void SaveHowDidYouFindUses(IEnumerable<HowDidYouFindUs> howDidYouFindUses)
        {
            foreach (var howDidYouFindUs in howDidYouFindUses)
            {
                _context.HowDidYouFindUses.Add(howDidYouFindUs);
            }

            _context.SaveChanges();
        }

        public bool DatabaseIsConnected()
        {
            return _context.Database.Exists();
        }
    }
}
