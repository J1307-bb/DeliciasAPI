using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class TermsItemResponse
    {
        public string Term {  get; set; }
    }

    public class TermsResponse
    {
        public string Title { get; set; }
        public List<TermsItemResponse> TermsItem {  get; set; }

    }
}
