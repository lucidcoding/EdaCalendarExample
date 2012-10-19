using System;
using System.Web.Mvc;

namespace Sales.UI.ViewModels
{
    public class ViewConsultantViewModel
    {
        public SelectList Consultants { get; set; }
        public Guid? ConsultantId { get; set; }
        public string FullName { get; set; }
    }
}