using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UscProject.Models;

namespace UscProject.Areas.Admin.ViewModels
{
    public class contact_comment_vm
    {
        public List<CommentTB> comment;
        public List<ContactTB> contact;
    }
}