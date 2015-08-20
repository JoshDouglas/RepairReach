//using Mvc.Mailer;

//namespace RepairReach.WebApplication.Mailers
//{ 
//    public class ReportMailer : MailerBase, IReportMailer 	
//    {
//        public ReportMailer()
//        {
//            MasterName="_Layout";
//        }
		
//        public virtual MvcMailMessage ReportProduced()
//        {
//            //ViewBag.Data = someObject;
//            return Populate(x =>
//            {
//                x.Subject = "ReportProduced";
//                x.ViewName = "ReportProduced";
//                x.To.Add("some-email@example.com");
//            });
//        }
 
//        public virtual MvcMailMessage ReportSent()
//        {
//            //ViewBag.Data = someObject;
//            return Populate(x =>
//            {
//                x.Subject = "ReportSent";
//                x.ViewName = "ReportSent";
//                x.To.Add("some-email@example.com");
//            });
//        }
 
//        public virtual MvcMailMessage ReportLoading()
//        {
//            //ViewBag.Data = someObject;
//            return Populate(x =>
//            {
//                x.Subject = "ReportLoading";
//                x.ViewName = "ReportLoading";
//                x.To.Add("some-email@example.com");
//            });
//        }
//    }
//}