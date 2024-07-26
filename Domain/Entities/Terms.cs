using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TermsItem
    {
        [Key]
        public int IdTermsItem { get; set; }
        public string Term {  get; set; }
    }

    public class Terms
    {
        [Key]
        public int IdTerms { get; set; }
        public string Title { get; set; }
        public List<TermsItem> TermsItem {  get; set; }

    }
}
