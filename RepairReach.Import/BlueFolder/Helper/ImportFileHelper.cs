using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RepairReach.Import.BlueFolder.Helper
{
    public static class ImportFileHelper
    {
        public static string ImportDirectory
        {
            get { return "bluefolder"; }
        }

        public static string CustomersFilePath
        {
            get { return Path.Combine(ImportDirectory, "customers.xlsx"); }
        }

        public static string CustomerContactsFilePath
        {
            get { return Path.Combine(ImportDirectory, "customerContacts.xlsx"); }
        }

        public static string CustomerContactEmailsFilePath
        {
            get { return Path.Combine(ImportDirectory, "customerContactEmails.xlsx"); }
        }

        public static string CustomerLocationsFilePath
        {
            get { return Path.Combine(ImportDirectory, "customerLocations.xlsx"); }
        }

        public static string ItemsFilePath
        {
            get { return Path.Combine(ImportDirectory, "items.xlsx"); }
        }

        public static string UsersFilePath
        {
            get { return Path.Combine(ImportDirectory, "users.xlsx"); }
        }

        public static string ServiceRequestsFilePath
        {
            get { return Path.Combine(ImportDirectory, "serviceRequests.xlsx"); }
        }

        public static string ServiceRequestLaborFilePath
        {
            get { return Path.Combine(ImportDirectory, "serviceRequestLabor.xlsx"); }
        }

        public static string ServiceRequestMaterialsFilePath
        {
            get { return Path.Combine(ImportDirectory, "serviceRequestMaterials.xlsx"); }
        }

        public static string ServiceRequestExpensesFilePath
        {
            get { return Path.Combine(ImportDirectory, "serviceRequestExpenses.xlsx"); }
        }
    }
}
